using System.Runtime.Serialization;

namespace Wordz.BE
{
    /// <summary>
    /// ����� ���������� "��������� �����"
    /// </summary>
    [DataContract]
    public class ExerciseAnswerText : ExerciseBase
    {
        /// <summary>
        /// ����� ����������
        /// </summary>
        [DataMember]
        public string Text { get; set; }
    }
}