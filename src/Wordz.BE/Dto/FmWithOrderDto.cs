using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Wordz.BE.Dto
{
    [DataContract]
    public class FmWithOrderDto : Fm
    {
        [DataMember]
        public int OrderInList { get; set; }

        public FmWithOrderDto(int id,
            string imageUrl,
            string url,
            string name,
            string description,
            bool useMediaPlayer,
            int? authorId,
            bool isEditable,
            int orderInList) :
            base(id, imageUrl, url, name, description, useMediaPlayer, authorId, isEditable)
        {
            OrderInList = orderInList;
        }
    }
}
