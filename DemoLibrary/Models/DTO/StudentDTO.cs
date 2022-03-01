using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoLibrary.Models
{
    public class StudentDTO
    {
        //Le DataAnnotation ci permettono di validare i dati in ingresso nel nostro controller

        [Required]//(ErrorMessage = "LastName richiesto!")]        
        public string LastName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public DateTime EnrollmentDate { get; set; }      

        public StudentDTO() { }

        public StudentDTO(string last, string first)
        {
            LastName = last;
            FirstName = first;            
        }

        public StudentDTO(string last, string first, DateTime enrollmentDate)
        {
            LastName = last;
            FirstName = first;
            EnrollmentDate = enrollmentDate;
        }
    }
}
