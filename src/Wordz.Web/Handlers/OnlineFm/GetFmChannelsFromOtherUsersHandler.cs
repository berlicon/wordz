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
    public class GetFmChannelsFromOtherUsersHandler : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            var accountId = StorageManager.UserLogined ? StorageManager.CurrentAccount.Id : BCCommon.AnonymousAccountId;
            var channels = BCCommon.GetOtherFmChannels(accountId, CurrentLanguage.NativeId, CurrentLanguage.LearnId);
            var returnObject = JsonHelper.JsonSerializer(channels);
            context.WriteSuccessAndExit(CurrentLanguage.Success, returnObject);
        }

        public bool IsReusable
        {
            get { return true; }
        }
    }
}