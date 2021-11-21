using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Wordz.BE.Dto
{
    public class ExerciseSelectDto : ExerciseSelect
    {
        [DataMember]
        public Picture QuestionPicture { get; set; }

        [DataMember]
        public IEnumerable<AnswerDto> Answers { get; set; }
    }
}
