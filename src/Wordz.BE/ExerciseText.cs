using System.Runtime.Serialization;
using Wordz.BE.Dto;

namespace Wordz.BE
{
    /// <summary>
    /// Упражнение "Текст"
    /// </summary>
    [DataContract]
    public class ExerciseText : ExerciseBase
    {
        /// <summary>
        /// Текст упражнения
        /// </summary>
        [DataMember]
        public string Text { get; set; }
    }
}
