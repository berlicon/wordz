using System.Runtime.Serialization;

namespace Wordz.BE
{
    /// <summary>
    /// ���������� "����� � ������"
    /// </summary>
    [DataContract]
    public class ExerciseSelectText : ExerciseBase
    {
        [DataMember]
        public string Text { get; set; }
    }
}