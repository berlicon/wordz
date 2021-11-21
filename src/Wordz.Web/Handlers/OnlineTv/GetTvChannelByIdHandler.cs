using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Wordz.BC;
using Wordz.Lng;
using Wordz.Web.Helpers;

namespace Wordz.Web.Handlers.OnlineTv
{
    public class GetTvChannelByIdHandler : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            int id;
            if (!int.TryParse(context.Request.QueryString["tvId"], out id))
            {
                context.WriteErrorAndExit(CurrentLanguage.IncorrectParams);
            }

            var tv = BCCommon.GetTvChannel(id, CurrentLanguage.NativeId);
            if (tv != null)
            {
                var retobj = JsonHelper.JsonSerializer(tv);
                context.WriteSuccessAndExit(CurrentLanguage.Success, retobj);
            }
            else
            {
                context.WriteErrorAndExit(CurrentLanguage.CantGetObject);
            }
        }

        public bool IsReusable
        {
            get { return true; }
        }
    }
}