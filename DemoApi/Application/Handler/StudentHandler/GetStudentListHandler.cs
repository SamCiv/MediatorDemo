using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DemoApi.Application.Queries.StudentQuery;
using DemoApi.Application.DTO;
using DemoApi.Application.Queries;

namespace DemoApi.Application.Handler.StudentHandler
{
    //Riceve una Request di tipo GetStudentListQuery e retituisce una List di Student
    public class GetStudentListHandler : IRequestHandler<GetStudentListQuery, ResultQC<ICollection<StudentDTO>>>
    {
        private readonly IStudentQueries _studentQueries;

        public GetStudentListHandler(IStudentQueries studentQueries)
        {
            _studentQueries = studentQueries;
        }

        public async Task<ResultQC<ICollection<StudentDTO>>> Handle(GetStudentListQuery request, CancellationToken cancellationToken)
        {
           
            ICollection<StudentDTO> listaStudenti = await _studentQueries.GetStudentList();
          
           if(listaStudenti.Count > 0)
                return  ResultQC<ICollection<StudentDTO>>.Success(listaStudenti);
           
           return ResultQC<ICollection<StudentDTO>>.Success();           

        }

        /*public async Task<List<StudentDTO>> Handle(GetStudentListQuery request, CancellationToken cancellationToken)
        {
            
            IQueryable<Student> studenti = _context.Students;  


            return await studenti.Select( s=> new StudentDTO(s.FirstMidName, s.LastName, s.EnrollmentDate)).ToListAsync();

            //List<Student> listaStudenti =
            //studenti.Select(s => new StudentDTO(s.FirstMidName, s.LastName)).ForEachAsync
            

           *//* List<StudentDTO> studentDTOs = new List<StudentDTO>();

            foreach (Student student in listaStudenti)
            {
                studentDTOs.Add(new StudentDTO(student.FirstMidName, student.LastName));
            }
            return studentDTOs;*//*
            //return await Task.FromResult(listaStudenti);
        }*/
    }
}
