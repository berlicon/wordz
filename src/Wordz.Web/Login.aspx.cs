using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wordz.Lng;

namespace Wordz.Web {
    public partial class Login : Page {

        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                if (StorageManager.UserLogined) {
                    txtNick.Text = StorageManager.CurrentAccount.Nick;
                    txtPassword.Text = StorageManager.CurrentAccount.Password;
                    txtEmail.Text = StorageManager.CurrentAccount.Email;
                }
            }

            btnSave.Attributes["onclick"] =
                string.Format("AccountSave('{0}','{1}','{2}','{3}');return false;",
                txtNick.ClientID,
                txtPassword.ClientID,
                txtEmail.ClientID,
                              CurrentLanguage.MessageNickRequired);
        }

        #region Web Form Designer generated code

        protected override void OnInit(EventArgs e) {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent() {
            this.Load += new EventHandler(this.Page_Load);
        }

        #endregion
    }
}