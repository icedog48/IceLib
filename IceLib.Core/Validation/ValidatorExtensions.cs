using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceLib.Core.Validation
{
    public static class ValidationResultExtensions
    {
        public static void AndThrow(this ValidationResult result)
        {
            if (!result.IsValid)
                throw new FluentValidation.ValidationException(result.Errors);
        }
    }
}
