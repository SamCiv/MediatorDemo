using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using DemoApi.Application.DTO;

using MediatR;

namespace DemoApi.Application.Commands.InstructorCommand
{   
    //necessario per la serializzazione/des
    [DataContract] 
    public record AddInstructorCommand : IRequest<bool>
    {
        [DataMember] //necessario per la ser/deser, indica che questo dato fa parte del "contratto"
        public InstructorDTO Instructor { get; private set; } //i setter deveno essere privati per garantire l'immutabilita

        public AddInstructorCommand(InstructorDTO instructor)
        {
            Instructor = instructor;
        }
    }

}
