using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wordz.BE.Dto
{
    public class CourseDetailsWithModulesInfoDto : CourseDetailsDto
    {
        /// <summary>
        /// Модули курса
        /// </summary>
        public ModuleWithPaymentInfoDto[] Modules { get; set; }
    }
}
