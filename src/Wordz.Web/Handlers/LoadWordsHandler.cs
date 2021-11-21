using System;
using System.Web;
using System.Web.SessionState;
using Wordz.BC;
using Wordz.BE;

namespace Wordz.Web.Handlers {
    public class LoadWordsHandler : IHttpHandler, IRequiresSessionState {
        public bool IsReusable {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context) {
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;

            WordsSource wordsSource = (WordsSource) int.Parse(request.QueryString["WordsSource"]);
            WordsSelector wordsSelector = (WordsSelector) int.Parse(request.QueryString["WordsSelector"]);
            int domainId = int.Parse(request.QueryString["DomainId"]);

            int wordCount = Constants.DefaultWordCount;
            try {
                wordCount = Math.Abs(int.Parse(request.QueryString["WordCount"]));
            } catch {}

            int wordStartIndex = Constants.DefaultWordStartIndex;
            try {
                wordStartIndex = int.Parse(request.QueryString["WordStartIndex"]);
            } catch {}

            string content = "";
            if (wordsSource == WordsSource.LoadFromProcessPage) {
                content = BCCommon.GetCheckedWordsHTML(
                    StorageManager.ProcessedWordIds);
            } else {
                IdNamePair[] idNamePairs =
                    BCCommon.GetWordIds(StorageManager.CurrentAccountId, wordsSelector,
                    domainId, wordCount, wordStartIndex, StorageManager.LanguageNativeId, StorageManager.LanguageLearnId);
                content = BCCommon.GetCheckedWordsHTML(idNamePairs);
            }

            response.Write(content);
            response.End();
        }
    }
}