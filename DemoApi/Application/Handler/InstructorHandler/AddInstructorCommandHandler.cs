using DemoApi.Application.Commands.InstructorCommand;
using DemoApi.Application.Commands.StudentCommand;
using Domain.AggregatesModel.InstructorAggregate;
using Domain.AggregatesModel.StudentAggregate;
using MediatR;

namespace DemoApi.Application.Handler.InstructorHandler
{

    public class AddInstructorCommandHandler : IRequestHandler<AddInstructorCommand, bool>
    {
        
        private readonly IInstructorRepository _repository; //nel Repository sono contenute le azioni che possono esserre eseguite

        public AddInstructorCommandHandler(IInstructorRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(AddInstructorCommand request, CancellationToken cancellationToken)
        {
            
            Instructor instructor = new Instructor(request.Instructor.FirstName, request.Instructor.LastName, request.Instructor.HireDate);

            _repository.Add(instructor);

            await _repository.UnitOfWork.SaveEntitiesAsync();

            return true;

        }
  
    }
}
