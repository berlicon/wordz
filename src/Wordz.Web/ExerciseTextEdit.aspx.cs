using System;
using System.Web.UI;
using Wordz.BC;
using Wordz.DB.Accessors;

namespace Wordz.Web
{
    public partial class ExerciseTextEdit : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int exerciseTextId;
            if (int.TryParse(Request.QueryString["Id"], out exerciseTextId))
            {
                var accountId = StorageManager.UserLogined ? StorageManager.CurrentAccount.Id : BCCommon.AnonymousAccountId;
                var exerciseText = ExerciseRelatedAccessor.GetExerciseTextById(exerciseTextId, accountId);
                if (exerciseText != null)
                {
                    ExerciseT = exerciseText;
                }
            }
            else
            {
                int moduleId;
                if (int.TryParse(Request.QueryString["ModuleId"], out moduleId))
                {
                    ModuleId.Value = moduleId.ToString();
                }
            }
        }

        /// <summary>
        /// Изменяемое упражнение
        /// </summary>
        protected BE.ExerciseText ExerciseT
        {
            get
            {
                return new BE.ExerciseText
                           {
                               Id = TextId.Value.Trim() != string.Empty ? int.Parse(TextId.Value.Trim()) : 0,
                               Name = NameText.Text.Trim(),
                               Description = DescriptionText.Text.Trim(),
                               Text = TextText.Text.Trim(),
                               ModuleId = ModuleId.Value.Trim() != string.Empty ? int.Parse(ModuleId.Value.Trim()) : 0
                           };
            }
            set
            {
                TextId.Value = value.Id.ToString();
                NameText.Text = value.Name;
                DescriptionText.Text = value.Description;
                TextText.Text = value.Text;
                ModuleId.Value = value.ModuleId.ToString();
            }
        }

        /// <summary>
        /// Обработка события - сохранение упражнения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnSaveClick(object sender, EventArgs e)
        {
            var accountId = StorageManager.UserLogined ? StorageManager.CurrentAccount.Id : BCCommon.AnonymousAccountId;
            if (ExerciseT.Id == default(Int32))
            {
                var exerciseText = ExerciseRelatedAccessor.AddExerciseText(ExerciseT, accountId);
                ExerciseT = exerciseText;
            }
            else
            {
                var exerciseTextId = ExerciseRelatedAccessor.UpdateExerciseText(ExerciseT, accountId);
            }
            Response.Redirect("/");
        }

        /// <summary>
        /// Обработка события - удаление упражнения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnDeleteClick(object sender, EventArgs e)
        {
            var accountId = StorageManager.UserLogined ? StorageManager.CurrentAccount.Id : BCCommon.AnonymousAccountId;
            ExerciseRelatedAccessor.DeleteExerciseTextById(ExerciseT.Id, accountId);
            Response.Redirect("/");
        }

    }
}