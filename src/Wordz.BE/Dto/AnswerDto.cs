using System.Runtime.Serialization;

namespace Wordz.BE.Dto
{
    public class AnswerDto : Answer
    {
        [DataMember]
        public Picture Picture { get; set; }
    }
}
