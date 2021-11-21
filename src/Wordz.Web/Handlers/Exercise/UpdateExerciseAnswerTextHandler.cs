using System.Collections.Specialized;
using System.Web;
using System.Web.SessionState;
using Wordz.BC;
using Wordz.DB.Accessors;
using Wordz.Web.Helpers;
using Wordz.Lng;

namespace Wordz.Web.Handlers.Exercise
{
    public class UpdateExerciseAnswerTextHandler : IHttpHandler, IRequiresSessionState
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
                    exercise.Id = ExerciseRelatedAccessor.UpdateExerciseAnswerText(exercise, accountId);
                    context.WriteSuccessAndExit("Update", JsonHelper.JsonSerializer(exercise));
                }
                else
                {
                    exercise = ExerciseRelatedAccessor.AddExerciseAnswerText(exercise, accountId);
                    context.WriteSuccessAndExit("Save", JsonHelper.JsonSerializer(exercise));
                }
            }
            else
            {
                context.WriteErrorAndExit(CurrentLanguage.Errors);
            }
        }

        private static BE.ExerciseAnswerText GetExerciseFromParams(NameValueCollection collection)
        {
            int exerciseId;
            if (!int.TryParse(collection["editExerciseId"], out exerciseId))
            {
                return null;
            }
            int moduleId;
            if (!int.TryParse(collection["editExerciseModuleId"], out moduleId))
            {
                return null;
            }
            var exercise = new BE.ExerciseAnswerText
                             {
                                 Id = exerciseId,
                                 Name = collection["name"],
                                 Description = collection["description"],
                                 Text = collection["questionText"],
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