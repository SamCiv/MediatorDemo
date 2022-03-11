using Domain.AggregatesModel.InstructorAggregate;
using Domain.AggregatesModel.StudentAggregate;
using Domain.Base;
using Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class SchoolContext : DbContext, IUnitOfWork //ISchoolContext,
    {

        public DbSet<Student> Students { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
       /* public DbSet<Course> Courses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<OfficeAssignment> OfficeAssignments { get; set; }*/


        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //per gestire le relazioni
            /*modelBuilder.Entity<Course>().ToTable(nameof(Course))
                .HasMany(c => c.Instructors)
                .WithMany(i => i.Courses);
            modelBuilder.Entity<Student>().ToTable(nameof(Student));
            modelBuilder.Entity<Instructor>().ToTable(nameof(Instructor));*/

            modelBuilder.ApplyConfiguration(new StudentEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new InstructorEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ClientRequestEntityTypeConfiguration());
        }

       

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default) //usato per gli eventi
        {
         
            var result = await base.SaveChangesAsync(cancellationToken);

            return true;

        }
        
        
        /* public async Task<int> SaveChangesAsync() 
        {
            return await base.SaveChangesAsync();
        }

        public EntityEntry Add(Object obj)
        {
            return base.Add(obj);
        }*/
    }
}
