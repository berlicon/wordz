using System.Collections.Specialized;
using System.Web;
using System.Web.SessionState;
using Wordz.DB.Accessors;
using Wordz.Web.Helpers;
using Wordz.Lng;

namespace Wordz.Web.Handlers.Exercise
{
    public class CheckExerciseTextAnswerHandler : IHttpHandler, IRequiresSessionState
    {
        private HttpRequest Request { get; set; }
        private HttpResponse Response { get; set; }

        public void ProcessRequest(HttpContext context)
        {
            context.WriteErrorIfNotLoggedAndExit();

            Request = context.Request;
            Response = context.Response;

            var account = StorageManager.CurrentAccount;

            var answer = GetAnswerFromParams(Request.Form);
            if (answer != null)
            {
                if (answer.Id != 0)
                {
                    answer.Id = ExerciseRelatedAccessor.UpdateExerciseTextAnswer(answer);
                    context.WriteSuccessAndExit("Update", JsonHelper.JsonSerializer(answer));
                }
                else
                {
                    answer = ExerciseRelatedAccessor.AddExerciseTextAnswer(answer);
                    context.WriteSuccessAndExit("Save", JsonHelper.JsonSerializer(answer));
                }
            }
            else
            {
                context.WriteErrorAndExit(CurrentLanguage.Errors);
            }
        }

        private static BE.ExerciseTextAnswer GetAnswerFromParams(NameValueCollection collection)
        {
            int answerId;
            if (!int.TryParse(collection["doAnswerId"], out answerId))
            {
                return null;
            }

            var exercise = new BE.ExerciseTextAnswer
            {
                Id = answerId,
                Text = collection["answer"], 
                AccountId = StorageManager.CurrentAccount.Id,
                ExerciseId = int.Parse(collection["doExerciseId"]),
                Mark = int.Parse(collection["mark"])
            };
            return exercise;
        }

        public bool IsReusable
        {
            get { return false; }
        }
    }
}