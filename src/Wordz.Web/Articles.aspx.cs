using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Wordz.BC;
using Wordz.Lng;
using Wordz.Web.Util;

namespace Wordz.Web {
    public partial class Articles : Page {
        protected void Page_Load(object sender, EventArgs e) {
            if (Request.Params["id"] != null) {
                int id = int.Parse(Request.Params["id"].ToString());
                string title;
                content.Text = BCCommon.GetArticleHTML(id, out title);
                PageUtil.RegisterScript(this,
                    string.Format("document.title='{0}'", title));

            } else {
                content.Text = BCCommon.GetArticlesListHTML(CurrentLanguage.NativeId, CurrentLanguage.LearnId);
            }
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