using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wordz.BC;
using Wordz.Lng;
using Wordz.Web.Controls;

namespace Wordz.Web {
    public partial class Vocabulary : Page {
        protected Progress ctlProgress;

        protected void Page_Load(object sender, EventArgs e) {
            lblCount.Text = (StorageManager.UserLogined)
                ? BCCommon.GetVocabularyInfo(
                StorageManager.CurrentAccount.Id,
                StorageManager.LanguageLearnId,
                StorageManager.LanguageNativeId)
                : CurrentLanguage.MessageUnknown;

            chkCheckAll.Attributes["onclick"] = string.Format(
                "CheckAll('{0}','{1}')",
                chkCheckAll.ClientID,
                cklWords.ClientID);

            btnLoad.Attributes["onclick"] =
                string.Format("LoadMyWords('{0}','{1}','{2}');return false;",
                cklWords.ClientID,
                lblCount.ClientID,
                ctlProgress.ClientID);

            btnDelete.Attributes["onclick"] =
                string.Format("if (confirm('{0}')) DeleteWords('{1}','{2}','{3}','{4}');return false;",
                CurrentLanguage.MessageAreYouSureWantDeleteWords,
                cklWords.ClientID,
                chkCheckAll.ClientID,
                              CurrentLanguage.MessageRegisteringRequiredForDeleteWords,
                ctlProgress.ClientID);
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