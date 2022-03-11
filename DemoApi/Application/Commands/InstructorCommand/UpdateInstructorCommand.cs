using DemoApi.Application.DTO;
using MediatR;
using System.Runtime.Serialization;

namespace DemoApi.Application.Commands.InstructorCommand
{
    [DataContract]
    public record UpdateInstructorCommand : IRequest<bool>
    {
        [DataMember]
        public InstructorDTO Instructor { get; private set; }

        [DataMember]
        public int Id { get; private set; }

        public UpdateInstructorCommand(InstructorDTO instructor, int id)
        {
            Instructor = instructor;
            Id= id;
        }

    }
}
