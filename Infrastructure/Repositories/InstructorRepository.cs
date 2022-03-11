using Domain.AggregatesModel.InstructorAggregate;
using Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class InstructorRepository : IInstructorRepository
    {
        private readonly SchoolContext _context;
        public IUnitOfWork UnitOfWork
        {
            get { return _context; }
        }

        public InstructorRepository(SchoolContext context)
        {
            _context = context;
        }

        public Instructor Add(Instructor instructor)
        {
            return _context.Instructors.Add(instructor).Entity;
        }

        public void Delete(Instructor instructor)
        {
            _context.Instructors.Remove(instructor);
        }

        public async Task<Instructor> GetAsync(int instructorId)
        {
            var instructor = _context.Instructors.FirstOrDefault();

            return await Task.FromResult(instructor);
        }

        public void Update(Instructor instructor)
        {
            _context.Instructors.Update(instructor);
        }
    }
}
