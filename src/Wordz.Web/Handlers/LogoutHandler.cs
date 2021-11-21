using System;
using System.Web;
using System.Web.SessionState;

namespace Wordz.Web.Handlers {
    public class LogoutHandler : IHttpHandler, IRequiresSessionState {
        public bool IsReusable {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context) {
            HttpResponse response = context.Response;
			StorageManager.CurrentAccount = null;

			HttpCookie myCookie = new HttpCookie("WordzCookie");
			myCookie.Values["nick"] = null;
			myCookie.Values["password"] = null;
			myCookie.Expires = DateTime.Now.AddYears(-10);
			response.Cookies.Add(myCookie);

            response.End();
        }
    }
}