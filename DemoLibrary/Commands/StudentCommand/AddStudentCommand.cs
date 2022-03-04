using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoLibrary.Models;
using MediatR;

namespace DemoLibrary.Commands.StudentCommand
{
    public record AddStudentCommand (StudentDTO Student) : IRequest<ResultQC<bool>>;

  /*  {
        public StudentDTO Student { get; set; } //passo il DTO dell ostudente che voglio Inserire

        public AddStudentCommand(StudentDTO student)
        {
            Student = student;
        }
    }*/
}
