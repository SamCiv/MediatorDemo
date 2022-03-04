using DemoLibrary.Context;
using DemoLibrary.Models;
using DemoLibrary.StudentQueries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace DemoLibrary.Handler.StudentHandler
{
    public class GetStudentByIdHandler : IRequestHandler<GetStudentByIdQuery, StudentDTO>
    {
        private readonly ISchoolContext _context;
       
        public GetStudentByIdHandler(ISchoolContext context)
        {
            _context = context;
        }

        public Task<StudentDTO> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            
            var studente = _context.Students.Where(s => s.ID == request.Id).FirstOrDefault();

            if(studente != null)
                return Task.FromResult(new StudentDTO(studente.FirstMidName, studente.LastName));

            throw new Exception();

            //return Task.FromResult(new StudentDTO()); 

        }
    }
}
