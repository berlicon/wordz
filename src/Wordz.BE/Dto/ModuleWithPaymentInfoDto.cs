using System.Globalization;
using System.Runtime.Serialization;

namespace Wordz.BE.Dto
{
    [DataContract]
    public class ModuleWithPaymentInfoDto : Module
    {
        [DataMember]
        public int? OwnerId { get; set; }

        [DataMember]
        public string CurrencyName { get; set; }
        
        [DataMember]
        public bool IsPayd { get; set; }

        [DataMember]
        public double? RateByCurrentUser { get; set; }

        [DataMember]
        public decimal PriceInCourseCurrency { get; set; }

        [DataMember]
        public double? TotalRate { get; set; }

        /// <summary>
        /// Строка RateByCurrentUser в формате для JS
        /// </summary>
        /// <returns></returns>
        public string GetRateByCurrentUserString()
        {
            return RateByCurrentUser.HasValue
                ? RateByCurrentUser.Value.ToString(new NumberFormatInfo { NumberDecimalSeparator = "." }) : "0";
        }

        /// <summary>
        /// Строка TotalRate в формате для JS
        /// </summary>
        /// <returns></returns>
        public string GetTotalRateString()
        {
            return TotalRate.HasValue
                ? TotalRate.Value.ToString(new NumberFormatInfo { NumberDecimalSeparator = "." }) : "0";
        }
    }
}
