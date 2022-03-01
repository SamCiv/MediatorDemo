using DemoLibrary.Models;
using MediatR;
using DemoLibrary.PersonQueries;
using Microsoft.AspNetCore.Mvc;
using DemoLibrary.Commands;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DemoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PersonController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        // GET: api/<PersonController>
        [HttpGet]
        public async Task<List<PersonModel>> Get()
        {
            return await _mediator.Send(new GetPersonListQuery());
        }

        // GET api/<PersonController>/5
        [HttpGet("{id}")]
        public async Task<PersonModel> Get(int id) => await _mediator.Send(new GetPersonByIdQuery(id)); //string Get(int id)
        /* {
             //return "value";
             return await _mediator.Send(new GetPersonByIdQuery(id));
         }*/

        // POST api/<PersonController>
        [HttpPost]
        public async Task<PersonModel> Post([FromBody] PersonModel value) //string value)
        {
            var richiesta = new InsertPersonCommand(value.FirstName, value.LastName); //istanzia un oggetto di tipo InsertPersonModel, ma la richiesta viene fatta successivamente
            return await _mediator.Send(richiesta);
        }

        /*
        // PUT api/<PersonController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PersonController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
         */
    }
}
