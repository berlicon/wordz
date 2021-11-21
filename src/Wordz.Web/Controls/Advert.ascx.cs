using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Wordz.LngOpt;

namespace Wordz.Web.Controls {
    public partial class Advert : UserControl {

        private int ad;

        public int Ad {
            get { return ad; }
            set { ad = value; }
        }

        private const string ad_Begun = @"
<!-- Блок Блок №1, создан 29.08.09 -->
<div id='block-1251548285'></div>
<script type='text/javascript'>
var begun_auto_pad = 150156790; var begun_auto_limit = {0};  begun_multispan = 1;
var begun_spans = new Array(
{{'span_id': 'block-1251548285', 'limit': {0}, 'width': '100%', 'type': 'vertical'}}
);

var begun_config = {{
'title': 		{{ 'fontSize': '13px', 'color': '#0000CC', 'decoration': 'none', 'hoverColor': '#D50303', 'hoverDecoration': 'none' }},
'text': 		{{ 'fontSize': '12px', 'color': '#000000', 'decoration': 'none', 'hoverColor': '#000000', 'hoverDecoration': 'none' }},
'domain':		{{ 'fontSize': '10px', 'color': '#008200', 'decoration': 'none', 'hoverColor': '#A0522D', 'hoverDecoration': 'none' }},
'begun': 		{{ 'fontSize': '11px', 'color': '#A0522D', 'decoration': 'none', 'hoverColor': '#A0522D', 'hoverDecoration': 'none' }},
'record': 		{{ 'padding': '5px', 'bgColor': '#FFFFFF', 'border': '1px #EBEBEB solid', 'hoverBgColor': '#F5F5F5', 'hoverBorder': '1px #CCCCCC solid' }},
'block': 		{{ 'padding': '5px', 'bgColor': '#EBEBEB', 'border': '1px #EBEBEB solid', 'hoverBgColor': '#EBEBEB', 'hoverBorder': '1px #EBEBEB solid' }},
'thumb': 0, 'thumbWidth': 56, 'fontFamily': 'inherit'}};
</script>
<script type='text/javascript' src='http://xml.zorkabiz.ru/get_config.php?pad=150156790&code=10621'></script>
<script type='text/javascript'>
function begunSwitchBanners(hide)
{{
	for (var i = begun_spans.length, j = 0; j < i; j++)
	{{
		var o = document.getElementById(begun_spans[j].span_id);
		if (o)
			o.style.display = hide ? 'none' : 'block';
	}}
}}

begunSwitchBanners(1);
</script>
<script type='text/javascript' src='http://autocontext.begun.ru/autocontext.js'></script>
<script type='text/javascript' src='http://xml.zorkabiz.ru/zorkablocks.js'></script>
<script type='text/javascript'>
setTimeout('begunSwitchBanners(0)', 2000);
</script>";

		private const string ad_Google_728_15 = @"
<script type='text/javascript'><!--
google_ad_client = 'ca-pub-5672994286958659';
/* Links */
google_ad_slot = '6513115746';
google_ad_width = 728;
google_ad_height = 15;
//-->
</script>
<script type='text/javascript'
src='http://pagead2.googlesyndication.com/pagead/show_ads.js'>
</script>
";

		private const string ad_Google_200_200 = @"
<script type='text/javascript'><!--
google_ad_client = 'pub-5672994286958659';
/* 200x200, created 8/31/09 */
google_ad_slot = '2902697343';
google_ad_width = 200;
google_ad_height = 200;
//-->
</script>
<script type='text/javascript'
src='http://pagead2.googlesyndication.com/pagead/show_ads.js'>
</script>
";

        private const string ad_Google_180_150 = @"
<script type='text/javascript'><!--
google_ad_client = 'pub-5672994286958659';
/* 180x150, created 9/1/09 */
google_ad_slot = '1557254593';
google_ad_width = 180;
google_ad_height = 150;
//-->
</script>
<script type='text/javascript'
src='http://pagead2.googlesyndication.com/pagead/show_ads.js'>
</script>
";

        private const string ad_Google_160_600 = @"
<script type='text/javascript'><!--
google_ad_client = 'pub-5672994286958659';
/* 160x600, created 9/1/09 */
google_ad_slot = '4136654490';
google_ad_width = 160;
google_ad_height = 600;
//-->
</script>
<script type='text/javascript'
src='http://pagead2.googlesyndication.com/pagead/show_ads.js'>
</script>
";

        private string GetGoogleAds(int ad) {
            switch (ad) {
				case 1: return ad_Google_728_15;
				case 3: return ad_Google_200_200;
				case 4: return ad_Google_200_200 + "<br><br>" + ad_Google_180_150;
                case 7: return ad_Google_160_600;
                case 10: return ad_Google_160_600 + "<br><br>" + ad_Google_160_600
							 + "<br><br>" + ad_Google_160_600 + "<br><br>" + ad_Google_160_600
							 + "<br><br>" + ad_Google_160_600 + "<br><br>" + ad_Google_160_600;
                default: return ad_Google_200_200;
            }
        }

        protected void Page_Load(object sender, EventArgs e) {
            pnlAd.InnerHtml = /*(LanguageOptions.IsMainSite
                && DateTime.Now.Millisecond > 750)
                ? string.Format(ad_Begun, Ad) : */GetGoogleAds(Ad);
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