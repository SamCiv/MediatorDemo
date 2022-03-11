using FluentValidation;
using DemoApi.Application.Commands.StudentCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApi.Application.Validations
{
    //Classe validator per il Command AddStudentCommand che aggiunge uno studente al DB
    public class AddStudentCommandValidator : AbstractValidator<AddStudentCommand>
    {
        public AddStudentCommandValidator() //Costruttore base
        {
            //RuleSet("Names", () =>
            //{
            RuleFor(command => command.Student.FirstName).NotEmpty().MinimumLength(2).OverridePropertyName("First name"); //NotEmpty include implicitamente NotNull
            RuleFor(command => command.Student.LastName).NotEmpty().MinimumLength(2).OverridePropertyName("Last name");
            // });

            RuleFor(command => command.Student.EnrollmentDate).NotEmpty();
            //RuleFor(customer => customer.Address).SetValidator(new AddressValidator()); //Esempio aggiunta validazione ad un paramentro che ha un altro validatore
        }
    }
}
