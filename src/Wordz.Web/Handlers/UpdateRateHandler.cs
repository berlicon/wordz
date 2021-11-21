using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Wordz.DB;
using Wordz.DB.Accessors;
using Wordz.Lng;

namespace Wordz.Web.Handlers
{
    public class UpdateRateHandler : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            if (!StorageManager.UserLogined)
            {
                context.Response.Write(string.Format("{{'status': 'ERR','msg': '{0}'}}", CurrentLanguage.YouAreNotLoggedIn));
                context.Response.End();
                return;
            }
            
            var request = context.Request;
            var accountId = StorageManager.CurrentAccount.Id;
            Guid targetElement;
            double value;

            if (!Guid.TryParse(request.Params["target_element"].ToString(), out targetElement))
            {
                context.Response.Write(string.Format("{{'status': 'ERR','msg': '{0}'}}", CurrentLanguage.IncorrectParams));
                context.Response.End();
                return;
            }
            if (!double.TryParse(request.Params["val"], 
                NumberStyles.Float, 
                new NumberFormatInfo(){NumberDecimalSeparator = "."}, 
                out value))
            {
				context.Response.Write(string.Format("{{'status': 'ERR','msg': '{0}'}}", CurrentLanguage.IncorrectParams));
                context.Response.End();
                return;
            }

            RatingsRelatedAccessor.UpdateRating(accountId, targetElement, value);
			context.Response.Write(string.Format("{{'status': 'OK','msg': '{0}'}}", CurrentLanguage.YourVoteWritten));
        }

        public bool IsReusable
        {
            get { return true; }
        }
    }
}