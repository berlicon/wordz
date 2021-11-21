using System.Runtime.Serialization;

namespace Wordz.BE.Dto
{
    [DataContract]
    public class ModuleWithPaymentAndExercisesInfoDto : ModuleWithPaymentInfoDto
    {
        [DataMember]
        public ExerciseBase[] Exercises { get; set; }
    }
}
