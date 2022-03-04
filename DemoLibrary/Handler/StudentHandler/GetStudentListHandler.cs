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
    public class GetStudentListHandler : IRequestHandler<GetStudentListQuery, ResultQC<List<StudentDTO>>>
    {
        private readonly ISchoolContext _context;

        public GetStudentListHandler(ISchoolContext context)
        {
            _context = context;
        }

        public async Task<ResultQC<List<StudentDTO>>> Handle(GetStudentListQuery request, CancellationToken cancellationToken)
        {
           var students = _context.Students;

           var listaStudenti = await students.Select(stud => new StudentDTO(stud.FirstMidName, stud.LastName, stud.EnrollmentDate)).ToListAsync();
           if(listaStudenti.Count > 0)
                return  ResultQC<List<StudentDTO>>.Success(listaStudenti);
           
           return ResultQC<List<StudentDTO>>.Success();           

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
