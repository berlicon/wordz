using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wordz.BC;
using Wordz.Lng;
using Wordz.Web.Controls;

namespace Wordz.Web {
    public partial class IrregularVerbs : Page {
        protected Progress ctlProgress;

        protected void Page_Load(object sender, EventArgs e) {
            lblInfo.Text = BCCommon.GetAccountVerbsInfo(StorageManager.CurrentAccountId);

            btnLoad.Attributes["onclick"] =
                string.Format("LoadVerbs('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}');return false;",
                cklWords.ClientID,
                txtWordCount.ClientID,
                rbLoadPopular.ClientID,
                rbShowForm1.ClientID,
                rbShowForm2.ClientID,
                rbShowForm3.ClientID,
                rbShowTranslate.ClientID,
                rbShowRandom.ClientID,
                rbShowAll.ClientID,
                rbSortForm1.ClientID,
                rbSortByType.ClientID,
                rbSortTranslate.ClientID,
                rbSortRandom.ClientID,
                chkNotUseWellKnownVerbs.ClientID,
                chkCheckAll.ClientID,
                ctlProgress.ClientID);

            chkCheckAll.Attributes["onclick"] = string.Format(
                "CheckAll('{0}','{1}')",
                chkCheckAll.ClientID,
                cklWords.ClientID);

            btnClear.Attributes["onclick"] =
                string.Format("if (confirm('{0}')) DeleteVerbs('{1}','{2}','{3}','{4}');return false;",
                              CurrentLanguage.MessageAreYouSureWantDeleteIrregularVerbs,
                cklWords.ClientID,
                lblInfo.ClientID,
                CurrentLanguage.MessageRegisteringRequiredForClearIrregularVerbs,
                ctlProgress.ClientID);

            btnUpdate.Attributes["onclick"] =
                string.Format("AddTestedVerbs('{0}','{1}','{2}','{3}','{4}');return false;",
                cklWords.ClientID,
                chkCheckAll.ClientID,
                lblInfo.ClientID,
                CurrentLanguage.MessageRegisteringRequiredForAddIrregularVerbs,
                ctlProgress.ClientID);

            btnCheck.Attributes["onclick"] =
                string.Format("TestVerbs('{0}','{1}');return false;",
                cklWords.ClientID,
                chkCheckAll.ClientID);
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