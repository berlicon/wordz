using System.Runtime.Serialization;

namespace Wordz.BE
{
    /// <summary>
    /// Упражнение "Выбор в тексте"
    /// </summary>
    [DataContract]
    public class ExerciseSelectText : ExerciseBase
    {
        [DataMember]
        public string Text { get; set; }
    }
}