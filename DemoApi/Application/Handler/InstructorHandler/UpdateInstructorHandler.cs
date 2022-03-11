using DemoApi.Application.Commands.InstructorCommand;
using Domain.AggregatesModel.InstructorAggregate;
using MediatR;

namespace DemoApi.Application.Handler.InstructorHandler
{
    public class UpdateInstructorHandler : IRequestHandler<UpdateInstructorCommand, bool>
    {
        private readonly IInstructorRepository _repository;

        public UpdateInstructorHandler(IInstructorRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(UpdateInstructorCommand request, CancellationToken cancellationToken)
        {
            Instructor instructor = await _repository.GetAsync(request.Id);

            if (instructor == null)
                return false;

            instructor.Update(request.Instructor.FirstName, request.Instructor.LastName, request.Instructor.HireDate);

            _repository.Update(instructor);
            
            await _repository.UnitOfWork.SaveEntitiesAsync();

            return true;
        }
    }
}
