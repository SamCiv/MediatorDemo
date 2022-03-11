using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApi.Application.Behaviors
{
    public class ValidatorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse> //where TResponse : class
    {

        //public readonly IValidator<TRequest>[] _validators; //si puo usare anche IEnumerable
        public readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidatorBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }
        
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
         
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request); //descrive il context in cui viene eseguito il controllo di convalida
                
                //risultati della validazione
                var validationResults = await Task.WhenAll( 
                    _validators.Select(validator => validator.ValidateAsync(context, cancellationToken)));
                /*var select = validationResults
                                .Select(r => r.Errors);
                var selectMany = validationResults.SelectMany(result => result.Errors).GroupBy(e => e.PropertyName);*/
               //lista contenente gli errori

               var failures = validationResults
                                .SelectMany(r => r.Errors) //per ogni result r prendo la lista gli errori
                                .Where(f => f != null) //prendo quelli non nulli
                                .ToList(); //esegue la query

                if (failures.Count != 0)
                    //throw new InputValidationException(failures);            
                    throw new ValidationException("Sono stati riscontrati degli errori nei dati forniti.", failures);
                //return Activator.CreateInstance(typeof(TResponse), failures.ToList()) as TResponse;

                //return Activator.CreateInstance(typeof(TResponse)) as TResponse;
            }
            
            return await next();
        }
    }
}
