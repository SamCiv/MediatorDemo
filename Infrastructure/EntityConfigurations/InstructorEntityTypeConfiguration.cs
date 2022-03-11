using Domain.AggregatesModel.InstructorAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infrastructure.EntityConfigurations
{
    internal class InstructorEntityTypeConfiguration : IEntityTypeConfiguration<Instructor>
    {
        public void Configure(EntityTypeBuilder<Instructor> instructorConfiguration)
        {
            instructorConfiguration.ToTable("Instructors");
             instructorConfiguration.HasKey(s => s.ID);
             
            //.UsePropertyAccessMode(PropertyAccessMode.Field) //Usa gli attributi come colonne anziche' le properties!

             instructorConfiguration.
                Property<string>("FirstName")
               .IsRequired();

             instructorConfiguration.
                Property<string>("LastName")
               .IsRequired();

             instructorConfiguration.
                Property<DateTime>("EnrollmentDate")
               .IsRequired();
        }
    }
}
