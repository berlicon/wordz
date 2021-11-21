using System.Collections.Specialized;
using System.Web;
using System.Web.SessionState;
using Wordz.BC;
using Wordz.DB.Accessors;
using Wordz.Web.Helpers;
using Wordz.Lng;

namespace Wordz.Web.Handlers.Exercise
{
    public class ExerciseSelectTextAddHandler : IHttpHandler, IRequiresSessionState
    {
        private HttpRequest Request { get; set; }
        private HttpResponse Response { get; set; }

        public void ProcessRequest(HttpContext context)
        {
            context.WriteErrorIfNotLoggedAndExit();

            Request = context.Request;
            Response = context.Response;

            var account = StorageManager.CurrentAccount;

            var exercise = GetExerciseFromParams(Request.Form);
            if (exercise != null)
            {
                var accountId = StorageManager.UserLogined ? StorageManager.CurrentAccount.Id : BCCommon.AnonymousAccountId;
                if (exercise.Id != 0)
                {
                    exercise.Id = ExerciseRelatedAccessor.UpdateExerciseSelectText(exercise, accountId);
                    context.WriteSuccessAndExit("Update", JsonHelper.JsonSerializer(exercise));
                }
                else
                {
                    exercise = ExerciseRelatedAccessor.AddExerciseSelectText(exercise, accountId);
                    context.WriteSuccessAndExit("Save", JsonHelper.JsonSerializer(exercise));
                }
            }
            else
            {
                context.WriteErrorAndExit(CurrentLanguage.Errors);
            }
        }

        private static BE.ExerciseSelectText GetExerciseFromParams(NameValueCollection collection)
        {
            int exerciseId;
            if (!int.TryParse(collection["exerciseSelectTextExercise"], out exerciseId))
            {
                return null;
            }
            int moduleId;
            if (!int.TryParse(collection["exerciseSelectTextExerciseModule"], out moduleId))
            {
                return null;
            }
            var exercise = new BE.ExerciseSelectText
            {
                Id = exerciseId,
                Name = collection["exerciseSelectTextName"],
                Description = collection["exerciseSelectTextDescription"],
                Text = collection["exerciseSelectTextQuestionText"],
                ModuleId = moduleId
            };
            return exercise;
        }

        public bool IsReusable
        {
            get { return false; }
        }
    }
}