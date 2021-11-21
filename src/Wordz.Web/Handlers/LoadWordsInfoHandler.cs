using System.Collections;
using System.Web;
using System.Web.SessionState;
using Wordz.BC;
using Wordz.BE;

namespace Wordz.Web.Handlers {
    public class LoadWordsInfoHandler : IHttpHandler, IRequiresSessionState {
        public bool IsReusable {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context) {
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;

            bool notUseWellKnownWords = bool.Parse(request.QueryString["NotUseWellKnownWords"].ToString());

            Hashtable words = BCCommon.CreateWordsCollection(StorageManager.SourceText, false, StorageManager.LanguageLearnId);
            int accountId = StorageManager.CurrentAccountId;

            Hashtable wordsCopy = new Hashtable(words.Count);
            foreach (object key in words.Keys) {
                WordElement wordElement = (WordElement) words[key];
                BCCommon.FillWordInfo(ref wordElement, accountId, StorageManager.LanguageNativeId, StorageManager.LanguageLearnId);
                wordsCopy[key] = wordElement;
            }
            BCCommon.UpdateMixedOrderForSynonyms(ref wordsCopy);
            words.Clear();
            words = wordsCopy;

            WordsInfo wordsInfo = BCCommon.GetWordsInfo(words, 0, int.MaxValue,
                notUseWellKnownWords, true, false);
            IdNamePair[] wordIds;
            BCCommon.ShowProcessText(wordsInfo.ProcessedWords,
                true, SortBy.MixedOrder, out wordIds);
            StorageManager.ProcessedWordIds = wordIds;

            bool addHeaderInfo = 
                (1.05 * StorageManager.SourceText.Length >
                BCCommon.MaxTextLengthForProcess);
            string content = BCCommon.GetTestedWordsHTML(wordIds, addHeaderInfo, StorageManager.LanguageNativeId, StorageManager.LanguageLearnId);

            response.Write(content);
            response.End();
        }
    }
}