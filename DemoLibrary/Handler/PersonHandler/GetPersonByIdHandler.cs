using DemoLibrary.DataAccess;
using DemoLibrary.Models;
using DemoLibrary.PersonQueries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoLibrary.PersonHandler
{
    public class GetPersonByIdHandler : IRequestHandler<GetPersonByIdQuery, PersonModel>
    {
        private readonly IMediator _mediator;

        //esempio in cui chiamo il mediator che va a chiamare un altro mediatr per ottenere la lista delle persone.
        //Avrei potuto anche reinterrogare la lista tramite IDataAccess

        public GetPersonByIdHandler(IMediator mediator)
        {
            this._mediator = mediator;
        }
        public async Task<PersonModel> Handle(GetPersonByIdQuery request, CancellationToken cancellationToken)
        {
            var results = await _mediator.Send(new GetPersonListQuery()); //ottengo la lista attraverso una Request di tipo GetPersonListQuery

            var person = results.FirstOrDefault( e => e.Id == request.Id );
            
            return person;
        }
    }
}
