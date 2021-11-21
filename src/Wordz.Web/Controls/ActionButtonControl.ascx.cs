using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wordz.Web.Controls
{
    public partial class PaymentButtonControl : System.Web.UI.UserControl
    {
        public string Text { get; set; }
        public string ConfirmText { get; set; }
        public string OnClickHandler { get; set; }
        public string Style { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}