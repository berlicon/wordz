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
    public class ApproveCourseHandler : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            context.WriteErrorIfNotLoggedAndExit();

            int accountId = StorageManager.CurrentAccountId;
            int courseId;

            if (!int.TryParse(context.Request.QueryString["courseId"], out courseId))
            {
                context.WriteErrorAndExit(CurrentLanguage.IncorrectParams);
            }

            int result = CoursesRelatedAccessor.CourseApprove(accountId, courseId);
            
            if (result < 0)
            {
				context.WriteErrorAndExit(CurrentLanguage.CantAproveCourse);
            }

			context.WriteSuccessAndExit(CurrentLanguage.AproveCourseSuccessful);
        }

        public bool IsReusable
        {
            get { return true; }
        }
    }
}