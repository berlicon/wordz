using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Wordz.BE.Dto
{
    [DataContract]
    public class RateOrClaimResultDto
    {
        [DataMember]
        public string ActionType { get; set; }
        
        [DataMember]
        public int Rate { get; set; }
    }
}
