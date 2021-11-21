using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wordz.BE.Dto
{
    public class FilmListItemDto
    {
        public int Id { get; set; }

        public int? AccountId { get; set; }

        public string Name { get; set; }

        public string CategoryName { get; set; }

        public string ImageUrl { get; set; }
    }
}
