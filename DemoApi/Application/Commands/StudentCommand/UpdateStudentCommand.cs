using DemoApi.Application.DTO;
using MediatR;
using System.Runtime.Serialization;

namespace DemoApi.Application.Commands.StudentCommand
{
    [DataContract]
    public record UpdateStudentCommand : IRequest<bool>
    {
        [DataMember]
        public StudentDTO Student { get; private set; }

        [DataMember]
        public int Id { get; private set; }

        public UpdateStudentCommand(StudentDTO student, int id)
        {
            Student = student;
            Id= id;
        }

    }
}
