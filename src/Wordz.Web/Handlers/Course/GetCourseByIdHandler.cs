using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Wordz.DB.Accessors;
using Wordz.Web.Helpers;
using Wordz.Lng;

namespace Wordz.Web.Handlers.Course
{
    public class GetCourseByIdHandler : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            context.WriteErrorIfNotLoggedAndExit();

            var account = StorageManager.CurrentAccount;

            int courseId;
            if (!int.TryParse(context.Request.QueryString["id"], out courseId))
            {
                context.WriteErrorAndExit(CurrentLanguage.IncorrectParams);
            }

            var details = CoursesRelatedAccessor.GetCourseDetailsWithModulesById(account.Id, courseId);
            if (details == null)
            {
                context.WriteErrorAndExit(CurrentLanguage.CantGetCourseById);
            }
            context.NoCache();
            context.Response.Write(JsonHelper.JsonSerializer(details));
        }

        public bool IsReusable
        {
            get { return true; }
        }
    }
}