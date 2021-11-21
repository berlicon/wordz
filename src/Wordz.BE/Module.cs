using System;
using System.Runtime.Serialization;

namespace Wordz.BE
{
    /// <summary>
    /// Описание модуля
    /// </summary>
    [DataContract]
    public class Module
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public Guid Number { get; set; }
        [DataMember]
        public int CourceId { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string DetailedDescription { get; set; }
        [DataMember]
        public int? PictureId { get; set; }
        [DataMember]
        public int? CurrencyId { get; set; }
        [DataMember]
        public decimal Price { get; set; }
        [DataMember]
        public string Url { get; set; }
        [DataMember]
        public string Tags { get; set; }
        [DataMember]
        public string Links { get; set; }
        [DataMember]
        public int OrderInCourse { get; set; }
        [DataMember]
        public int ExerciseMaxNumber { get; set; }
    }
}
