using System.Runtime.Serialization;

namespace Wordz.BE
{
    /// <summary>
    /// Класс упражнения "Выбор"
    /// </summary>
    [DataContract]
    public class ExerciseSelect : ExerciseBase
    {
        [DataMember]
        public string Text { get; set; }

        [DataMember]
        public int? PictureId { get; set; }
    }
}
