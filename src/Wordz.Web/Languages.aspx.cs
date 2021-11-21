using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Wordz.BC;

namespace Wordz.Web {
    public partial class Languages : Page {

        protected void Page_Load(object sender, EventArgs e) {
            rptLanguage.DataSource = BCCommon.GetUsedLanguages();
            rptLanguage.DataBind();
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