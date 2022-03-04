using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoLibrary.Exceptions.CommandException
{
    [Serializable]
    public class AddStudentCommandException : Exception
    {
       // public const string Title = "Errore Inserimento Studente";

        public string Title { get; } = "Errore inserimento studente";

        public AddStudentCommandException(string message) : base(message)
        {
            
        }

        

    }
}
