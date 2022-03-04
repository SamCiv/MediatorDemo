using DemoLibrary.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoLibrary.StudentQueries
{
    //Ritorno la lista degli studenti presenti nel DB 
    public record GetStudentListQuery : IRequest<ResultQC<List<StudentDTO>>>;

}
