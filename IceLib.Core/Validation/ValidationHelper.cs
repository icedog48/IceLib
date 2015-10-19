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

        public bool HasErrors(object obj)
        {
            var validationResult = new List<ValidationResult>();

            var context = new ValidationContext(obj);

            Validator.TryValidateObject(obj, context, validationResult, true);

            this.validationErrors = validationResult.Select(error => new ValidationError()
            {
                MemberName = error.MemberNames.FirstOrDefault(),
                ErrorMessage = error.ErrorMessage
            });

            return this.validationErrors.Any();
        }
    }
}
