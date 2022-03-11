using DemoApi.Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApi.Application.Queries.InstructorQuery
{
 
    public record GetInstructorByIdQuery(int Id) : IRequest<InstructorDTO>;

   
}
