using System.Text;
using System.Web;
using System.Web.SessionState;
using Wordz.BC;
using Wordz.BE;
using Wordz.Web.Util;

namespace Wordz.Web.Handlers {
    public class AnalysisHandler : IHttpHandler, IRequiresSessionState {
        public bool IsReusable {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context) {
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;

            string wordsString = HandlersUtil.GetContent(request.InputStream);
            string[] words = wordsString.Split(BCCommon.Separator);
            int accountId = StorageManager.CurrentAccountId;

            StringBuilder builder = new StringBuilder();
            foreach (string word in words) {
                WordElement wordElement = new WordElement(word);
                BCCommon.FillWordInfo(ref wordElement, accountId, StorageManager.LanguageNativeId,StorageManager.LanguageLearnId);
                builder.Append(wordElement.WellKnown ?
                    BCCommon.KnownWordClass[0] : BCCommon.UnknownWordClass[0]);
            }

            response.Write(builder.ToString());
            response.End();
        }
    }
}