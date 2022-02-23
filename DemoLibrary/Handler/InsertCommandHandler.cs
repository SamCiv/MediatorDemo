using DemoLibrary.Models;
using DemoLibrary.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoLibrary.DataAccess;

namespace DemoLibrary.Handler
{
    //Handler che inserisce la persona nella lista
    public class InsertCommandHandler : IRequestHandler<InsertPersonCommand, PersonModel>

    {
        private readonly IDataAccess _data;

        public InsertCommandHandler(IDataAccess data)
        {
            this._data = data;
        }

        public async Task<PersonModel> Handle(InsertPersonCommand request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_data.InsertPerson(request.FirstName, request.LastName)); //InsertPerson ritorna PersonModel
        }
    }
}
