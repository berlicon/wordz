using System.Runtime.Serialization;

namespace Wordz.BE
{
    /// <summary>
    /// Класс ответа в упражнении "Выбор"
    /// </summary>
    [DataContract]
    public class Answer
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int ExerciseId { get; set; }

        [DataMember]
        public string Text { get; set; }

        [DataMember]
        public int? PictureId { get; set; }

        [DataMember]
        public bool IsRight { get; set; }
    }
}
