using System;
using System.Web.UI;
using Wordz.BC;
using Wordz.DB.Accessors;
using Wordz.Lng;

namespace Wordz.Web
{
    public partial class ExerciseText : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int exerciseTextId;
                if (int.TryParse(Request.QueryString["Id"], out exerciseTextId))
                {
                    var accountId = StorageManager.UserLogined
                                        ? StorageManager.CurrentAccount.Id
                                        : BCCommon.AnonymousAccountId;
                    ExerciseT = ExerciseRelatedAccessor.GetExerciseTextById(exerciseTextId, accountId);
                    ModuleId.Value = ExerciseT.ModuleId.ToString();
                }
				TextBlock.Text = ExerciseT != null ? ExerciseT.Text : CurrentLanguage.CantLoadExercise;
            }
        }

        protected BE.ExerciseText ExerciseT { get; set; }

        protected void OnOkClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Module/" + ModuleId.Value);
        }
    }
}