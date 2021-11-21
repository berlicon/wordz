using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Wordz.DB.Accessors;
using Wordz.Web.Helpers;
using Wordz.Lng;

namespace Wordz.Web.Handlers.Module
{
    public class GetModuleByIdHandler : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            context.WriteErrorIfNotLoggedAndExit();

            var account = StorageManager.CurrentAccount;

            int moduleId;
            if (!int.TryParse(context.Request.QueryString["moduleId"], out moduleId))
            {
                context.WriteErrorAndExit(CurrentLanguage.IncorrectParams);
            }

            var module = CoursesRelatedAccessor.GetModuleWithExercisesById(account.Id, moduleId);
            if (module == null)
            {
                context.WriteErrorAndExit(CurrentLanguage.CantGetCourseById);
            }

            string serializedObject = string.Empty;
            try
            {
                serializedObject = JsonHelper.JsonSerializer(module);
            }
            catch (Exception)
            {
                context.WriteErrorAndExit(CurrentLanguage.CantGetObject);
            }

			context.WriteSuccessAndExit(CurrentLanguage.GetCourseSuccessful, serializedObject);
        }

        public bool IsReusable
        {
            get { return true; }
        }
    }
}