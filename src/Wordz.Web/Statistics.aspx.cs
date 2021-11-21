using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Wordz.BC;

namespace Wordz.Web {
    public partial class Statistics : Page {

        protected void Page_Load(object sender, EventArgs e) {
            //Version = xxx.yyy.zzz
            //xxx = 1; yyy = changelists count; zzz = number accounts on site
            //xxx.yyy - define in on page HTML
            lblSiteVersion.Text = BCCommon.GetAccountsCount().ToString();

            rptUsersByVocabulary.DataSource = BCCommon.GetTOPUsersByVocabulary(StorageManager.LanguageLearnId);
            rptUsersByVocabulary.DataBind();

            rptUsersByVerbs.DataSource = BCCommon.GetTOPUsersByVerbs();
            rptUsersByVerbs.DataBind();

            rptUsersByDictionaryQuiz.DataSource = BCCommon.GetTOPUsersByDictionaryQuiz(StorageManager.LanguageLearnId);
            rptUsersByDictionaryQuiz.DataBind();

			rptUsersByWordsOrderQuiz.DataSource = BCCommon.GetTOPUsersByWordsOrderQuiz(StorageManager.LanguageLearnId);
			rptUsersByWordsOrderQuiz.DataBind();
		
			rptUsersByLevelQuiz.DataSource = BCCommon.GetTOPUsersByLevelQuiz(StorageManager.LanguageLearnId);
			rptUsersByLevelQuiz.DataBind();
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