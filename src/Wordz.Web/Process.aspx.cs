using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wordz.BC;
using Wordz.Web.Controls;

namespace Wordz.Web {
    public partial class Process : Page {
        protected Progress ctlProgress;

        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack && Request.QueryString[BCCommon.LoadDataParam] != null) {
                txtSrc.Text = StorageManager.SourceText;
            }

            btnAddWords.HRef = "/AddWords/1";

            btnProcess.Attributes["onclick"] =
                string.Format("Process('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}');return false;",
                txtDest.ClientID,
                rbWordOrderEngRus.ClientID,
                rbWordOrderRusEng.ClientID,
                chkNotUseWellKnownWords.ClientID,
                chkNotUseNotKnownWords.ClientID,
                chkNotUseNotSoundedWords.ClientID,
                rbSortAlphabet.ClientID,
                rbSortFrequency.ClientID,
                rbSortWordsLength.ClientID,
                rbSortMixedOrder.ClientID,
                rbSortOriginalOrder.ClientID,
                txtMinFrequency.ClientID,
                txtMaxSignedWords.ClientID,
                lblWordCount.ClientID,
                lblMaxFrequency.ClientID,
                ctlProgress.ClientID);

            txtSrc.Attributes["onblur"] =
                string.Format("AddSourceText('{0}');return false;",
                txtSrc.ClientID);
        }

        #region Web Form Designer generated code

        protected override void OnInit(EventArgs e) {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent() {
            this.btnLoadFile.Click += new EventHandler(this.btnLoadFile_Click);
            this.Load += new EventHandler(this.Page_Load);
        }

        #endregion

        private void btnLoadFile_Click(object sender, EventArgs e) {
            if (ctlFile.PostedFile != null && ctlFile.PostedFile.ContentLength > 0) {
                StorageManager.UseSpecialWordSeparator = false;
                StorageManager.SourceText = txtSrc.Text =
                    BCCommon.GetFileContent(
                    ctlFile.PostedFile.InputStream, BCCommon.MaxTextLengthForProcess);
            }
        }
    }
}