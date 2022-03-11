using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
namespace DemoLibrary.DataAccess
{
    //non abbiamo un collegamento con il DB ma una semplice lista dove sono immagazzinati i nostri dati
    public class DemoDataAccess : IDataAccess
    {
        private List<PersonModel> people = new List<PersonModel>();

        public DemoDataAccess()
        {
            people.Add(new PersonModel { Id = 1, FirstName = "Ponzio", LastName = "Pilato" });
            people.Add(new PersonModel { Id = 2, FirstName = "Giulio", LastName = "Cesare" });
        }

        public List<PersonModel> GetPeople() //potrei chiamare il DB e poi ritornare la lista dei People
        {
            return people;
        }

        public PersonModel InsertPerson(string firstName, string lastName)
        {
            PersonModel person = new PersonModel() { FirstName = firstName, LastName = lastName };
            person.Id = people.Max(p => p.Id) + 1; //prendo il valore max dell'Id e gl iaggiungo 1
            people.Add(person);

            return person; //ritorno person poiche voglio vedere cio che ho inserito
        }
    }
}
*/