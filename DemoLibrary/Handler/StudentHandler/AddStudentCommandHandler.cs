using DemoLibrary.Commands.StudentCommand;
using DemoLibrary.Context;
using DemoLibrary.Exceptions.CommandException;
using DemoLibrary.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoLibrary.Handler.StudentHandler
{
    public class AddStudentCommandHandler : IRequestHandler<AddStudentCommand, ResultQC<bool>>
    {
        private readonly ISchoolContext _context;

        public AddStudentCommandHandler(ISchoolContext context)
        {
            _context = context;
        }


        public async Task<ResultQC<bool>> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {
            var stud = _context.Students.Where(s => s.FirstMidName == request.Student.FirstName && s.LastName == request.Student.LastName).FirstOrDefault();

            if (stud != null)
               throw new AddStudentCommandException("L'utente che si sta cercando di inserire e' gia' presente nel DB");
                       
            //Creo il nuovo studente e gl iassegno i parametri della Request
            Student student = new Student();
            student.FirstMidName = request.Student.FirstName;
            student.LastName = request.Student.LastName;
            student.EnrollmentDate = request.Student.EnrollmentDate;

            _context.Add(student);
            
            await _context.SaveChangesAsync();

            return ResultQC<bool>.Success(true);
           
            //return ResultQC<bool>.Failure(true);
            
            
            
            
        }
    }
}
