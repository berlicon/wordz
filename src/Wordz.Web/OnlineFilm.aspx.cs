using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wordz.BC;
using Wordz.BE;
using Wordz.Lng;
using Wordz.Web.Controls;

namespace Wordz.Web {
    public partial class OnlineFilm : Page {
        protected Progress ctlProgress;

        private int _accountId = 0;

        #region Web Form Designer generated code

        protected override void OnInit(EventArgs e) {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent() {
            this.Load += new EventHandler(this.OnlineFilm_Load);
            this.rptCategories.ItemDataBound += new RepeaterItemEventHandler(rptCategories_ItemDataBound);
			//this.btnOK.Click += new EventHandler(btnOK_Click);
			//this.btnBad.Click += new EventHandler(btnBad_Click);
		}

		//void btnBad_Click(object sender, EventArgs e)
		//{
		//    BCCommon.UpdateFilmAvailability(int.Parse(Request.QueryString["Id"]), false);
		//}

		//void btnOK_Click(object sender, EventArgs e)
		//{
		//    BCCommon.UpdateFilmAvailability(int.Parse(Request.QueryString["Id"]), true);
		//}

        #endregion

        private void OnlineFilm_Load(object sender, EventArgs e)
        {
            _accountId = StorageManager.UserLogined ? StorageManager.CurrentAccountId : BCCommon.AnonymousAccountId;

            if (!IsPostBack) {
                rptCategories.DataSource = BCCommon.GetFilmCategories(CurrentLanguage.NativeId); 
                rptCategories.DataBind();

                lblInfo.Text = CurrentLanguage.MessageALotFilms;
                btnSearch.Attributes["onclick"] =
                    string.Format("SearchFilm(0,'{0}','{1}','{2}','{3}');return false;",
                    txtSearch.ClientID,
                    divContent.ClientID,
                    lblInfo.ClientID,
                    ctlProgress.ClientID);

                btnSetFilmListMode.Attributes["onclick"] =
                    string.Format("SetFilmMode('{0}','{1}','{2}','{3}');return false;",
                    false,
                    divContent.ClientID,
                    lblInfo.ClientID,
                    ctlProgress.ClientID);

                btnSetFilmIconMode.Attributes["onclick"] =
                    string.Format("SetFilmMode('{0}','{1}','{2}','{3}');return false;",
                    true,
                    divContent.ClientID,
                    lblInfo.ClientID,
                    ctlProgress.ClientID);

                StorageManager.FoundedFilms = BCCommon.GetFilmsByCategory(_accountId, 0, CurrentLanguage.NativeId, CurrentLanguage.LearnId);
					divContent.InnerHtml = BCCommon.GetFilmsListHTML(StorageManager.FoundedFilms, StorageManager.FilmIconMode);
            }

            int filmId;
            var filmIdParamStr = Request.QueryString["Id"];
            if (filmIdParamStr == null || !int.TryParse(filmIdParamStr, out filmId))
            {
                filmId = 0;
            }

            int part;
            var partParamStr = Request.QueryString["Part"];
            if (partParamStr == null || !int.TryParse(partParamStr, out part))
            {
                part = 1;
            }

            if (filmId > 0) {
                Film film = BCCommon.GetFilm(filmId, StorageManager.LanguageNativeId);
                lblInfo.Text = film.GetFilmTitleHtml(part);

                string content = string.Empty;

                if (string.IsNullOrEmpty(film.PlayerCode))
                {
                    content = string.Format(film.PlayerPattern,
                                            (part == 1) ? film.Url : film.PartUrls[part - 2].ToString());

                    //TODO: Hack change few params in tags...
                    //on site http://www.film-online.us in "Source View" html code
                    //different than if you download this code through TeleportPro...
                    try
                    {
                        string tppabs = "tppabs=\"";
                        int indexTppabsStart = content.IndexOf(tppabs);
                        int indexTppabsEnd = content.IndexOf("\"", indexTppabsStart + tppabs.Length);
                        string tppabsFull = content.Substring(indexTppabsStart + tppabs.Length - 1,
                                                              2 + indexTppabsEnd - indexTppabsStart - tppabs.Length);
                        content = content.Remove(indexTppabsStart, 1 + indexTppabsEnd - indexTppabsStart);

                        string src = "src=\"";
                        int indexSrcStart = content.IndexOf(src);
                        int indexSrcEnd = content.IndexOf("\"", indexSrcStart + src.Length);
                        string srcFull = content.Substring(indexSrcStart, 1 + indexSrcEnd - indexSrcStart);

                        string value = "value=\"";
                        int indexValueStart = content.IndexOf(value);
                        int indexValueEnd = content.IndexOf("\"", indexValueStart + value.Length);
                        string valueFull = content.Substring(indexValueStart, 1 + indexValueEnd - indexValueStart);

                        content = content.Replace(srcFull, "src=" + tppabsFull);
                        content = content.Replace(valueFull, "value=" + tppabsFull);
                    }
                    catch
                    {
                    }
                    //-------------- end hack ---------------
                }
                else
                {
                    content = film.PlayerCode;
                }
                divContent.InnerHtml = content;
            }

            initControls();
        }

        private void initControls()
        {
            if (!StorageManager.UserLogined)
            {
                editChannelsBtn.Visible = false;
            }

            myAccountId.Value = _accountId.ToString();
        }

        private void rptCategories_ItemDataBound(object sender, RepeaterItemEventArgs e) {
            if (e.Item.DataItem != null) {
                IdName category = (IdName)e.Item.DataItem;
                HtmlAnchor hypFilm = (HtmlAnchor)e.Item.FindControl("hypFilm"); 
                hypFilm.InnerText = category.Name;
                hypFilm.Attributes["onclick"] =
                    string.Format("SearchFilm({0},'{1}','{2}','{3}','{4}');return false;",
                    category.Id,
                    txtSearch.ClientID,
                    divContent.ClientID,
                    lblInfo.ClientID,
                    ctlProgress.ClientID);
            }
        }
    }
}