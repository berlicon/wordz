using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wordz.BC.Validation
{
    public class ValidationList : List<ValidationForPropertyMessage>
    {
        public void AddValidationMessage(string propertyName, string message)
        {
            var msg = new ValidationForPropertyMessage {Message = message, PropertyName = propertyName};
            Add(msg);
        }
    }
}
