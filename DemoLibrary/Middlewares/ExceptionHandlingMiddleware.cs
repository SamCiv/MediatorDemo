﻿using DemoLibrary.Exceptions;
using DemoLibrary.Exceptions.CommandException;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace DemoLibrary.Middlewares
{
    public sealed class ExceptionHandlingMiddleware : IMiddleware
    {
        //gli passo il context e Delegate contenete il next
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
           
        }

        public async Task HandleException(HttpContext context, Exception e)
        {

            var statusCode = GetStatusCode(e); //prendo lo status code dell'eccezione

            //string? message = GetErrors(e);

            var response = new
            {
                title = GetTitle(e),
                status = statusCode,
                detail = e.Message,
                errors = GetErrors(e)
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            string risultato = JsonSerializer.Serialize(response);

            await context.Response.WriteAsync(risultato);
        }

        //ritorna lo statusCode da usare nell'errore interrogando StatusCodes
        private static int GetStatusCode(Exception exception)
        {
            int statusCode;

            switch (exception)
            {
                case AddStudentCommandException ex:
                    statusCode = StatusCodes.Status400BadRequest;
                    break;

                case InputValidationException ex:
                    statusCode = StatusCodes.Status400BadRequest;
                    break;

                default:
                    statusCode = StatusCodes.Status500InternalServerError;
                    break;
            }

            return statusCode;
        }

        private static string GetTitle(Exception exception)
        {
            switch (exception)
            {
                case AddStudentCommandException ex:
                    return ex.Title;

                case InputValidationException ex:
                    return ex.Title;

                default:
                    return "Server Error";
            }
        }

        private static IReadOnlyDictionary<string, string[]>? GetErrors(Exception exception) //Errors e' un elemento dizionario che contiente gli errori relativi ad un exception
        {
            //IReadOnlyDictionary<string, string[]> errors = null; //caso in cui l'exception non abbia informazioni sull errore
            IEnumerable<ValidationFailure> errors = null;

            if (exception is ValidationException validationException)
            {  //is-then-cast
                errors = validationException.Errors;

                var res = errors.GroupBy(e => e.PropertyName, e => e.ErrorMessage,
                    (propertyName, errorMessage) =>
                    new
                    {
                        Key = propertyName,
                        Values = errorMessage.Distinct().ToArray()
                    }).ToDictionary(x => x.Key, x => x.Values);

                return res;
            }

            return null;
        }
    }
}
