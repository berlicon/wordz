using System.Web;
using System.Web.SessionState;
using Wordz.BC;
using Wordz.Web.Util;

namespace Wordz.Web.Handlers {
    public class AddSourceTextHandler : IHttpHandler, IRequiresSessionState {
        public bool IsReusable {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context) {
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;
            string text = HandlersUtil.GetContent(request.InputStream);
            StorageManager.SourceText =
                (text.Length < BCCommon.MaxTextLengthForProcess)
                ? text : text.Substring(0, (int)BCCommon.MaxTextLengthForProcess);
            response.Write(BCCommon.OkText);
            response.End();
        }
    }
}