using System.Runtime.Serialization;

namespace Wordz.BE
{
    /// <summary>
    /// Класс ответа в упражнении "Текстовый ответ"
    /// </summary>
    [DataContract]
    public class ExerciseTextAnswer
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int ExerciseId { get; set; }

        [DataMember]
        public int? AccountId { get; set; }

        [DataMember]
        public string Text { get; set; }

        [DataMember]
        public int Mark { get; set; }
    }
}