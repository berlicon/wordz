using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wordz.BE.Dto
{
    public class EntityOrderingUpdateInfoDto
    {
        public List<EntityOrderingInfo> OrderingInfo { get; private set; }

        public EntityOrderingUpdateInfoDto()
        {
            OrderingInfo = new List<EntityOrderingInfo>();
        }
    }

    public struct EntityOrderingInfo
    {
        public int Id { get; set; }
        public int OrderIndex { get; set; }
    }
}
