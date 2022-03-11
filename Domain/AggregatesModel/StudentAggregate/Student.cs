using Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.AggregatesModel.StudentAggregate
{
    //POCO Class: non deve avere dipendenze da EF
    //Le navigation property devono essere readonly e modificabili solo all'interno della stessa classe.
    //Se devo aggiungere uno studente devo usare un apposito metodo di tale classe.
    //Posso configurare, tramite DataAnnotations, le proprieta da mappare nel DB ma per non "inquinare" il codice verra' fatto nel layer INfrastructure
    public class Student : IAggregateRoot
    {
        public int ID { get; private set; }

        public string FirstMidName { get; private set; }

        public string LastName { get; private set; }    
        
        public DateTime EnrollmentDate { get; private set; }
        
        public string FullName
        {
            get
            {
                return LastName + ", " + FirstMidName;
            }
        }

        //public ICollection<Enrollment> Enrollments { get; set; } //deve essere readonly

        public Student(string firstMidName, string lastName, DateTime enrollmentDate) 
        {
            FirstMidName = firstMidName;
            LastName = lastName;
            EnrollmentDate = enrollmentDate;
        }

        public void Update(string firstMidName, string lastName, DateTime enrollmentDate)
        {
            if(firstMidName != FirstMidName)
                FirstMidName = firstMidName;

            if(lastName != LastName)
                LastName = lastName;

            if (enrollmentDate != EnrollmentDate)
                EnrollmentDate = enrollmentDate;
        }
    }
}
