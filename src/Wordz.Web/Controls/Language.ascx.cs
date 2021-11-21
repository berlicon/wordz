using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wordz.BC;
using Wordz.BE;

namespace Wordz.Web.Controls {
    public partial class Language : UserControl {

        private bool hideNative = false;
        private string learnText = "";
        private string nativeText = "";

        public bool HideNative {
            get { return hideNative; }
            set { hideNative = value; }
        }

        public string LearnText {
            get { return learnText; }
            set { learnText = value; }
        }

        public string NativeText {
            get { return nativeText; }
            set { nativeText = value; }
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

        protected void Page_Load(object sender, EventArgs e) {
            IdName[] allLanguages = BCCommon.GetLanguages();

            ddlLanguageNative.DataSource = ddlLanguageLearn.DataSource = allLanguages;
            ddlLanguageNative.DataValueField = ddlLanguageLearn.DataValueField = IdName.IdField;
            ddlLanguageNative.DataTextField = ddlLanguageLearn.DataTextField = IdName.NameField;
            ddlLanguageNative.DataBind();
            ddlLanguageLearn.DataBind();
            ddlLanguageNative.SelectedIndex = BCCommon.GetLanguageIndex(
                allLanguages, StorageManager.LanguageNativeId);
            ddlLanguageLearn.SelectedIndex = BCCommon.GetLanguageIndex(
                allLanguages, StorageManager.LanguageLearnId);

            ddlLanguageNative.Attributes["onchange"] =
                ddlLanguageLearn.Attributes["onchange"] =
                string.Format("ChangeLanguage('{0}','{1}');return false;",
                ddlLanguageNative.ClientID,
                ddlLanguageLearn.ClientID);

            pnlNative.Style["display"] = HideNative ? "none" : "";
            if (LearnText.Length > 0) {
                lblLearnText.InnerText = LearnText;
            }
            if (NativeText.Length > 0) {
                lblNativeText.InnerText = NativeText;
            }
        }
    }
}