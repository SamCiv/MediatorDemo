using DemoApi.Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApi.Application.Queries.InstructorQuery
{
    //Ritorno la lista degli studenti presenti nel DB 
    public record GetInstructorListQuery : IRequest<ResultQC<ICollection<InstructorDTO>>>;

}
