using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wordz.BC.Validation
{
    public class ValidationForPropertyMessage
    {
        /// <summary>
        /// Название свойства
        /// </summary>
        public string PropertyName { get; set; }
        /// <summary>
        /// Текст валидации для свойства
        /// </summary>
        public string Message { get; set; }

        public ValidationForPropertyMessage()
        {
        }

        public ValidationForPropertyMessage(string message, string propertyName)
        {
            Message = message;
            PropertyName = propertyName;
        }
    }
}
