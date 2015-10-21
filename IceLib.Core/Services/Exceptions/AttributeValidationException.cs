using FluentValidation.Results;
using IceLib.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IceLib.Services.Exceptions
{
    [Serializable]
    public class AttributeValidationException : ValidationException
    {
        public AttributeValidationException() { }

        public AttributeValidationException(string message) : base(message) { }

        public AttributeValidationException(IEnumerable<ValidationError> errors): base(errors) { }
    }
}
