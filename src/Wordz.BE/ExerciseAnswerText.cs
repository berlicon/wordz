using System.Runtime.Serialization;

namespace Wordz.BE
{
    /// <summary>
    /// Класс упражнения "Текстовый ответ"
    /// </summary>
    [DataContract]
    public class ExerciseAnswerText : ExerciseBase
    {
        /// <summary>
        /// Текст упражнения
        /// </summary>
        [DataMember]
        public string Text { get; set; }
    }
}