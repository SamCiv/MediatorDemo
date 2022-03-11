using Domain.AggregatesModel.StudentAggregate;
using Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class StudentRepository : IStudentRepository //IStudentRepository e' contenuto nel modello
    {
        private readonly SchoolContext _context;
       
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public StudentRepository(SchoolContext context)
        {
            _context = context;
        }

        public Student Add(Student student)
        {
            return _context.Students.Add(student).Entity;            
        }

        //Questo e' un comando di Query. Viene creato per vedere se uno studente con un determinato id e' presente nel DB.
        //utile nel caso devo fare un update e voglio vedere se effettivamente l'utente esiste nel DB.
        public async Task<Student> GetAsync(int studentId) 
        {
            var student = _context.Students.Where(s => s.ID == studentId).FirstOrDefault();            

            return await Task.FromResult(student);            
        }

        public void Update(Student student)
        {
            _context.Students.Update(student);            
        }

        public void Delete(Student student)
        {
            _context.Remove(student);
        }
    }
}
