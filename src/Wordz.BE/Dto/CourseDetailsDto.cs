using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Wordz.BE.Dto
{
    [DataContract]
    public class CourseDetailsDto : Course
    {
        #region Добавочные поля из хранимки
        
        public string CategoryName { get; set; }
        public string UILanguageName { get; set; }
        [DataMember]
        public string CurrencyName { get; set; }

        #endregion

        /// <summary>
        /// Введенный ранее пароль юзером на этот курс
        /// </summary>
        public string StoredPasswordByUser { get; set; }

        /// <summary>
        /// Определяет, все ли дочерние элементы заапрувлены
        /// </summary>
        public bool IsAllChildsApproved { get; set; }

        /// <summary>
        /// Определяет, куплен ли курс тем пользователем
        /// от которого производился запрос
        /// </summary>
        public bool IsBuyedByCurrentUser { get; set; }

        /// <summary>
        /// Определяет рейтинг курс, выставленный пользователем, от которого
        /// производился запрос 
        /// </summary>
        public double? RateByCurrentUser { get; set; }

        /// <summary>
        /// Строка RateByCurrentUser в формате для JS
        /// </summary>
        /// <returns></returns>
        public string GetRateByCurrentUserString()
        {
            return RateByCurrentUser.HasValue
                ? RateByCurrentUser.Value.ToString(new NumberFormatInfo { NumberDecimalSeparator = "." }) : "0";
        }
    }
}
