using DemoLibrary.Commands.StudentCommand;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoLibrary
{
    public class DeleteStudentByExceptionHandler : RequestExceptionHandler<DeleteStudentByIdCommand, ResultQC<bool>, Exception>
    {
        private readonly ILogger<DeleteStudentByIdCommand> _logger;

        public DeleteStudentByExceptionHandler(ILogger<DeleteStudentByIdCommand> logger)
        {
            _logger = logger;
        }

        protected override void Handle(DeleteStudentByIdCommand request, Exception exception, RequestExceptionHandlerState<ResultQC<bool>> state)
        {
            //state.SetHandled(bool.Failure); //handled

            //state.SetHandled(false); //

            var name = typeof(DeleteStudentByIdCommand).Name;

            _logger.LogError($"Errore nell'eseguire la richiesta {name} con errore:{exception.Message}");


        }

        /* protected override void Handle(DeleteStudentByIdCommand request, Exception exception, RequestExceptionHandlerState<bool> state)
         {
             throw new NotImplementedException();
         }*/
    }

    public class RequestGenericExceptionHandler<TRequest, TResponse> : IRequestExceptionHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {

        private readonly ILogger<DeleteStudentByIdCommand> _logger;

        public RequestGenericExceptionHandler(ILogger<DeleteStudentByIdCommand> logger)
        {
            _logger = logger;
        }

        public Task Handle(TRequest request, Exception exception, RequestExceptionHandlerState<TResponse> state, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

}