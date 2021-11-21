using System;
using System.Text;
using System.Web.UI;
using Wordz.BC;
using Wordz.DB.Accessors;

namespace Wordz.Web
{
    /// <summary>
    /// Страница вызова добавления/изменения и выполнения упражнения "Выбор в тексте"
    /// </summary>
    public partial class ExerciseSelectText : Page
    {
        public BE.ExerciseSelectText Exercise { get; set; }

        public string ExerciseText { get; set; }

        public string Answer { get; set; }

        private string GetExerciseText()
        {
            var exerciseTextForUser = new StringBuilder();
            //парсим текст, создавая на основе его разметку для пользователя
            var exerciseCharArray = Exercise.Text.ToCharArray();
            for (int i = 0; i < exerciseCharArray.Length; i++)
            {
                switch (exerciseCharArray[i])
                {
                    case '[':
                        exerciseTextForUser.Append(@"<select id='select_" + i + "' size='1'><option id='option_" + i + "' value='0'>");
                        break;
                    case '*':
                        exerciseTextForUser.Remove(exerciseTextForUser.Length - 3, 3).Append("1'>");
                        break;
                    case '/':
                        exerciseTextForUser.Append(@"</option><option id='option_" + i + "' value='0'>");
                        break;
                    case ']':
                        exerciseTextForUser.Append(@"</option></select>");
                        break;
                    default:
                        exerciseTextForUser.Append(exerciseCharArray[i]);
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
            bool isAdd = true;
            for (int i = 0; i < exerciseCharArray.Length; i++)
            {
                switch (exerciseCharArray[i])
                {
                    case '*':
                    case ']':
                        isAdd = true;
                        break;
                    case '/':
                    case '[':
                        isAdd = false;
                        break;
                    default:
                        if (isAdd)
                        {
                            exerciseTextAnswer.Append(exerciseCharArray[i]);
                        }
                        break;
                }
            }
            return exerciseTextAnswer.ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            int exerciseId;
            if (int.TryParse(Request.QueryString["Id"], out exerciseId))
            {
                var accountId = StorageManager.UserLogined ? StorageManager.CurrentAccount.Id : BCCommon.AnonymousAccountId;
                Exercise = ExerciseRelatedAccessor.GetExerciseSelectTextById(exerciseId, accountId);
                ExerciseText = GetExerciseText();
                Answer = GetAnswer();
            }
            if (Exercise == null)
            {
                Exercise = new BE.ExerciseSelectText
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
            }
        }

        protected void OnOkClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Module/" + ModuleId.Value);
        }

        protected void OnCheckButtonClick(object sender, EventArgs e)
        {
            int answerId;
            //if (int.TryParse(SelectedAnswer.Value, out answerId))
            //{
            //    var answersDto = ExerciseT.Answers.FirstOrDefault(x => x.IsRight && x.Id == answerId);
            //    Alert(answersDto != null ? "Правильно!" : "Неправильно!");
            //}
            //else
            //{
            //    Alert("Ответ не выбран!");
            //}
        }

        private void Alert(string text)
        {
            var csname1 = "PopupScript";
            var cstype = this.GetType();
            var cs = Page.ClientScript;
            if (!cs.IsStartupScriptRegistered(cstype, csname1))
            {
                var cstext1 = "alert('" + text + "');";
                cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            }
        }
    }
}