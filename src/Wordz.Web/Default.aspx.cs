using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Wordz.BC;
using Wordz.BE.Dto;
using Wordz.Web.Handlers;

namespace Wordz.Web
{
    public partial class Default : Page
    {
        /// <summary>
        /// Список всех курсов
        /// </summary>
        public IEnumerable<CourseDetailsDto> TheCourses { get; set; }

        public LoadPictureHandler PictureHandler { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var accountId = StorageManager.UserLogined
                                    ? StorageManager.CurrentAccount.Id
                                    : BCCommon.AnonymousAccountId;

                PictureHandler = new LoadPictureHandler();

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
                    TheCourses = new CourseDetailsDto[] {};
                }
            }
        }
    }
}