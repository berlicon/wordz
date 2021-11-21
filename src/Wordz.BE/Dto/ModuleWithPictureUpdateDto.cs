using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wordz.BE.Dto
{
    public class ModuleWithPictureUpdateDto : Module
    {
        /// <summary>
        /// Определяет необходимость удалить картинку из базы
        /// </summary>
        public bool IsNeedRemovePicture { get; set; }

        /// <summary>
        /// Изображение курса
        /// </summary>
        public Picture Picture { get; set; }

        /// <summary>
        /// Порядок упражнений
        /// </summary>
        public ExerciseOrderInfo[] ExercisesOrder { get; set; }
    }

    public class ExerciseOrderInfo
    {
        public int ExerciseId { get; set; }
        public ExerciseType ExerciseType { get; set; }
        public int OrderIndex { get; set; }
    }
}
