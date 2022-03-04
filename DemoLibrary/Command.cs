using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoLibrary
{
    //T input, R risposta
    public class IdentCommand<T, R> : IRequest<R> where T : IRequest<R>
    {
        public T Command { get; } //Command da eseguire
        public Guid Id { get; } //Id dell'operazione


        public IdentCommand(T command, Guid id)
        {
            Command = command;
            id = id;
        }

    }
}
