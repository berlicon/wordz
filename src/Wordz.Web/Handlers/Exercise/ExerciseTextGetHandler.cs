using System.Web;
using System.Web.SessionState;
using Wordz.BC;
using Wordz.DB.Accessors;
using Wordz.Web.Helpers;
using Wordz.Lng;

namespace Wordz.Web.Handlers.Exercise
{
    public class ExerciseTextGetHandler : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            context.WriteErrorIfNotLoggedAndExit();

            int exerciseId;
            if (!int.TryParse(context.Request.QueryString["id"], out exerciseId))
            {
                context.WriteErrorAndExit(CurrentLanguage.IncorrectParams);
            }
            var accountId = StorageManager.UserLogined ? StorageManager.CurrentAccount.Id : BCCommon.AnonymousAccountId;

            var details = ExerciseRelatedAccessor.GetExerciseTextById(exerciseId, accountId);
            if (details == null)
            {
                context.WriteErrorAndExit(CurrentLanguage.CantGetExerciseById);
            }
            context.Response.Write(JsonHelper.JsonSerializer(details));
        }

        public bool IsReusable
        {
            get { return true; }
        }
    }
}