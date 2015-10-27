using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceLib.Validation
{
    public class ValidationHelper
    {
        public ValidationHelper()
        {

        }

        private IEnumerable<ValidationError> validationErrors;

        public IEnumerable<ValidationError> Errors { get { return validationErrors; } }

        public void Validate(object obj)
        {
            var validationResult = new List<ValidationResult>();

            var context = new ValidationContext(obj);

            Validator.TryValidateObject(obj, context, validationResult, true);

            if (validationResult.Any())
            {
                this.validationErrors = validationResult.Select(error => new ValidationError()
                {
                    MemberName = error.MemberNames.FirstOrDefault(),
                    ErrorMessage = error.ErrorMessage
                });

                throw new IceLib.Services.Exceptions.ValidationException(this.validationErrors);
            }
        }
    }
}
