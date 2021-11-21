using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wordz.BC;
using Wordz.BE;
using Wordz.Lng;
using Wordz.Web.Controls;
using Wordz.Web.Util;

namespace Wordz.Web {
    public partial class AddWords : Page {
        protected Progress ctlProgress;

        protected void Page_Load(object sender, EventArgs e) {
            btnBackToProcess.HRef = "/Process/1";

            chkCheckAll.Attributes["onclick"] = string.Format(
                "CheckAll('{0}','{1}')",
                chkCheckAll.ClientID,
                cklWords.ClientID);

            btnAdd.Attributes["onclick"] =
                string.Format("AddWords('{0}','{1}','{2}','{3}');return false;",
                cklWords.ClientID,
                chkCheckAll.ClientID,
                CurrentLanguage.MessageRegisteringRequiredForAddWords,
                ctlProgress.ClientID);

            btnLoad.Attributes["onclick"] =
                string.Format("LoadWords('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}');return false;",
                cklWords.ClientID,
                rbLoadFromGlobalDictionary.ClientID,
                rbSelectForDomain.ClientID,
                ddlDomain.ClientID,
                rbSelectRandom.ClientID,
                rbSelectOrdered.ClientID,
                txtWordCount.ClientID,
                txtWordStartIndex.ClientID,
                chkCheckAll.ClientID,
                ctlProgress.ClientID);

            string changeControlsStateScript = 
                string.Format("ChangeControlsState('{0}','{1}','{2}','{3}','{4}','{5}','{6}');",
                rbLoadFromProcessPage.ClientID,
                rbSelectForDomain.ClientID,
                ddlDomain.ClientID,
                rbSelectRandom.ClientID,
                rbSelectOrdered.ClientID,
                txtWordCount.ClientID,
                txtWordStartIndex.ClientID);
            
            rbLoadFromGlobalDictionary.Attributes["onclick"] =
                rbLoadFromProcessPage.Attributes["onclick"] =
                changeControlsStateScript;

            ddlDomain.DataValueField = IdName.IdField;
            ddlDomain.DataTextField = IdName.NameField;
            ddlDomain.DataSource = BCCommon.GetDomains(StorageManager.LanguageNativeId);
            ddlDomain.DataBind();

            if (!IsPostBack && Request.QueryString[BCCommon.UseProcessedWordsParam] != null) {
                cklWords.InnerHtml = BCCommon.GetCheckedWordsHTML(
                    StorageManager.ProcessedWordIds);
                rbLoadFromProcessPage.Checked = true;
            }

            PageUtil.RegisterScript(this, changeControlsStateScript);
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