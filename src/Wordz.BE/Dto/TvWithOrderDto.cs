using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Wordz.BE.Dto
{
    [DataContract]
    public class TvWithOrderDto : Tv
    {
        [DataMember]
        public int OrderInList { get; set; }
    }
}
