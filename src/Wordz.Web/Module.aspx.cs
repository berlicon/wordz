using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Wordz.BC;
using Wordz.BE;
using Wordz.BE.Dto;
using Wordz.DB.Accessors;
using Wordz.Web.Helpers;
using Wordz.Lng;

namespace Wordz.Web
{
	public partial class Module : System.Web.UI.Page
	{
	    public ModuleWithPaymentAndExercisesInfoDto TheModule { get; set; }
		
        protected void Page_Load(object sender, EventArgs e)
		{
            var idString = Request.QueryString["Id"].ReturnEmptyIfNull();
            int moduleId;
            var accountId = StorageManager.UserLogined ? StorageManager.CurrentAccount.Id : BCCommon.AnonymousAccountId;
            if (int.TryParse(idString, out moduleId))
            {
                TheModule = CoursesRelatedAccessor.GetModuleWithExercisesById(accountId, moduleId);

                if (TheModule == null)
                {
                    processModuleNotFoundError();
                }
                else
                {
                    if (!TheModule.IsPayd && TheModule.Price > 0)
                    {
                        Response.Redirect("/Course/" + TheModule.CourceId);
                        return;
                    }

                    courseIdField.Value = TheModule.CourceId.ToString();
                    fillViewFromModel();
                }
            }
		    else
            {
                if (idString.ToLower() =="new")
                {
                    int courseId;
                    if (int.TryParse(Request.QueryString["CourseId"], out courseId))
                    {
                        courseIdField.Value = courseId.ToString();
                        fillViewFromModelForNewModule(courseId);
                    }
                    else
                    {
                        processModuleNotFoundError();
                    }
                }
                else
                {
                    processModuleNotFoundError();
                }
            }
		}

        /// <summary>
        /// Заполняет базовые элементы
        /// </summary>
        private void fillBaseElements()
        {
            editButtonsContainer.InnerHtml =
                RenderHelper.Button("editYesBtn",
                                    CurrentLanguage.Save,
                                    null,
                                    "display: inline")
                + "&nbsp;&nbsp;&nbsp;" +
                RenderHelper.Button("editNoBtn",
                                    CurrentLanguage.Cancel,
                                    null,
                                    "display: inline");
        }

        /// <summary>
        /// Заполнение формы из модели
        /// </summary>
        private void fillViewFromModel()
        {
            fillBaseElements();

            UserComments1.TargetElement = TheModule.Number;

            moduleIdField.Value = TheModule.Id.ToString();
            lblModuleTitle.InnerText = TheModule.Name.HtmlEncode();
            lblModuleName.Text = TheModule.Name.HtmlEncode();
            lblModuleDescription.Text = TheModule.Description.HtmlEncode();
            lblModuleDetailedDescription.Text = TheModule.DetailedDescription.ReplaceScriptTags();
            lblModuleLinks.Text = TextHelper.GetLinksFromString(TheModule.Links.HtmlEncode());
            lblModuleTags.Text = TheModule.Tags.HtmlEncode();
            lblModuleUrl.Text = TheModule.Url.HtmlEncode();

            lblModulePrice.Text = TheModule.Price + " (" + TheModule.CurrencyName + ")";

            backUrlBtn.HRef = "/Course/" + TheModule.CourceId;

            moduleExerciseRepeater.DataSource =
                TheModule.Exercises.Select(ex =>
                                           new
                                               {
                                                   Link = (ex.ExerciseType == ExerciseType.Select 
                                                            || ex.ExerciseType == ExerciseType.SelectText)
                                                        ? ex.ExerciseType.GetViewUrl() + ex.Id
                                                        : ex.ExerciseType.GetViewUrl() + "/" + ex.Id,
                                                   ex.Name
                                               }
                    );
            moduleExerciseRepeater.DataBind();

            if (TheModule.PictureId.HasValue)
            {
                moduleImage.ImageUrl = string.Format("Handlers/LoadPictureHandler.ashx?id={0}",
                                                     TheModule.PictureId.Value);
            }
            else
            {
                moduleImage.Visible = false;
            }

            //lblModuleEditExerciseDiv.InnerHtml += "<table width=\"100%\" id=\"exerciseListId\">";
            lblModuleEditExerciseDiv.InnerHtml += "<ul id=\"exerciseListId\" class=\"connectedSortableExercises\">";

            for (var i = 0; i < TheModule.Exercises.Length; i++)
            {
                var exercise = TheModule.Exercises[i];
                string exDoMethod;
                string exEditMethod;
                switch (exercise.ExerciseType)
                {
                    case ExerciseType.Text:
                        exDoMethod = "OpenExerciseText(" + exercise.Id + ");";
                        exEditMethod = "EditExerciseText(" + exercise.Id + ");";
                        break;
                    case ExerciseType.Select:
                        exDoMethod = "OpenExerciseSelect(" + exercise.Id + ");";
                        exEditMethod = "EditExerciseSelect(" + exercise.Id + ");";
                        break;
                    case ExerciseType.TextAnswer:
                        exDoMethod = "OpenExerciseAnswerText(" + exercise.Id + ");";
                        exEditMethod = "EditExerciseAnswerText(" + exercise.Id + ");";
                        break;
                    case ExerciseType.SelectText:
                        exDoMethod = "OpenExerciseSelectText(" + exercise.Id + ");";
                        exEditMethod = "EditExerciseSelectText(" + exercise.Id + ");";
                        break;
                    case ExerciseType.SkipText:
                        exDoMethod = "OpenExerciseSkipText(" + exercise.Id + ");";
                        exEditMethod = "EditExerciseSkipText(" + exercise.Id + ");";
                        break;
                    default:
                        exDoMethod = string.Empty;
                        exEditMethod = string.Empty;
                        break;
                }
                //lblModuleEditExerciseDiv.InnerHtml += RenderHelper.ExerciseTableRow(
                //    exDoMethod,
                //    exercise.Name,
                //    exercise.Description,
                //    exType, i);

                lblModuleEditExerciseDiv.InnerHtml += RenderHelper.ExerciseLiRow(
                    exDoMethod,
                    exercise,
                    i);
            }
            
            //lblModuleEditExerciseDiv.InnerHtml += "</table>";

            lblModuleEditExerciseDiv.InnerHtml += "</ul>";

            var account = StorageManager.CurrentAccount;

            if (account != null && (account.IsAdmin || account.Id == TheModule.OwnerId))
            {
                editButtonPanel.InnerHtml = RenderHelper.Button(
                    "courseEditBtn",
                    CurrentLanguage.EditModule,
                    "editModule(false, " + TheModule.Id + ");",
                    "display: inline");
            }
        }

        private void fillViewFromModelForNewModule(int courseId)
        {
            fillBaseElements();
            moduleIdField.Value = "0"; // У модуля нет Id
            isNewModule.Text = "<script type='text/javascript'>document.isNewModule = true;</script>";
            isNewModule.Visible = true;
            backUrlBtn.HRef = "/Course/" + courseId;
        }

        private void processModuleNotFoundError()
        {
            Response.Redirect("/");
        }
	}
}