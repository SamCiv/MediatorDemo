using Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.AggregatesModel.InstructorAggregate
{
    public class Instructor : IAggregateRoot
    {
        public int ID { get; private set; }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTime EnrollmentDate { get; private set; }


        public Instructor(string firstName, string lastName, DateTime enrollmentDate)
        {
            FirstName = firstName;
            LastName = lastName;
            EnrollmentDate = enrollmentDate;
        }

        public void Update(string firstName, string lastName, DateTime enrollmentDate)
        {
            if (firstName != FirstName)
                FirstName = firstName;

            if (lastName != LastName)
                LastName = lastName;

            if (enrollmentDate != EnrollmentDate)
                EnrollmentDate = enrollmentDate;
        }
    }
}
