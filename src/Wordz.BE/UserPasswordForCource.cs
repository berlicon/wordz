using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wordz.BE
{
    public class UserPasswordForCource
    {
        /// <summary>
        /// Id пользователя
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Id курса
        /// </summary>
        public int CourseId { get; set; }

        /// <summary>
        /// Пароль введенный пользователем
        /// </summary>
        public string StoredPassword { get; set; }
    }
}
