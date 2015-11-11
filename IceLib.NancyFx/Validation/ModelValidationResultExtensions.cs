using FluentValidation.Results;
using Nancy.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceLib.NancyFx.Validation
{
    public static class ModelValidationResultExtensions
    {
        public static void AndThrow(this ModelValidationResult result)
        {
            if (!result.IsValid)
            {
                var errors = result
                                .Errors
                                    .SelectMany(x => x.Value)
                                        .Select(x => new ValidationFailure(x.MemberNames.FirstOrDefault(), x.ErrorMessage));

                throw new FluentValidation.ValidationException(errors);
            }
        }
    }
}
