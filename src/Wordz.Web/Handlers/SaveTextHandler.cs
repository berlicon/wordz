using System.Web;
using System.Web.SessionState;

namespace Wordz.Web.Handlers {
    public class SaveTextHandler : IHttpHandler, IRequiresSessionState {
        public bool IsReusable {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context) {
            HttpResponse response = context.Response;
            
            response.ClearContent();
            response.ClearHeaders();
            
            response.ContentType = "text/plain";
            response.ContentEncoding = System.Text.Encoding.UTF8;

            response.AddHeader("Content-Disposition", "attachment; filename=\"words.txt\"");
            response.Write(StorageManager.ProcessedText);
            response.End();
        }
    }
}