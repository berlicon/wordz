using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Wordz.BE
{
    /// <summary>
    /// Пользовательский комментарий
    /// </summary>
    [DataContract]
    public class UserComment
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Text { get; set; }
        [DataMember]
        public int AuthorAccountId { get; set; }
        [DataMember]
        public Guid TargetElement { get; set; }
        [DataMember]
        public DateTime CreatedDate { get; set; }
        [DataMember]
        public int Rating { get; set; }
        [DataMember]
        public int ClaimsCount { get; set; }
        [DataMember]
        public int AnswerLevel { get; set; }
    }
}
