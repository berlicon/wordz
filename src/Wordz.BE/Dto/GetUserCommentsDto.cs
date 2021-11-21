using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Wordz.BE.Dto
{
    [DataContract]
    public class GetUserCommentsDto
    {
        [DataMember]
        public IList<UserCommentWithExtraDto> Comments { get; set; }

        [DataMember]
        public int Pages { get; set; }

        [DataMember]
        public int PageSize { get; set; }

        [DataMember]
        public int Count { get; set; }

        [DataMember]
        public int CurrentAccountId { get; set; }
    }
}
