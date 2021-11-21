using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wordz.BE
{
    public class Payment
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int? CourseId { get; set; }
        public int? ModuleId { get; set; }
        public int CurrencyId { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
