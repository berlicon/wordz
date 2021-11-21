using System.Web;
using System.Web.SessionState;
using Wordz.BC;
using Wordz.Web.Util;

namespace Wordz.Web.Handlers {
    public class DeleteWordsHandler : IHttpHandler, IRequiresSessionState {
        public bool IsReusable {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context) {
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;

            if (StorageManager.UserLogined) {
                string idsString = HandlersUtil.GetContent(request.InputStream);
                string[] ids = idsString.Split(BCCommon.WordsIdSeparator);
                BCCommon.DeleteWordsFromAccount(StorageManager.CurrentAccount.Id, ids, StorageManager.LanguageLearnId);
                response.Write(BCCommon.OkText);
            }
            response.End();
        }
    }
}