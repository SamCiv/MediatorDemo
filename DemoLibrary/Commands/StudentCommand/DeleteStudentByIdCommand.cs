using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoLibrary.Commands.StudentCommand
{
    public record DeleteStudentByIdCommand(int Id) : IRequest<bool>;
/*    
 *    
 *    {
        public int Id { get; set; }

        public DeleteStudentByIdCommand(int id)
        {
            Id = id;
        }
    }*/
}
