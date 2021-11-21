using System;
using System.Text;
using System.Web.UI;
using Wordz.BC;
using Wordz.DB.Accessors;

namespace Wordz.Web
{
    /// <summary>
    /// Класс страницы вызова создания/редактирования и выполнения упражнения "Пропуски в тексте"
    /// </summary>
    public partial class ExerciseSkipText : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int exerciseId;
            if (int.TryParse(Request.QueryString["Id"], out exerciseId))
            {
                var accountId = StorageManager.UserLogined ? StorageManager.CurrentAccount.Id : BCCommon.AnonymousAccountId;
                Exercise = ExerciseRelatedAccessor.GetExerciseSkipTextById(exerciseId, accountId);
                ExerciseText = GetExerciseText();
                Answer = GetAnswer();
            }
            if (Exercise == null)
            {
                Exercise = new BE.ExerciseSkipText
                {
                    Id = 0
                };
            }
            else
            {
                ModuleId.Value = Exercise.ModuleId.ToString();
            }

            int exerciseModuleId;
            if (int.TryParse(Request.QueryString["ModuleId"], out exerciseModuleId))
            {
                Exercise.ModuleId = exerciseModuleId;
                ModuleId.Value = exerciseModuleId.ToString();
            }
        }

        public BE.ExerciseSkipText Exercise { get; set; }

        public string ExerciseText { get; set; }

        public string Answer { get; set; }

        private string GetExerciseText()
        {
            var exerciseTextForUser = new StringBuilder();
            //парсим текст, создавая на основе его разметку для пользователя
            var exerciseCharArray = Exercise.Text.ToCharArray();
            bool isAdd = true;
            int answerLenght = 0;
            for (int i = 0; i < exerciseCharArray.Length; i++)
            {
                switch (exerciseCharArray[i])
                {
                    case '[':
                        isAdd = false;
                        exerciseTextForUser.Append(@"<input id='input_" + i + "' ");
                        break;
                    case ']':
                        isAdd = true;
                        exerciseTextForUser.Append(@"maxlength='" + answerLenght + "' />");
                        answerLenght = 0;
                        break;
                    default:
                        if (isAdd)
                        {
                            exerciseTextForUser.Append(exerciseCharArray[i]);
                        }
                        else
                        {
                            answerLenght++;
                        }
                        break;
                }
            }
            return exerciseTextForUser.ToString();
        }

        private string GetAnswer()
        {
            var exerciseTextAnswer = new StringBuilder();
            //парсим текст, создавая на основе его разметку для пользователя
            var exerciseCharArray = Exercise.Text.ToCharArray();
            for (int i = 0; i < exerciseCharArray.Length; i++)
            {
                switch (exerciseCharArray[i])
                {
                    case ']':
                    case '[':
                        break;
                    default:
                        exerciseTextAnswer.Append(exerciseCharArray[i]);
                        break;
                }
            }
            return exerciseTextAnswer.ToString();
        }
    }
}