using System;
using System.Linq;
using System.Web.UI;
using System.Windows.Forms;
using Wordz.BC;
using Wordz.BE.Dto;
using Wordz.DB.Accessors;
using Wordz.Web.Helpers;
using Wordz.Lng;

namespace Wordz.Web
{
    /// <summary>
    /// Страница выполнения упражнения "Выбор"
    /// </summary>
    public partial class ExerciseSelect : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int exerciseId;
            var idString = Request.QueryString["Id"].ReturnEmptyIfNull();
            if (int.TryParse(idString, out exerciseId))
            {
                var accountId = StorageManager.UserLogined ? StorageManager.CurrentAccount.Id : BCCommon.AnonymousAccountId;
                var exerciseText = ExerciseRelatedAccessor.GetExerciseSelectById(exerciseId, accountId);
                if (exerciseText != null)
                {
                    ExerciseT = exerciseText;
                    ModuleId.Value = exerciseText.ModuleId.ToString();
                }
            }
            else
            {
                int moduleId;
                if (int.TryParse(Request.QueryString["ModuleId"], out moduleId))
                {
                    ExerciseT.ModuleId = moduleId;
                    ModuleId.Value = moduleId.ToString();
                }
            }
        }
        
        protected ExerciseSelectDto ExerciseT { get; set; }

        protected void OnCheckButtonClick(object sender, EventArgs e)
        {
            int answerId;
            if (int.TryParse(SelectedAnswer.Value, out answerId))
            {
                var answersDto = ExerciseT.Answers.FirstOrDefault(x => x.IsRight && x.Id == answerId);
				Alert(answersDto != null ? CurrentLanguage.Correct : CurrentLanguage.Incorrect);
            }
            else
            {
				Alert(CurrentLanguage.AnswerNotChosen);
            }
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

        protected void OnOkClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Module/" + ModuleId.Value);
        }
    }
}