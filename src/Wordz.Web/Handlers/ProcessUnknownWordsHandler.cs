using System.Web;
using System.Web.SessionState;
using Wordz.BC;
using Wordz.Web.Util;

namespace Wordz.Web.Handlers {
    public class ProcessUnknownWordsHandler : IHttpHandler, IRequiresSessionState {
        public bool IsReusable {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context) {
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;

            StorageManager.SourceText = HandlersUtil.GetContent(request.InputStream);
            string url = "/Process/1";
            response.Write(url);
            response.End();
        }
    }
}