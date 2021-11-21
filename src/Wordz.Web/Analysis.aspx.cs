using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wordz.BC;
using Wordz.Lng;
using Wordz.Web.Controls;

namespace Wordz.Web {
    public partial class Analysis : Page {
        protected Progress ctlProgress;

        protected void Page_Load(object sender, EventArgs e) {
            spanUnknownWord.Attributes["class"] = BCCommon.UnknownWordClass;
            spanKnownWord.Attributes["class"] = BCCommon.KnownWordClass;

            btnMakeAllUnknown.Attributes["onclick"] = string.Format(
                "MakeAllWords('{0}','{1}')",
                cklWords.ClientID,
                false);

            btnMakeAllKnown.Attributes["onclick"] = string.Format(
                "MakeAllWords('{0}','{1}')",
                cklWords.ClientID,
                true);

            btnAnalysis.Attributes["onclick"] =
                string.Format("Analysis('{0}','{1}');return false;",
                cklWords.ClientID,
                ctlProgress.ClientID);

            btnUpdateVocabulary.Attributes["onclick"] =
                string.Format("if (confirm('{0}')) UpdateVocabulary('{1}','{2}','{3}');return false;",
                CurrentLanguage.MessageAreYouSureWantDeleteAndAddWords,
                cklWords.ClientID,
                CurrentLanguage.MessageRegisteringRequiredForUpdateVocabulary,
                ctlProgress.ClientID);

            btnProcessUnknownWords.Attributes["onclick"] =
                string.Format("ProcessUnknownWords('{0}');return false;",
                cklWords.ClientID);
        }

        #region Web Form Designer generated code

        protected override void OnInit(EventArgs e) {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent() {
            this.Load += new EventHandler(this.Page_Load);
            this.btnLoadFile.Click += new EventHandler(btnLoadFile_Click);
        }

        #endregion

        private void btnLoadFile_Click(object sender, EventArgs e) {
            if (ctlFile.PostedFile != null && ctlFile.PostedFile.ContentLength > 0) {
                StorageManager.UseSpecialWordSeparator = true;
                StorageManager.SourceText = BCCommon.GetFileContent(
                    ctlFile.PostedFile.InputStream, BCCommon.MaxTextLengthForAnalysis);
                bool addCutHeaderInfo = 
                    (ctlFile.PostedFile.InputStream.Length >= BCCommon.MaxTextLengthForAnalysis);
                cklWords.InnerHtml = new BCCommon().GetTextAsHTMLForAnalysis(
                    StorageManager.SourceText, StorageManager.CurrentAccountId,
                    StorageManager.LanguageNativeId, StorageManager.LanguageLearnId,
                    addCutHeaderInfo);
            }
        }
    }
}