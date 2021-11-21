using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Wordz.BE.Dto
{
    [DataContract]
    public class UserCommentWithExtraDto : UserComment
    {
        /// <summary>
        /// Имя пользователя, добавившего коммент
        /// </summary>
        [DataMember]
        public string UserName { get; set; }

        /// <summary>
        /// Время создания строкой
        /// </summary>
        [DataMember]
        public string CreateDateString { get; set; }

        [DataMember]
        public bool IsCanClaim { get; set; }

        [DataMember]
        public bool IsCanRate { get; set; }
    }
}
