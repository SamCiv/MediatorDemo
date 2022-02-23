using DemoLibrary.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoLibrary.Commands
{
    //esempio di command, quindi effettuo un inserimento nella mia lista
    //passo firstname e lastname e ritorno un personModel. Potrei anche evitarlo ma lo faccio per visualizzare il dato ottenuto
    public record InsertPersonCommand(string FirstName, string LastName) : IRequest<PersonModel>;
    
    /*public class InsertPersonCommand : IRequest<PersonModel>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public InsertPersonCommand(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }*/
}
