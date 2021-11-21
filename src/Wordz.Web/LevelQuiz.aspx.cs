using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wordz.BE;
using Wordz.Lng;
using Wordz.Web.Controls;

namespace Wordz.Web
{
	public partial class LevelQuiz : System.Web.UI.Page
	{
		protected Progress ctlProgress;
		
		protected void Page_Load(object sender, EventArgs e) {
			litTestsCount.Text = Constants.LevelQuizTestsCount.ToString();
			btnStartExamAgain.Attributes["onclick"] =
				"location.href='/LevelQuiz';return false;";

			btnStartExam.Attributes["onclick"] =
				string.Format("gt('{0}').style.display='';LoadLevelQuiz('{1}','{2}','{3}');return false;",
				tblButtons.ClientID,
				tdExam.ClientID,
				hdnStep.ClientID,
				ctlProgress.ClientID);

			btnNext.Attributes["onclick"] =
				string.Format("TryFinishLevelQuiz('{0}','{1}','{2}','{3}','{4}','{5}',{6},'{7}','{8}','{9}');return false;",
				tdExam.ClientID,
				Constants.MainDivId,
				tblButtons.ClientID,
				hdnStep.ClientID,
				hdnSuccessCount.ClientID,
				ctlProgress.ClientID,
				Constants.LevelQuizTestsCount,
				StorageManager.UserLogined,
				hdnErrorInfo.ClientID,
				CurrentLanguage.MessageEnterNick
				);

			btnAbortExam.Attributes["onclick"] =
				string.Format("gt('{3}').value={6};TryFinishLevelQuiz('{0}','{1}','{2}','{3}','{4}','{5}',{6},'{7}','{8}','{9}');return false;",
				tdExam.ClientID,
				Constants.MainDivId,
				tblButtons.ClientID,
				hdnStep.ClientID,
				hdnSuccessCount.ClientID,
				ctlProgress.ClientID,
				Constants.LevelQuizTestsCount,
				StorageManager.UserLogined,
				hdnErrorInfo.ClientID,
				CurrentLanguage.MessageEnterNick
				);
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.Load += new EventHandler(this.Page_Load);
		}
		#endregion
	}
}