using System.Collections.Specialized;
using System.Web;
using System.Web.SessionState;
using Wordz.BC;
using Wordz.BE;
using Wordz.DB.Accessors;
using Wordz.Web.Helpers;
using Wordz.Lng;

namespace Wordz.Web.Handlers.Exercise
{
    public class ExerciseAnswerTextAddHandler : IHttpHandler, IRequiresSessionState
    {
        private HttpRequest Request { get; set; }
        private HttpResponse Response { get; set; }

        public void ProcessRequest(HttpContext context)
        {
            context.WriteErrorIfNotLoggedAndExit();

            Request = context.Request;
            Response = context.Response;

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
                    exercise.Name = string.IsNullOrEmpty(exercise.Name) ? "Name" : exercise.Name;
                    exercise.Description = string.IsNullOrEmpty(exercise.Description) ? "Description" : exercise.Description;
                    exercise.Text = string.IsNullOrEmpty(exercise.Text) ? "Text" : exercise.Text;
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
            if (!int.TryParse(collection["editExerciseAnswerText"], out exerciseId))
            {
                return null;
            }
            int moduleId;
            if (!int.TryParse(collection["editExerciseAnswerTextModule"], out moduleId))
            {
                return null;
            }
            var exercise = new BE.ExerciseAnswerText
            {
                Id = exerciseId,
                Name = collection["exerciseAnswerTextName"],
                Description = collection["exerciseAnswerTextDescription"],
                Text = collection["exerciseAnswerTextQuestionText"], 
                ModuleId = moduleId,
                ExerciseType = ExerciseType.TextAnswer
            };
            return exercise;
        }

        public bool IsReusable
        {
            get { return false; }
        }
    }
}