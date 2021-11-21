using System;
using System.Web;
using System.Web.SessionState;
using Wordz.BC;
using Wordz.BE;

namespace Wordz.Web.Handlers {
    public class LoginHandler : IHttpHandler, IRequiresSessionState {
        public bool IsReusable {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context) {
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;

            string nick = request.QueryString["Nick"];
            string password = request.QueryString["Password"];

            Account account = BCCommon.GetAccount(nick.Trim(), password.Trim());
            if (!account.IsEmpty) {
                StorageManager.CurrentAccount = account;

				HttpCookie myCookie = new HttpCookie("WordzCookie");
				myCookie.Values["nick"] = nick;
				myCookie.Values["password"] = password;
				myCookie.Expires = DateTime.Now.AddYears(1);
				response.Cookies.Add(myCookie);
            }

            response.Write(account.Nick);
            response.End();
        }
    }
}