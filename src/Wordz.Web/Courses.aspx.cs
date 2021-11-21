using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Wordz.BC;
using Wordz.BE.Dto;

namespace Wordz.Web
{
    /// <summary>
    /// Класс страницы со список всех курсов
    /// </summary>
	public partial class Courses : Page
	{
        /// <summary>
        /// Список всех курсов
        /// </summary>
        public IEnumerable<CourseDetailsDto> TheCourses { get; set; }

		protected void Page_Load(object sender, EventArgs e)
		{
		    if (!IsPostBack)
		    {
		        var accountId = StorageManager.UserLogined
		                            ? StorageManager.CurrentAccount.Id
		                            : BCCommon.AnonymousAccountId;

                var courseList = BCCommon.GetCourseList(accountId, true);
		        if (courseList != null)
		        {
                    TheCourses = courseList
                        .OrderBy(y => y.CategoryId)
                        .ThenBy(x => x.Name)
                        .ToArray();
		        }
                else
		        {
		            TheCourses = new CourseDetailsDto[0];
		        }
		    }
		}

        //protected void onCheckedChanged(object sender, EventArgs e)
        //{
        //    var coursesList = this.FindControl("courses");
        //    var directories = this.FindControl("directories");
        //    var checkBoxControl = sender as CheckBox;
        //    if (coursesList != null && directories != null && checkBoxControl != null)
        //    {
        //        coursesList.Visible = !checkBoxControl.Checked;
        //        directories.Visible = checkBoxControl.Checked;
        //    }
        //}
	}
}