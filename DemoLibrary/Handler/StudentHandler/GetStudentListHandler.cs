using MediatR;
using DemoLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoLibrary.StudentQueries;
using DemoLibrary.Context;
using Microsoft.EntityFrameworkCore;

namespace DemoLibrary.StudentHandler
{
    //Riceve una Request di tipo GetStudentListQuery e retituisce una List di Student
    public class GetStudentListHandler : IRequestHandler<GetStudentListQuery, List<StudentDTO>>
    {
        private readonly ISchoolContext _context;

        public GetStudentListHandler(ISchoolContext context)
        {
            _context = context;
        }

        public async Task<List<StudentDTO>> Handle(GetStudentListQuery request, CancellationToken cancellationToken)
        {
            
            IQueryable<Student> studenti = _context.Students;  
            
            //List<Student> listaStudenti =
            //studenti.Select(s => new StudentDTO(s.FirstMidName, s.LastName)).ForEachAsync
            return await studenti.Select( s=> new StudentDTO(s.FirstMidName, s.LastName)).ToListAsync();

           /* List<StudentDTO> studentDTOs = new List<StudentDTO>();

            foreach (Student student in listaStudenti)
            {
                studentDTOs.Add(new StudentDTO(student.FirstMidName, student.LastName));
            }
            return studentDTOs;*/
            //return await Task.FromResult(listaStudenti);
        }
    }
}
