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
    public class ValidationException : Exception
    {
        public IEnumerable<ValidationError> Errors { get; set; }

        public ValidationException() { }

        public ValidationException(string message) : base(message)
        {
            var errors = new List<ValidationError>();
                errors.Add(new ValidationError() { ErrorMessage = message });

            this.Errors = errors.AsEnumerable();
        }

        public ValidationException(IEnumerable<ValidationError> errors)
        {
            this.Errors = errors;
        }
    }
}
