using DemoApi.Application.DTO;
using DemoApi.Application.Queries;
using DemoApi.Application.Queries.InstructorQuery;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace DemoApi.Application.Handler.InstructorHandler
{
    public class GetInstructorByIdHandler : IRequestHandler<GetInstructorByIdQuery, InstructorDTO>
    {
       private readonly IInstructorQueries _instructorQueries;

        public GetInstructorByIdHandler(IInstructorQueries instructorQueries)
        {
            _instructorQueries = instructorQueries;
        }

        public async Task<InstructorDTO> Handle(GetInstructorByIdQuery request, CancellationToken cancellationToken)
        {
            ResultQC<InstructorDTO> result = await _instructorQueries.GetInstructorByID(request.Id);

            if(result.Result == null)
                throw new Exception("Il professore non e' stato trovato");

            var instructor = result.Result;

            return instructor;

            /*if(studente != null)
                return Task.FromResult(new StudentDTO(studente.FirstMidName, studente.LastName));

            throw new Exception();*/

            //return Task.FromResult(new StudentDTO()); 

        }
    }
}
