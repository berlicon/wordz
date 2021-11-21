using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using Wordz.BC;
using Wordz.BE;
using Wordz.BE.Dto;
using Wordz.DB.Accessors;
using Wordz.Web.Helpers;
using Wordz.Lng;

namespace Wordz.Web
{
	public partial class Course : Page
	{
        public CourseDetailsWithModulesInfoDto TheCourse { get; set; }
        public ModuleWithPaymentInfoDto[] TheModules { get; set; }
        public bool IsNew { get; set; }
        public decimal CourceCalculatedPrice { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            IsNew = false;
            var idString = Request.QueryString["Id"].ReturnEmptyIfNull();
            int courseId;
            var accountId = StorageManager.UserLogined ? StorageManager.CurrentAccount.Id : BCCommon.AnonymousAccountId;
            if (int.TryParse(idString, out courseId))
            {
                TheCourse = CoursesRelatedAccessor.GetCourseDetailsWithModulesById(accountId, courseId);

                if (TheCourse != null)
                {
                    if (checkPassword())
                    {
                        showExistingCourse();
                    }
                    else
                    {
                        showIncorrectPassword();
                    }
                }
                else
                {
                    showCourseNotFoundError();
                }
            }
            else
            {
                if (idString.ToLower() == "new")
                {
                    showNewCourse();
                }
                else
                {
                    showCourseNotFoundError();
                }
            }
        }

        private bool checkPassword()
        {
            return string.IsNullOrEmpty(TheCourse.Password)
                || StorageManager.UserLogined && StorageManager.CurrentAccount.IsAdmin
                || TheCourse.OwnerId == (StorageManager.UserLogined ? StorageManager.CurrentAccount.Id : BCCommon.AnonymousAccountId)
                || TheCourse.Password == TheCourse.StoredPasswordByUser;
        }

        private void showNewCourse()
        {
            TheCourse = new CourseDetailsWithModulesInfoDto { Id = 0, Number = Guid.NewGuid() };
            TheModules = new ModuleWithPaymentInfoDto[] { };
            IsNew = true;

            if (!StorageManager.UserLogined)
            {
                mainContainer.Visible = false;
                errorUserMessageTextContainer.Visible = true;
                errorScript.Visible = true;

                errorUserMessageText.InnerText = CurrentLanguage.RegisterToCreateCourses;
            }
        }

        private void showIncorrectPassword()
        {
            TheCourse = new CourseDetailsWithModulesInfoDto { Id = TheCourse.Id, Number = TheCourse.Number };
            TheModules = new ModuleWithPaymentInfoDto[] { };
            
            if (!StorageManager.UserLogined)
            {
                mainContainer.Visible = false;
                errorUserMessageTextContainer.Visible = true;
                errorScript.Visible = true;

                errorUserMessageText.InnerText = CurrentLanguage.RegisterToViewCourses;
            }
            else
            {
                mainContainer.Visible = false;
                errorIncorrectPasswordMessageContainer.Visible = true;
                errorIncorrectPasswordMessageText.InnerText = CurrentLanguage.EnterCoursePassword;
            }
        }

        private void showExistingCourse()
        {
            TheModules = TheCourse.Modules;

            backUrlBtn.HRef = "/";

            if ((!TheCourse.IsAllChildsApproved || !TheCourse.IsApproved)
                && StorageManager.CurrentAccount != null
                && StorageManager.CurrentAccount.IsAdmin)
            {
                approveBtnDiv.InnerHtml = RenderHelper.Button(
                    "approveChangesBtn",
					CurrentLanguage.ApproveChanges,
                    "approveCourse(" + TheCourse.Id + ");",
                    "display: inline");
                approveBtnDiv.Visible = true;
            }

            var account = StorageManager.CurrentAccount;

            if (account != null && (account.IsAdmin || account.Id == TheCourse.OwnerId))
            {
                editBtnDiv.InnerHtml = RenderHelper.Button(
                    "courseEditBtn",
					CurrentLanguage.EditCourse,
                    "editCourse(false, " + TheCourse.Id + ");",
                    "display: inline");
                editBtnDiv.Visible = true;
            }

            CourceCalculatedPrice = !TheCourse.Modules.Any(m => m.IsPayd)
                    ? TheCourse.Price
                    : calculateCoursePrice();

            if (!IsNew && !TheCourse.IsBuyedByCurrentUser)
            {
                payForCourseButton.ConfirmText = CurrentLanguage.AreYouSureYouWantToPayCourse +
                                                 CourceCalculatedPrice + "(" + TheCourse.CurrencyName + ")";
                payForCourseButton.OnClickHandler = "payForWholeCourse(" + TheCourse.Id + ", " +
                                                    CourceCalculatedPrice.GetDecimalCaptionJs() + "," + TheCourse.CurrencyId +
                                                    ");";
                payForCourseButton.Style = "display : inline";
                payForCourseButton.Visible = true;
            }
            
            UserComments1.TargetElement = TheCourse.Number;
        }

        private decimal calculateCoursePrice()
        {
            var sumOfNonPayed = TheCourse.Modules
                .Where(module => !module.IsPayd)
                .Sum(module => module.PriceInCourseCurrency);
            var totalSum = TheCourse.Modules.Sum(module => module.PriceInCourseCurrency);
            if (totalSum == 0)
            {
                return TheCourse.Price;
            }
            return Math.Round(TheCourse.Price*(sumOfNonPayed/totalSum),2);
        }
        
	    /// <summary>
        /// Обработка ошибки, когда модуль не был найден
        /// </summary>
        private void showCourseNotFoundError()
        {
            Response.Redirect("/");
        }
	}
}