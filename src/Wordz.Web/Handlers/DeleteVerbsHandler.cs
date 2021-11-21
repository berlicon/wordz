using System.Web;
using System.Web.SessionState;
using Wordz.BC;

namespace Wordz.Web.Handlers {
    public class DeleteVerbsHandler : IHttpHandler, IRequiresSessionState {
        public bool IsReusable {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context) {
            HttpResponse response = context.Response;

            if (StorageManager.UserLogined) {
                BCCommon.DeleteAllVerbsInAccount(StorageManager.CurrentAccount.Id);
                response.Write(BCCommon.OkText);
            }
            response.End();
        }
    }
}