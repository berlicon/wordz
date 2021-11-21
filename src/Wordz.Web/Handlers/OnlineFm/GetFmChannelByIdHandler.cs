using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Wordz.BC;
using Wordz.Lng;
using Wordz.Web.Helpers;

namespace Wordz.Web.Handlers.OnlineFm
{
    public class GetFmChannelByIdHandler : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            int id;
            if (!int.TryParse(context.Request.QueryString["fmId"], out id))
            {
                context.WriteErrorAndExit(CurrentLanguage.IncorrectParams);
            }

            var fm = BCCommon.GetFmChannel(id, CurrentLanguage.NativeId);
            if (fm != null)
            {
                var retobj = JsonHelper.JsonSerializer(fm);
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