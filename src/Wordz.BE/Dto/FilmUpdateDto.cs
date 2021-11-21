using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Wordz.BE.Dto
{
    public class FilmUpdateDto : Film
    {
        public const string NativeLanguageDbPropertyName = "native_language_id";

        public const string LearnLanguageDbPropertyName = "language_id";

        [DataMember]
        public int NativeLanguageId { get; set; }

        [DataMember]
        public int LearnLanguageId { get; set; }
    }
}
