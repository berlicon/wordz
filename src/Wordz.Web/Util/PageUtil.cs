using System;
using System.Web.UI;

namespace Wordz.Web.Util {
    public class PageUtil {
        private PageUtil() {}

        public static void RegisterAlert(Page page, string body) {
            page.RegisterStartupScript(Guid.NewGuid().ToString(),
                string.Format("<script language=javascript>alert('{0}');</script>", body));
        }

        public static void RegisterScript(Page page, string body) {
            page.RegisterStartupScript(Guid.NewGuid().ToString(),
                string.Format("<script language=javascript>{0}</script>", body));
        }
    }
}