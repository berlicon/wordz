using System.Web;
using System.Web.SessionState;
using Wordz.BC;

namespace Wordz.Web.Handlers {
    public class LoadLevelQuizHandler : IHttpHandler, IRequiresSessionState {
        public bool IsReusable {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context) {
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;

            int step = int.Parse(request.QueryString["Step"].ToString());

            string content = BCCommon.GetLevelQuizHTML(++step, StorageManager.LanguageLearnId);
            response.Write(content);
            response.End();
        }
    }
}