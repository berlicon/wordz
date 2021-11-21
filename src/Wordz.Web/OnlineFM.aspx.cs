using System;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wordz.BC;
using Wordz.BE;
using Wordz.Lng;
using Wordz.Web.Util;

namespace Wordz.Web
{
    public partial class OnlineFM : Page
    {

        #region Web Form Designer generated code

        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.Load += new EventHandler(this.OnlineFM_Load);
        }

        #endregion

        private const string BaseFmImagesPath = "/img/FmChannels/";

        private const string contentMediaPlayerPattern = @"
<object id='MediaPlayer' type='application/x-oleobject'
classid='CLSID:6BF52A52-394A-11d3-B153-00C04F79FAA6'
height='100%' width='100%'>
<param name='URL' value='{0}'>
<param name='SendPlayStateChangeEvents' value='True'>
<param name='AutoStart' value='True'>
<param name='ShowStatusBar' value='True'>
<param name='ShowAudioControls' value='True'>
<param name='ShowControls' value='true'>
<param name='ShowPositionControls' value='True'>
<param name='uiMode' value='mini'>
<param name='PlayCount' value='9999'>
<param name='stretchToFit' value='True'>
<param name='enableContextMenu' value='False'>
<embed type='application/x-mplayer2'
pluginspage='http://www.microsoft.com/downloads/details.aspx?FamilyID=9194bb10-00ea-4500-859d-9a319738d4d8&amp;DisplayLang=en/'
src='{0}'
name='player'
showcontrols='1'
showdisplay='0'
showstatusbar='1'
animationatstart='1'
transparentatstart='0'
allowchangedisplaysize='1'
autosize='0'
displaysize='0'
enablecontextmenu='0'
windowless='1'
enablefullscreencontrols='1'
height='100%'
width='100%'>
</object>";

        private const string contentRealPlayerPattern = @"
<object classid='clsid:CFCDAA03-8BE4-11cf-B84B-0020AFBBCCFA'
id='MediaPlayer' height='100%' width='100%'>
<param name='_ExtentX' value='5345'>
<param name='_ExtentY' value='1058'>
<param name='AUTOSTART' value='-1'>
<param name='SHUFFLE' value='0'>
<param name='PREFETCH' value='0'>
<param name='NOLABELS' value='0'>
<param name='SRC' value='{0}'>
<param name='CONTROLS' value='ControlPanel,StatusField'>
<param name='LOOP' value='0'>
<param name='NUMLOOP' value='0'>
<param name='CENTER' value='0'>
<param name='MAINTAINASPECT' value='0'>
<param name='BACKGROUNDCOLOR' value='#000000'>
<embed src='{0}'
name='MediaPlayer'
type='audio/x-pn-realaudio-plugin'
transparentatstart='0'
autostart='true'
animationatstart='0'
controls='ControlPanel,StatusField'
autosize='true'
displaysize='0'
height='100%'
width='100%'>
</object>";

        private void SetChannel(Fm fm)
        {
            lblChannelInfo.Text = fm.Description;
            string contentPattern = (fm.UseMediaPlayer)
                ? contentMediaPlayerPattern
                : contentRealPlayerPattern;
            divContent.InnerHtml = string.Format(contentPattern, fm.Url);
            PageUtil.RegisterScript(this,
                string.Format("document.title=\"{0}\"", fm.Description));
        }

        private void OnlineFM_Load(object sender, EventArgs e)
        {
            if (!StorageManager.UserLogined)
            {
                editChannelsBtn.Visible = false;
            }

            var accountId = StorageManager.UserLogined ? StorageManager.CurrentAccountId : BCCommon.AnonymousAccountId;

            myAccountId.Value = accountId.ToString();
            baseFmImagePath.InnerHtml = BaseFmImagesPath;

            var channels = BCCommon.GetFmChannels(
                StorageManager.UserLogined
                    ? StorageManager.CurrentAccountId
                    : BCCommon.AnonymousAccountId,
                CurrentLanguage.NativeId, CurrentLanguage.LearnId);

            Array.Sort(channels, (a, b) => a.OrderInList.CompareTo(b.OrderInList));

            foreach (var channel in channels)
            {
                if (channel.AccountId == null)
                {
                    channel.ImageUrl = BaseFmImagesPath + channel.ImageUrl;
                }
            }

            dlChannels.DataSource = channels;
            dlChannels.DataBind();

            if (Request.QueryString["Id"] != null)
            {
                int id;
                if (int.TryParse(Request.QueryString["Id"], out id))
                {
                    var fm = BCCommon.GetFmChannel(id, CurrentLanguage.NativeId);
                    SetChannel(fm);
                }
                else
                {
                    Response.Redirect("/OnlineFm/");
                }
            }
            else
            {
                string defaultChannelNumber = ConfigurationSettings.AppSettings.Get("DefaultFmChannelNumber");
                if (defaultChannelNumber != null)
                {
                    int number = int.Parse(defaultChannelNumber);
                    if (((Fm[])dlChannels.DataSource).Length > number)
                    {
                        SetChannel(((Fm[])dlChannels.DataSource)[number]);
                    }
                }
            }
        }
    }
}