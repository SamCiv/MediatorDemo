using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApi.Application.Commands
{
    //Classe DTO per l'esecuzione dei Command. Tale classe permette di implementare l'idempotenza
    public class IdentifiedCommand<T, R> : IRequest<R> where T : IRequest<R> //il parametro T deve implementare l'interfaccia IRequest<R> dove R e' il tipo di ritorno della richiesta. Per INfo vedere https://docs.microsoft.com/it-it/dotnet/csharp/language-reference/keywords/where-generic-type-constraint
    {
        public T Command { get; } //Command da eseguire
        public Guid Id { get; } //ID univoco della richiesta

        public IdentifiedCommand(T command, Guid id)
        {
            Command = command;
            Id = id;
        }
    }
}
