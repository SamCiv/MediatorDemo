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
    public class GetStudentByIdHandler : IRequestHandler<GetStudentByIdQuery, StudentDTO>
    {
       private readonly IStudentQueries _studentQueries;

        public GetStudentByIdHandler(IStudentQueries studentQueries)
        {
           _studentQueries = studentQueries;
        }

        public async Task<StudentDTO> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            ResultQC<StudentDTO> result = await _studentQueries.GetStudentByID(request.Id);

            if(result.Result == null)
                throw new Exception("Lo studente non e' stato trovato");

            var studente = result.Result;

            return studente;

            /*if(studente != null)
                return Task.FromResult(new StudentDTO(studente.FirstMidName, studente.LastName));

            throw new Exception();*/

            //return Task.FromResult(new StudentDTO()); 

        }
    }
}
