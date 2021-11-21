using System.Runtime.Serialization;

namespace Wordz.BE
{
    /// <summary>
    /// Базовый класс для упражнения
    /// </summary>
    [DataContract]
    public class ExerciseBase
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        /// Идентификатор модуля
        /// </summary>
        [DataMember]
        public int ModuleId { get; set; }

        /// <summary>
        /// Порядковый номер в модуле
        /// </summary>
        [DataMember]
        public int OrdinalNumber { get; set; }

        /// <summary>
        /// Тип упражнения
        /// </summary>
        [DataMember]
        public ExerciseType ExerciseType { get; set; }

        /// <summary>
        /// Идентификатор оригинала упражнения
        /// </summary>
        [DataMember]
        public int? ParentId { get; set; }

        /// <summary>
        /// Признак утвержденности упражнения
        /// </summary>
        [DataMember]
        public bool IsApproved { get; set; }
    }
}
