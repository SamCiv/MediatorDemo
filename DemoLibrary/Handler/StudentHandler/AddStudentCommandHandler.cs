using DemoLibrary.Commands.StudentCommand;
using DemoLibrary.Context;
using DemoLibrary.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoLibrary.Handler.StudentHandler
{
    public class AddStudentCommandHandler : IRequestHandler<AddStudentCommand>
    {
        private readonly ISchoolContext _context;

        public AddStudentCommandHandler(ISchoolContext context)
        {
            _context = context;
        }


        public async Task<Unit> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {
            //Creo il nuovo studente e gl iassegno i parametri della Request
            Student student = new Student();
            student.FirstMidName = request.Student.FirstName;
            student.LastName = request.Student.LastName;
            student.EnrollmentDate = request.Student.EnrollmentDate;

            _context.Add(student);
            
            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
