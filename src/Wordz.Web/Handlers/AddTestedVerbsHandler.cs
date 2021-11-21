using System.Web;
using System.Web.SessionState;
using Wordz.BC;
using Wordz.Web.Util;

namespace Wordz.Web.Handlers {
    public class AddTestedVerbsHandler : IHttpHandler, IRequiresSessionState {
        public bool IsReusable {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context) {
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;

            if (StorageManager.UserLogined) {
                string idsString = HandlersUtil.GetContent(request.InputStream);
                string[] ids = idsString.Split(BCCommon.WordsIdSeparator);
                BCCommon.AddVerbsToAccount(StorageManager.CurrentAccount.Id, ids);
                response.Write(BCCommon.GetAccountVerbsInfo(StorageManager.CurrentAccountId));
            }
            response.End();
        }
    }
}