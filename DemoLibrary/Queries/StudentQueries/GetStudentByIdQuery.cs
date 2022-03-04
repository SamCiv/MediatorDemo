using DemoLibrary.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoLibrary.StudentQueries
{
 
    public record GetStudentByIdQuery(int Id) : IRequest<StudentDTO>;

    public record GetStudentByIdQuery2(int Id) : IRequest<ResultQC<StudentDTO>>;

    /*   public class GetStudentByIdQuery : IRequest<StudentDTO>
    {
        public int Id { get; set; } //gli passo l'id dello studente 

        public GetStudentByIdQuery(int id)
        {
            Id = id;
        }
    }*/
}
