using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wordz.BE.Dto;
using Wordz.Lng;
using Wordz.BE;
using Wordz.BC;

namespace Wordz.Web.Controls {
    public partial class Login : UserControl {

        protected override void OnPreRender(EventArgs e) {
            base.OnPreRender(e);

            //TODO: !!!Delete this IF block!!!
//            if (!IsPostBack) {
//                txtNick.Text = "berk2";
//            }
            //--------------------------------
            AccountMoneyInfoDto[] moneyInfos = new AccountMoneyInfoDto[]{};

			try {
				HttpCookie myCookie = new HttpCookie("WordzCookie");
				myCookie = Request.Cookies["WordzCookie"];
				if (myCookie != null && myCookie.Values["nick"] != null && myCookie.Values["password"] != null ) {
					Account account = BCCommon.GetAccount(
						myCookie.Values["nick"],
						myCookie.Values["password"]);
					if (!account.IsEmpty)
					{
					    var prevAccount = StorageManager.CurrentAccount;
					    StorageManager.CurrentAccount = account;
					    moneyInfos = BCCommon.GetAccountMoneyInfos(account.Id);
                        if (prevAccount == null || (prevAccount.Id != account.Id))
                        {
                            redirectScriptId.Visible = true;
                        }
					}
				}
			} catch {}

            cashLabel.Text = string.Join(" | ",
                                         moneyInfos.Select(
                                             moneyInfo => moneyInfo.Currency.LetterCode + ": " + moneyInfo.Value));

            tblLogin.Style["display"] = !StorageManager.UserLogined ? "inline" : "none";
            tblLogout.Style["display"] = StorageManager.UserLogined ? "inline" : "none";
            lblNick.Text = (StorageManager.UserLogined)
                ? StorageManager.CurrentAccount.Nick : "";

            btnLogin.Attributes["onclick"] =
                string.Format("Login('{0}','{1}','{2}','{3}','{4}','{5}');return false;",
                tblLogin.ClientID,
                tblLogout.ClientID,
                txtNick.ClientID,
                txtPassword.ClientID,
                lblNick.ClientID,
                              CurrentLanguage.MessageWrongNickOrPassword);
            btnLogout.Attributes["onclick"] =
                string.Format("Logout('{0}','{1}');return false;",
                tblLogin.ClientID,
                tblLogout.ClientID);
        }
    }
}