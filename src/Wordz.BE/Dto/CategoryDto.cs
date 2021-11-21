using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wordz.BE.Dto
{
    public class CategoryDto
    {
        public int Id { get; set; }
        /// <summary>
        /// Название категории
        /// </summary>
        public string CategoryName { get; set; }
        /// <summary>
        /// Для какого языка было получено DTO
        /// </summary>
        public int LanguageId { get; set; }
    }
}
