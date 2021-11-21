using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wordz.BC;
using Wordz.BE;
using Wordz.BE.Dto;
using Wordz.Lng;
using Wordz.Web.Helpers;
using Wordz.Web.Util;

namespace Wordz.Web {
    public partial class OnlineTV : Page {

        private const string BaseTvImagesPath = "/img/TvChannels/";

        #region Web Form Designer generated code

        protected override void OnInit(EventArgs e) 
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent() 
        {
            this.Load += new EventHandler(this.OnlineTV_Load);
        }

        #endregion

        private void SetChannel(Tv tv)
        {
            onlineTvTitle.InnerText = tv.Name;
            lblChannelInfo.Text = tv.Description;
            divContent.InnerHtml = "<!--" + tv.Url + "-->";
            //PageUtil.RegisterScript(this,
            //    string.Format("document.title=\"{0}\"", tv.Description));
        }

        private void OnlineTV_Load(object sender, EventArgs e)
        {
            //editChannelsBtn.InnerHtml = RenderHelper.Button(
            //    "editChannelsBtnId",
            //    "Редактировать список каналов",
            //    "page.editTv()",
            //    "display: inline"
            //    );
            
            //editButtonsContainer.InnerHtml =
            //    RenderHelper.Button("editYesBtn",
            //                        "Сохранить",
            //                        null,
            //                        "display: inline")
            //    + "&nbsp;&nbsp;&nbsp;" +
            //    RenderHelper.Button("editNoBtn",
            //                        "Отмена",
            //                        null,
            //                        "display: inline");

            if (!StorageManager.UserLogined)
            {
                editChannelsBtn.Visible = false;
            }

            var accountId = StorageManager.UserLogined ? StorageManager.CurrentAccountId : BCCommon.AnonymousAccountId;

            myAccountId.Value = accountId.ToString();
            baseTvImagePath.InnerHtml = BaseTvImagesPath;

            var channels = BCCommon.GetTvChannels(accountId, CurrentLanguage.NativeId, CurrentLanguage.LearnId)
                .OrderBy(channel => channel.OrderInList)
                .ToArray();

            foreach (var channel in channels)
            {
                if (channel.AccountId == null)
                {
                    channel.ImageUrl = BaseTvImagesPath + channel.ImageUrl;
                }
            }

            dlChannels.DataSource = channels;
            dlChannels.DataBind();

            Action setToDefaultChannel = () =>
                                          {

                                              string defaultChannelNumber =
                                                  ConfigurationManager.AppSettings["DefaultTvChannelNumber"];
                                              if (defaultChannelNumber != null)
                                              {
                                                  int number = int.Parse(defaultChannelNumber);
                                                  if (((TvWithOrderDto[]) dlChannels.DataSource).Length > number)
                                                  {
                                                      SetChannel(((TvWithOrderDto[]) dlChannels.DataSource)[number]);
                                                  }
                                              }
                                          };

            int id;
            if (int.TryParse(Request.QueryString["Id"], out id)) 
            {
                Tv tv = BCCommon.GetTvChannel(id, CurrentLanguage.NativeId);
                if (tv != null)
                {
                    SetChannel(tv);
                }
                else
                {
                    Response.Redirect("/OnlineTv/");
                }
            }
            else
            {
                setToDefaultChannel();
            }
        }
    }
}