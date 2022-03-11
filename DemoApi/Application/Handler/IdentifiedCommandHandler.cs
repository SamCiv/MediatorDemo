
using DemoApi.Application.Commands;
using DemoApi.Application.Commands.StudentCommand;
using Infrastructure.Idempotency;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApi.Application.Handler
{
    //Come richiesta prende IdentifiedCommand e restituisce R
    public class IdentifiedCommandHandler<T, R> : IRequestHandler<IdentifiedCommand<T, R>, R> where T : IRequest<R>
    {
        private readonly IMediator _mediator;
        private readonly IRequestManager _requestmanager; //Gestisce le richieste e tiene traccia di esse nel DB

        public IdentifiedCommandHandler(IMediator mediator, IRequestManager requestmanager)
        {
            _mediator = mediator;
            _requestmanager = requestmanager;
        }

        //messaggio contiene il Command (request) da eseguire. Bisogna vedere se tale request e' stata gia' presa in carica o meno
        public async Task<R> Handle(IdentifiedCommand<T, R> messaggio, CancellationToken cancellationToken)
        {
            
            bool controllo = await _requestmanager.ExistsAsync(messaggio.Id); //Se true il comando esiste gia'

            if (controllo)
            {
                return CreaRisultatoPerRichiestaDuplicata();
            }

            else
            {
                await _requestmanager.CreateRequestForCommandAsync<T>(messaggio.Id);

                try
                {
                    var command = messaggio.Command;

                    //StampaInfo(ref command);                    

                    var result = await _mediator.Send(command, cancellationToken);

                    return result;
                }
                catch //Nel caso sussiste un Exception nel salvataggio, ovvero negli handler
                {
                    throw; //l'errore viene gestito dal middleware
                    //return default(R);
                }
            }                

        }

        protected virtual R CreaRisultatoPerRichiestaDuplicata()
        {
            return default(R);
        }

        //Stampa le info per il logging ----> da Fare

        protected void StampaInfo(ref T command)
        {
            //var nameCommand = command.Get
            var idProperty = string.Empty;
            var commandId = string.Empty;

            switch (command) //per il LOGGING
            {
                case AddStudentCommand addStudentCommand:
                    idProperty = nameof(addStudentCommand.Student.FirstName);
                    commandId = addStudentCommand.Student.FirstName;
                    break;

                default:
                    idProperty = "Id?";
                    commandId = "N/A";
                    break;
            }
        }
    }
}
