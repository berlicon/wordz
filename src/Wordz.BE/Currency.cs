using System.Runtime.Serialization;

namespace Wordz.BE
{
    [DataContract]
    public class Currency
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string LetterCode { get; set; }
        [DataMember]
        public int DigitCode { get; set; }
    }
}
