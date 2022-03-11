//using DemoLibrary.Context;
using DemoApi.Application.DTO;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApi.Application.Queries
{
    public class StudentQueries : IStudentQueries
    {
        private readonly SchoolContext _context;

        public StudentQueries(SchoolContext context)
        {
            _context = context;
        }

        public Task<ResultQC<StudentDTO>> GetStudentByID(int id)
        {
            var student = _context.Students.Where(s=> s.ID == id).FirstOrDefault();

            if (student != null)
            {
                var res = ResultQC<StudentDTO>.Success(new StudentDTO(student.FirstMidName, student.LastName));

                return Task.FromResult(res);
            }

            return Task.FromResult(ResultQC<StudentDTO>.Success());

            //throw new KeyNotFoundException($"L'utente con id:{id} non e' presente nel DB.");
        }

        

        public async Task<ICollection<StudentDTO>> GetStudentList()
        {
            ICollection<StudentDTO> students = await _context.Students.Select( stud => new StudentDTO(stud.FirstMidName, stud.LastName, stud.EnrollmentDate) ).ToListAsync();

            return students;
            
        }
    }
}
