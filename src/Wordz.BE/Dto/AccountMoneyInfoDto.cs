using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wordz.BE.Dto
{
    public class AccountMoneyInfoDto
    {
        public Currency Currency { get; set; }
        public decimal Value { get; set; }
    }
}
