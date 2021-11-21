using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Wordz.BC;
using Wordz.Web.Helpers;
using Wordz.Lng;

namespace Wordz.Web.Handlers.UserComment
{
    public class RateOrClaimCommentHandler : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            context.WriteErrorIfNotLoggedAndExit();

            int commentId;
            if (!int.TryParse(context.Request.Params["commentId"], out commentId))
            {
                context.WriteErrorAndExit(CurrentLanguage.IncorrectParams);
            }

            var actionType = context.Request.Params["actionType"];
            var accountId = StorageManager.CurrentAccountId;
            int result;

            switch (actionType)
            {
                case "rateNegative":
                    result = BCCommon.RateUserComment(accountId, commentId, false);
                    context.WriteSuccessAndExit("Ok!", JsonHelper.JsonSerializer(result));
                    break;
                case "ratePositive":
                    result = BCCommon.RateUserComment(accountId, commentId, true);
                    context.WriteSuccessAndExit("Ok!", JsonHelper.JsonSerializer(result));
                    break;
                case "claim":
                    result = BCCommon.ClaimUserComment(accountId, commentId);
                    context.WriteSuccessAndExit("Ok!", JsonHelper.JsonSerializer(result));
                    break;
            }
        }

        public bool IsReusable
        {
            get { return false; }
        }
    }
}