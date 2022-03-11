using Domain.AggregatesModel.StudentAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EntityConfigurations
{
    public class StudentEntityTypeConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> studentConfiguration) //Da implementare
        {
            studentConfiguration.ToTable("Students");
            studentConfiguration.HasKey(s => s.ID);
            studentConfiguration.Ignore(s => s.FullName);
            //.UsePropertyAccessMode(PropertyAccessMode.Field) //Usa gli attributi come colonne anziche' le properties!

            studentConfiguration.
                Property<string>("FirstMidName")
               .IsRequired();

            studentConfiguration.
                Property<string>("LastName")
               .IsRequired();

            studentConfiguration.
                Property<DateTime>("EnrollmentDate")
               .IsRequired();

        }
    }
}
