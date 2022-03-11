using DemoApi.Application.DTO;
using DemoApi.Application.Queries;
using DemoApi.Application.Queries.StudentQuery;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace DemoApi.Application.Handler.StudentHandler
{
    public class GetStudentByIdHandler2 : IRequestHandler<GetStudentByIdQuery2, ResultQC<StudentDTO>>
    {
        
        private readonly IStudentQueries _studentQueries;

        public GetStudentByIdHandler2(IStudentQueries studentQueries)
        {
            _studentQueries = studentQueries;
        }

        public Task<ResultQC<StudentDTO>> Handle(GetStudentByIdQuery2 request, CancellationToken cancellationToken)
        {

            var studente = _studentQueries.GetStudentByID(request.Id);

            //var studente = _context.Students.Where(s => s.ID == request.Id).FirstOrDefault();

            return studente;

            /*if (studente != null)
            {
                var res = ResultQC<StudentDTO>.Success(new StudentDTO(studente.FirstMidName, studente.LastName));

                return Task.FromResult(res);
            }

            return Task.FromResult(ResultQC<StudentDTO>.Success());*/
            
        }

    }
    
}
