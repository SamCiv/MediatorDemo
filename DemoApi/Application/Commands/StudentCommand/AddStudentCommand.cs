using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using DemoApi.Application.DTO;

using MediatR;

namespace DemoApi.Application.Commands.StudentCommand
{   
    //necessario per la serializzazione/des
    [DataContract] 
    public record AddStudentCommand : IRequest<bool>//IRequest<ResultQC<bool>>
    {
        [DataMember] //necessario per la ser/deser, indica che questo dato fa parte del "contratto"
        public StudentDTO Student { get; private set; } //i setter deveno essere privati per garantire l'immutabilita

        public AddStudentCommand(StudentDTO student)
        {
            Student = student;
        }
    }

}
