using System.Web;
using System.Web.SessionState;
using Wordz.BC;

namespace Wordz.Web.Handlers {
    public class ChangeLanguageHandler : IHttpHandler, IRequiresSessionState {
        public bool IsReusable {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context) {
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;

            StorageManager.LanguageNativeId = int.Parse(request.QueryString["LanguageNativeId"].ToString());
            StorageManager.LanguageLearnId = int.Parse(request.QueryString["LanguageLearnId"].ToString());

            response.Write(BCCommon.OkText);
            response.End();
        }
    }
}