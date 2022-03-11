using MediatR;

namespace DemoApi.Application.Commands.StudentCommand
{
    public record DeleteStudentByIdCommand(int StudentId) : IRequest<bool>;
    
}
