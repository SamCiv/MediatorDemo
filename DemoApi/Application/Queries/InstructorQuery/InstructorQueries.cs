
using DemoApi.Application.DTO;
using Infrastructure;
using Microsoft.EntityFrameworkCore;


namespace DemoApi.Application.Queries.InstructorQuery
{
    public class InstructorQueries : IInstructorQueries
    {
        private readonly SchoolContext _context;

        public InstructorQueries(SchoolContext context)
        {
            _context = context;
        }

        public Task<ResultQC<InstructorDTO>> GetInstructorByID(int id)
        {
            var instructor = _context.Instructors.Where(s=> s.ID == id).FirstOrDefault();

            if (instructor != null)
            {
                var res = ResultQC<InstructorDTO>.Success(new InstructorDTO(instructor.FirstName, instructor.LastName, instructor.EnrollmentDate));

                return Task.FromResult(res);
            }

            return Task.FromResult(ResultQC<InstructorDTO>.Success());

            //throw new KeyNotFoundException($"L'utente con id:{id} non e' presente nel DB.");
        }

        

        public async Task<ICollection<InstructorDTO>> GetInstructorList()
        {
            ICollection<InstructorDTO> instructors = await _context.Instructors.Select( inst => new InstructorDTO(inst.FirstName, inst.LastName, inst.EnrollmentDate) ).ToListAsync();

            return instructors;
            
        }
    }
}
