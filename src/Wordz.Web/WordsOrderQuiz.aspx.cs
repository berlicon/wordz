using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wordz.BE;
using Wordz.Lng;
using Wordz.Web.Controls;

namespace Wordz.Web {
    public partial class WordsOrderQuiz : Page {
        protected Progress ctlProgress;

        protected void Page_Load(object sender, EventArgs e) {
            litTestsCount.Text = Constants.WordsOrderQuizTestsCount.ToString();
            btnStartExamAgain.Attributes["onclick"] =
                "location.href='/WordsOrderQuiz';return false;";

            btnStartExam.Attributes["onclick"] =
                string.Format("gt('{0}').style.display='';LoadWordsOrderQuiz('{1}','{2}','{3}');return false;",
                tblButtons.ClientID,
                tdExam.ClientID,
                hdnStep.ClientID,
                ctlProgress.ClientID);

            btnNext.Attributes["onclick"] =
                string.Format("TryFinishWordsOrderQuiz('{0}','{1}','{2}','{3}','{4}','{5}',{6},'{7}','{8}','{9}');return false;",
                tdExam.ClientID,
                Constants.MainDivId,
                tblButtons.ClientID,
                hdnStep.ClientID,
                hdnSuccessCount.ClientID,
                ctlProgress.ClientID,
                Constants.WordsOrderQuizTestsCount,
                StorageManager.UserLogined,
                hdnErrorInfo.ClientID,
                CurrentLanguage.MessageEnterNick
                );

            btnAbortExam.Attributes["onclick"] =
                string.Format("gt('{3}').value={6};TryFinishWordsOrderQuiz('{0}','{1}','{2}','{3}','{4}','{5}',{6},'{7}','{8}','{9}');return false;",
                tdExam.ClientID,
                Constants.MainDivId,
                tblButtons.ClientID,
                hdnStep.ClientID,
                hdnSuccessCount.ClientID,
                ctlProgress.ClientID,
                Constants.WordsOrderQuizTestsCount,
                StorageManager.UserLogined,
                hdnErrorInfo.ClientID,
                CurrentLanguage.MessageEnterNick
                );
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