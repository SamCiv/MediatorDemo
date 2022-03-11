using DemoApi.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApi.Application.Queries
{
    public interface IStudentQueries
    {
        public Task<ICollection<StudentDTO>> GetStudentList();

        public Task<ResultQC<StudentDTO>> GetStudentByID(int id);
    }
}
