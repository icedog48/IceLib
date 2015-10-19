﻿using FluentValidation.Results;
using IceLib.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceLib.Service.Exceptions
{
    [Serializable]
    public class ValidationException : Exception
    {
        public IList<ValidationError> Errors { get; set; }

        public ValidationException() { }
        public ValidationException(string message) : base(message) { }
        public ValidationException(string message, Exception inner) : base(message, inner) { }
        protected ValidationException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }

        public ValidationException(IList<ValidationError> errors)
        {
            this.Errors = errors;
        }
    }
}
