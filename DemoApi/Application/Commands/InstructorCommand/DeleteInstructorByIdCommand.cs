using MediatR;

namespace DemoApi.Application.Commands.InstructorCommand
{
    public record DeleteInstructorByIdCommand(int InstructorId) : IRequest<bool>;
    
}
