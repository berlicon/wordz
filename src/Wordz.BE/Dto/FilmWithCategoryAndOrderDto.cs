using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Wordz.BE.Dto
{
    [DataContract]
    public class FilmWithCategoryAndOrderDto : Film
    {
        [DataMember]
        public string CategoryName { get; set; }
        
        [DataMember]
        public int OrderInList { get; set; }
    }
}
