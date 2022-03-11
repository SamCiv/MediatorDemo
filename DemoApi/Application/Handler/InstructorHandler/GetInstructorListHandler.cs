using MediatR;
using DemoApi.Application.DTO;
using DemoApi.Application.Queries.InstructorQuery;

namespace DemoApi.Application.Handler.InstructorHandler
{
    //Riceve una Request di tipo GetStudentListQuery e retituisce una List di Student
    public class GetInstructorListHandler : IRequestHandler<GetInstructorListQuery, ResultQC<ICollection<InstructorDTO>>>
    {
        private readonly IInstructorQueries _instructorQueries;

        public GetInstructorListHandler(IInstructorQueries instructorQueries)
        {
            _instructorQueries = instructorQueries;
        }

        public async Task<ResultQC<ICollection<InstructorDTO>>> Handle(GetInstructorListQuery request, CancellationToken cancellationToken)
        {
           
            ICollection<InstructorDTO> listaInstructor = await _instructorQueries.GetInstructorList();
          
           if(listaInstructor.Count > 0)
                return  ResultQC<ICollection<InstructorDTO>>.Success(listaInstructor);
           
           return ResultQC<ICollection<InstructorDTO>>.Success();           

        }
        
    }
}
