using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wordz.BC;
using Wordz.Lng;
using Wordz.Web.Controls;
using Wordz.Web.Util;

namespace Wordz.Web {
    public partial class CheckWords : Page {
        protected Progress ctlProgress;

        protected void Page_Load(object sender, EventArgs e) {
            cklWordsOriginal.Style["display"] = "none";

            chkCheckAll.Attributes["onclick"] = string.Format(
                "CheckAll('{0}','{1}');CheckAll('{0}','{2}');",
                chkCheckAll.ClientID,
                cklWordsOriginal.ClientID,
                cklWordsTranslation.ClientID);

            btnChangePanel.Attributes["onclick"] = string.Format(
                "ChangePanel('{0}','{1}');return false;",
                cklWordsOriginal.ClientID,
                cklWordsTranslation.ClientID);

            btnSelect.Attributes["onclick"] =
                string.Format("CheckAllByParam('{0}','{1}','{2}');CheckAllByParam('{0}','{3}','{4}');return false;",
                txtMinPercent.ClientID,
                cklWordsOriginal.ClientID, "p",
                cklWordsTranslation.ClientID, "r");

            btnTest.Attributes["onclick"] =
                string.Format("TestWords('{0}','{1}');return false;",
                cklWordsOriginal.ClientID,
                cklWordsTranslation.ClientID);

            btnAdd.Attributes["onclick"] =
                string.Format("AddTestedWords('{0}','{1}','{2}','{3}','{4}');return false;",
                cklWordsOriginal.ClientID,
                cklWordsTranslation.ClientID,
                chkCheckAll.ClientID,
                              CurrentLanguage.MessageRegisteringRequiredForAddWords,
                ctlProgress.ClientID);
        }

        #region Web Form Designer generated code

        protected override void OnInit(EventArgs e) {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent() {
            this.Load += new EventHandler(this.Page_Load);
            this.btnLoadFile.Click +=new EventHandler(btnLoadFile_Click);
        }

        #endregion

        private void btnLoadFile_Click(object sender, EventArgs e) {
            if (ctlFile.PostedFile != null && ctlFile.PostedFile.ContentLength > 0) {
                StorageManager.SourceText =
                    BCCommon.GetFileContent(
                    ctlFile.PostedFile.InputStream, BCCommon.MaxTextLengthForProcess);
            }

            string script =
                string.Format("LoadWordsInfo('{0}','{1}','{2}','{3}');",
                cklWordsOriginal.ClientID,
                cklWordsTranslation.ClientID,
                chkNotUseWellKnownWords.ClientID,
                ctlProgress.ClientID);

            PageUtil.RegisterScript(this, script);
        }
    }
}