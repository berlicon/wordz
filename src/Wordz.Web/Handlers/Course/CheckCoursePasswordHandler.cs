using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Wordz.BC;
using Wordz.DB;
using Wordz.Web.Helpers;
using Wordz.Lng;

namespace Wordz.Web.Handlers.Course
{
    public class CheckCoursePasswordHandler : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            context.WriteErrorIfNotLoggedAndExit();

            var accountId = StorageManager.CurrentAccountId;
            int courseId;
            if (!int.TryParse(context.Request.Params["courseId"], out courseId))
            {
				context.WriteErrorAndExit(CurrentLanguage.IncorrectParams);
            }
            string password = context.Request.Params["password"];

            var result = BCCommon.CheckAndUpdatePasswordForCource(accountId, courseId, password);
            if (result == 0)
            {
                context.WriteSuccessAndExit("OK!");
            }
            else
            {
				context.WriteErrorAndExit(CurrentLanguage.WrongPassword);
            }
        }

        public bool IsReusable
        {
            get { return true; }
        }
    }
}