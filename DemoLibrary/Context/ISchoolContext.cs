using DemoLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DemoLibrary.Context
{
    public interface ISchoolContext
    {
        DbSet<Course> Courses { get; set; }
        DbSet<Department> Departments { get; set; }
        DbSet<Enrollment> Enrollments { get; set; }
        DbSet<Instructor> Instructors { get; set; }
        DbSet<OfficeAssignment> OfficeAssignments { get; set; }
        DbSet<Student> Students { get; set; }

        Task<int> SaveChangesAsync();

        EntityEntry Add(Object entity);
    }
}