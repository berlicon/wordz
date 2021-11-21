using System.Runtime.Serialization;

namespace Wordz.BE
{
    /// <summary>
    /// Упражнение "Пропуски в тексте"
    /// </summary>
    [DataContract]
    public class ExerciseSkipText : ExerciseBase
    {
        [DataMember]
        public string Text { get; set; }
    }
}