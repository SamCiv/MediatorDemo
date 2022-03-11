using Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.AggregatesModel.InstructorAggregate
{
    public interface IInstructorRepository : IRepository<Instructor>
    {
        Instructor Add(Instructor instructor);

        void Delete(Instructor instructor);

        void Update(Instructor instructor);

        Task<Instructor> GetAsync(int instructorId);
    }
}
