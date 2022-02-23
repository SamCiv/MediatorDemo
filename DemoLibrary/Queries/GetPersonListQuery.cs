using DemoLibrary.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoLibrary.Queries
{
    public record GetPersonListQuery() : IRequest<List<PersonModel>>; //esempio con record, List<PersonModel> e cio che ritorna la request

    //esempio con la classe

    //public class GetPersonListQueryClass : IRequest<List<PersonModel>>
    //{ }

}
