using System;
using System.Web.UI;
using Wordz.BC;
using Wordz.DB.Accessors;

namespace Wordz.Web
{
    public partial class ExerciseTextAnswer : Page
    {
        public BE.ExerciseAnswerText ExerciseText { get; set; }

        public BE.ExerciseTextAnswer Answer { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            int exerciseId;
            if (int.TryParse(Request.QueryString["Id"], out exerciseId))
            {
                var accountId = StorageManager.UserLogined ? StorageManager.CurrentAccount.Id : BCCommon.AnonymousAccountId;
                ExerciseText = ExerciseRelatedAccessor.GetExerciseAnswerTextById(exerciseId, accountId);
            }
            if (ExerciseText == null)
            {
                ExerciseText = new BE.ExerciseAnswerText
                                   {
                                       Id = 0
                                   };
            }
            else
            {
                ModuleId.Value = ExerciseText.ModuleId.ToString();
            }

            int exerciseModuleId;
            if (int.TryParse(Request.QueryString["ModuleId"], out exerciseModuleId))
            {
                ExerciseText.ModuleId = exerciseModuleId;
                ModuleId.Value = exerciseModuleId.ToString();
            }

            Answer = ExerciseRelatedAccessor.GetExerciseTextAnswers(StorageManager.CurrentAccount.Id, exerciseId) ??
                     new BE.ExerciseTextAnswer
                         {
                             Id = 0,
                             AccountId = StorageManager.CurrentAccount.Id,
                             ExerciseId = exerciseId,
                             Text = string.Empty
                         };
        }
    }
}