using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Runtime.Serialization;
using System.Text;

namespace Wordz.BE
{
    /// <summary>
    /// Описание курса
    /// </summary>
    [DataContract]
    public class Course
    {
        [DataMember]
        public int? Id { get; set; }
        [DataMember]
        public Guid Number { get; set; }
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
        public int? UILanguageId { get; set; }
        [DataMember]
        public int? LocationId { get; set; }
        [DataMember]
        public int? CategoryId { get; set; }
        [DataMember]
        public string Url { get; set; }
        [DataMember]
        public string Authors { get; set; }
        [DataMember]
        public string Contacts { get; set; }
        [DataMember]
        public string Tags { get; set; }
        [DataMember]
        public string Links { get; set; }
        [DataMember]
        public bool IsEditable { get; set; }
        [DataMember]
        public bool IsCopied { get; set; }
        [DataMember]
        public bool IsPublic { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string GoogleAdvertisment { get; set; }
        [DataMember]
        public bool IsApproved { get; set; }
        [DataMember]
        public int? OwnerId { get; set; }
    }
}
