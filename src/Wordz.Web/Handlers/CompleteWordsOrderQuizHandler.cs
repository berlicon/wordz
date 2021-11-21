using System.Web;
using System.Web.SessionState;
using Wordz.BC;

namespace Wordz.Web.Handlers {
    public class CompleteWordsOrderQuizHandler : IHttpHandler, IRequiresSessionState {
        public bool IsReusable {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context) {
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;

            string nick = request.QueryString["Nick"].ToString();
            int successCount = int.Parse(request.QueryString["SuccessCount"].ToString());
            int accountId = StorageManager.CurrentAccountId;

            string content = BCCommon.GetWordsOrderQuizResultHTML(
                accountId, nick, successCount, StorageManager.LanguageLearnId);
            response.Write(content);
            response.End();
        }
    }
}