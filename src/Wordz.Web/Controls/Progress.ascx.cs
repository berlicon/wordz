using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace Wordz.Web.Controls {
    public partial class Progress : UserControl {

        public new string ClientID {
            get { return divProgress.ClientID; }
        }

        protected void Page_Load(object sender, EventArgs e) {
            divProgress.Style["display"] = "none";
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