using DemoLibrary.Exceptions;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoLibrary.Behaviors
{
    public class ValidatorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse> where TResponse : class
    {

        //public readonly IValidator<TRequest>[] _validators; //si puo usare anche IEnumerable
        public readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidatorBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }
        
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var responseType = typeof(TResponse);
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                var validationResults = await Task.WhenAll(
                    _validators.Select(validator => validator.ValidateAsync(context, cancellationToken)));

                /*var failures = validationResults.SelectMany(r => r.Errors)
                    .Where(f => f != null)
                    .ToList();*/

                var failures = validationResults.SelectMany(r => r.Errors) //per ogni result r prendo gli errori
                                .Where(f => f != null) //prendo quelli non nulli
                                /*.GroupBy(x => x.PropertyName,
                                      x => x.ErrorMessage,
                                      (propertyName, errorMessages) => new
                                      {
                                        Key = propertyName,
                                        Values = errorMessages.Distinct().ToArray()
                                      })*/
                                //.ToDictionary(x => x.Key, x => x.Values);
                                .ToList();

                if (failures.Count != 0)
                    throw new InputValidationException(failures);                    

                //return Activator.CreateInstance(typeof(TResponse), failures.ToList()) as TResponse;

                //return Activator.CreateInstance(typeof(TResponse)) as TResponse;
            }
            
            return await next();
        }
    }
}
