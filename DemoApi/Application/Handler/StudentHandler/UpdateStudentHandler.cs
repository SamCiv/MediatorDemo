using DemoApi.Application.Commands.StudentCommand;
using Domain.AggregatesModel.StudentAggregate;
using MediatR;

namespace DemoApi.Application.Handler.StudentHandler
{
    public class UpdateStudentHandler : IRequestHandler<UpdateStudentCommand, bool>
    {
        private readonly IStudentRepository _repository;

        public UpdateStudentHandler(IStudentRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            Student studente = await _repository.GetAsync(request.Id);

            if (studente == null)
                return false;

            studente.Update(request.Student.FirstName, request.Student.LastName, request.Student.EnrollmentDate);

            _repository.Update(studente);
            
            await _repository.UnitOfWork.SaveEntitiesAsync();

            return true;
        }
    }
}
