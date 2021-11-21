using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Wordz.DB;
using Wordz.Web.Helpers;
using Wordz.Lng;

namespace Wordz.Web.Handlers.OnlineTv
{
    public class DeleteTvChannelByIdHandler : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            context.WriteErrorIfNotLoggedAndExit();

            int channelId;
            if (int.TryParse(context.Request.QueryString["channelId"], out channelId))
            {
                var accountId = StorageManager.CurrentAccountId;
                var result = DBCommon.DeleteTvChannel(accountId, channelId);
                if (result < 0)
                {
                    context.WriteErrorAndExit(CurrentLanguage.CantDeleteChannel);
                }
                else
                {
                    context.WriteSuccessAndExit(CurrentLanguage.Success);
                }
            }
            else
            {
                context.WriteErrorAndExit(CurrentLanguage.IncorrectParams);
            }
        }

        public bool IsReusable
        {
            get { return true; }
        }
    }
}