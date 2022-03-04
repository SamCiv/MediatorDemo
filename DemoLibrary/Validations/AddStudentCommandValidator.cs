using DemoLibrary.Commands.StudentCommand;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoLibrary.Validations
{
    public class AddStudentCommandValidator :AbstractValidator<AddStudentCommand>
    {
        public AddStudentCommandValidator()
        {
            RuleFor(command => command.Student.FirstName).NotEmpty();
            RuleFor(command => command.Student.LastName).NotEmpty();
            RuleFor(command => command.Student.EnrollmentDate).NotEmpty();
        }
    }
}
