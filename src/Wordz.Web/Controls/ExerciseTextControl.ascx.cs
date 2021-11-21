using System;
using System.Web.UI;
using Wordz.BC;
using Wordz.DB.Accessors;

namespace Wordz.Web.Controls
{
    public partial class ExerciseTextControl : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //int exerciseTextId;
            //if (int.TryParse(Request.QueryString["Id"], out exerciseTextId))
            //{
            //    var accountId = StorageManager.UserLogined ? StorageManager.CurrentAccount.Id : BCCommon.AnonymousAccountId;
            //    ExerciseT = ExerciseRelatedAccessor.GetExerciseTextById(exerciseTextId, accountId);
            //}
            //else
            //{
            //    int moduleId;
            //    if (int.TryParse(Request.QueryString["ModuleId"], out moduleId))
            //    {
            //        ExerciseT = new BE.ExerciseText
            //                        {
            //                            Id = 0,
            //                            ModuleId = moduleId
            //                        };
            //    }
            //}
        }

        protected BE.ExerciseText ExerciseText { get; set; }
    }
}