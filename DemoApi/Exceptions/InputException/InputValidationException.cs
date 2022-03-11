using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoLibrary.Exceptions
{
    [Serializable]
    public class InputValidationException : ValidationException
    {        
        public string Title { get; } = "Input Validation Error";

        public InputValidationException(IEnumerable<ValidationFailure> errors) : base(errors)
        {
                           
        }
    }
}
