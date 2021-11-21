using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wordz.BE.Dto
{
    public class CourseWithPictureUpdateDto : Course
    {
        /// <summary>
        /// Определяет необходимость удалить картинку из базы
        /// </summary>
        public bool IsNeedRemovePicture { get; set; }
        
        /// <summary>
        /// Изображение курса
        /// </summary>
        public Picture Picture { get; set; }

        public ModuleOrderInfo[] ModulesOrder { get; set; }

        public int[] ModuleIdsToDelete { get; set; }
    }

    public class ModuleOrderInfo
    {
        public int ModuleId { get; set; }
        public int OrderIndex { get; set; }
    }
}
