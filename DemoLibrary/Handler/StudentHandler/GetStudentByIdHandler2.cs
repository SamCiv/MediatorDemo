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
    public class GetStudentByIdHandler2 : IRequestHandler<GetStudentByIdQuery2, ResultQC<StudentDTO>>
    {
        private readonly ISchoolContext _context;
       
        public GetStudentByIdHandler2(ISchoolContext context)
        {
            _context = context;
        }

        public Task<ResultQC<StudentDTO>> Handle(GetStudentByIdQuery2 request, CancellationToken cancellationToken)
        {
            var studente = _context.Students.Where(s => s.ID == request.Id).FirstOrDefault();

            if (studente != null)
            {
                var res = ResultQC<StudentDTO>.Success(new StudentDTO(studente.FirstMidName, studente.LastName));

                return Task.FromResult(res);
            }

            return Task.FromResult(ResultQC<StudentDTO>.Success());
            
        }

    }
    
}
