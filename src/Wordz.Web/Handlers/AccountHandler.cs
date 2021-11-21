using System;
using System.Web;
using System.Web.SessionState;
using Wordz.BC;
using Wordz.BE;

namespace Wordz.Web.Handlers {
    public class AccountHandler : IHttpHandler, IRequiresSessionState {
        public bool IsReusable {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context) {
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;

            string nick = request.QueryString["Nick"];
            string password = request.QueryString["Password"];
            string email = request.QueryString["Email"];

			if (nick.Trim().Length > 0) {
				Account workAccount = new Account(
					StorageManager.CurrentAccountId,
					nick.Trim(), email.Trim(), password.Trim());

				if (StorageManager.UserLogined) {
					bool successUpdate = BCCommon.UpdateAccount(workAccount);
					if (successUpdate) {
						StorageManager.CurrentAccount = workAccount;
						response.Write(BCCommon.OkText);
					}
				} else {
					Account account = BCCommon.AddAccount(workAccount);
					if (!account.IsEmpty) {
						StorageManager.CurrentAccount = account;
						response.Write(BCCommon.OkText);
					}
				}

				HttpCookie myCookie = new HttpCookie("WordzCookie");
				myCookie.Values["nick"] = nick;
				myCookie.Values["password"] = password;
				myCookie.Expires = DateTime.Now.AddYears(1);
				response.Cookies.Add(myCookie);
			}
            response.End();
        }
    }
}