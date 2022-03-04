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
    internal class InputValidationException : ValidationException
    {
        public IEnumerable<ValidationFailure>? errors;
        public string Title { get; set; }

        public string Type { get; } = "https://tools.ietf.org/html/rfc7231#section-6.5.1";

        public InputValidationException(string title, string message) : base(message) //usa il cotruttore base di Exception
        {
            Title = title;           
        }

        public InputValidationException(IEnumerable<ValidationFailure> errors) : base(errors)
        {
            Title = "Input Validation Error";
                       
        }
    }
}
