using System;
using System.Collections;
using System.Globalization;
using System.Web;
using System.Web.SessionState;
using Wordz.BC;
using Wordz.BE;
using Wordz.Lng;

namespace Wordz.Web.Handlers {
    public class ProcessHandler : IHttpHandler, IRequiresSessionState {
        public bool IsReusable {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context) {
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;

            WordsOrder wordsOrder = (WordsOrder) int.Parse(request.QueryString["WordOrder"]);
            bool notUseWellKnownWords = bool.Parse(request.QueryString["NotUseWellKnownWords"].ToString());
            bool notUseNotKnownWords = bool.Parse(request.QueryString["NotUseNotKnownWords"].ToString());
            bool notUseNotSoundedWords = bool.Parse(request.QueryString["NotUseNotSoundedWords"].ToString());
            SortBy sortBy = (SortBy) int.Parse(request.QueryString["Sort"]);
            
            double minFrequencyDouble;
            double.TryParse(request.QueryString["MinFrequency"], NumberStyles.Integer, new NumberFormatInfo(), out minFrequencyDouble);
            decimal minFrequency = (decimal) minFrequencyDouble;
            
            double maxSignedWordsDouble;
            bool convertSuccess = double.TryParse(request.QueryString["MaxSignedWords"], NumberStyles.Integer, new NumberFormatInfo(), out maxSignedWordsDouble);
            int maxSignedWords = (convertSuccess) ? (int) maxSignedWordsDouble : int.MaxValue;

            Hashtable words = BCCommon.CreateWordsCollection(
                StorageManager.SourceText, StorageManager.UseSpecialWordSeparator, StorageManager.LanguageLearnId);
            int accountId = StorageManager.CurrentAccountId;

            Hashtable wordsCopy = new Hashtable(words.Count);
            foreach (object key in words.Keys) {
                WordElement wordElement = (WordElement) words[key];
                BCCommon.FillWordInfo(ref wordElement, accountId, StorageManager.LanguageNativeId, StorageManager.LanguageLearnId);
                wordsCopy[key] = wordElement;
            }
            words.Clear();
            words = wordsCopy;

            WordsInfo wordsInfo = BCCommon.GetWordsInfo(words, minFrequency,
                maxSignedWords, notUseWellKnownWords, notUseNotKnownWords,
                notUseNotSoundedWords);
            IdNamePair[] wordIds;
            StorageManager.ProcessedText =
                BCCommon.ShowProcessText(wordsInfo.ProcessedWords,
                wordsOrder == WordsOrder.Learn, sortBy, out wordIds);
            StorageManager.ProcessedWordIds = wordIds;
            StorageManager.LearnFirstWord = (wordsOrder == WordsOrder.Learn);

            string cutHeaderInfo = 
                (1.05 * StorageManager.SourceText.Length >
                BCCommon.MaxTextLengthForProcess)
                ? string.Format(CurrentLanguage.MessageCutDataForAnalysisFormatString,
                BCCommon.MaxTextLengthForProcess) + 
                Environment.NewLine + Environment.NewLine
                : string.Empty;

            response.Write(wordsInfo.WordsCount);
            response.Write(BCCommon.Separator);
            response.Write(wordsInfo.MaxFrequency);
            response.Write(BCCommon.Separator);
            response.Write(cutHeaderInfo);
            response.Write(StorageManager.ProcessedText);
            response.End();
        }
    }
}