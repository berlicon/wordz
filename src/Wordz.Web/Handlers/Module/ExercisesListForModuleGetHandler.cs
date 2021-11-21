using System.Web;
using System.Web.SessionState;
using Wordz.BC;
using Wordz.DB.Accessors;
using Wordz.Web.Helpers;
using Wordz.Lng;

namespace Wordz.Web.Handlers.Module
{
    public class ExercisesListForModuleGetHandler : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            context.WriteErrorIfNotLoggedAndExit();

            int moduleId;
            if (!int.TryParse(context.Request.QueryString["moduleId"], out moduleId))
            {
                context.WriteErrorAndExit(CurrentLanguage.IncorrectParams);
            }
            var accountId = StorageManager.UserLogined ? StorageManager.CurrentAccount.Id : BCCommon.AnonymousAccountId;

            var details = ExerciseRelatedAccessor.GetExercisesBaseArray(accountId, moduleId);
            if (details == null)
            {
				context.WriteErrorAndExit(CurrentLanguage.CantGetExerciseListForModule);
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