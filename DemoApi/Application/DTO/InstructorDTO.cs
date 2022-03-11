using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApi.Application.DTO
{
    public class InstructorDTO
    {
              
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime HireDate { get; set; }

        public InstructorDTO()
        {
        }
        public InstructorDTO(string lastName, string firstName, DateTime hireDate)
        {
            LastName = lastName;
            FirstName = firstName;
            HireDate = hireDate;
        }
            
    }
}
