using DemoApi.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApi.Application.Queries.InstructorQuery
{
    public interface IInstructorQueries
    {
        public Task<ICollection<InstructorDTO>> GetInstructorList();

        public Task<ResultQC<InstructorDTO>> GetInstructorByID(int id);
    }
}
