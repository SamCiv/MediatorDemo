using Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.AggregatesModel.StudentAggregate
{
    public interface IStudentRepository : IRepository<Student> //Usa il marker interface IAggregateRoot
    {
        Student Add(Student student);

        void Delete(Student student);

        void Update(Student student);

        Task<Student> GetAsync(int studentId);
    }
}
