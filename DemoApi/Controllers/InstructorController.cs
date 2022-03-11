using DemoApi.Application.DTO;
using DemoApi.Application.Commands.InstructorCommand;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DemoApi.Application.Queries.InstructorQuery;
using DemoApi.Application.Commands;

namespace DemoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InstructorController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        [ProducesResponseType(typeof(InstructorDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get()
        {
            var risposta = await _mediator.Send(new GetInstructorListQuery());

            if (risposta.IsSuccess && risposta.Result != null) //lo studente e' stato trovato nel DB
                return Ok(risposta.Result);

            if (risposta.IsSuccess && risposta.Result == null) //lo studente non e' stato trovato nel DB, MA la richiesta non ha prodotto errori interni
                return NotFound();

            return BadRequest();

        }


        [HttpGet("{instructorId:int}")]
        [ProducesResponseType(typeof(InstructorDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int instructorId)
        {
            var risposta = await _mediator.Send(new GetInstructorByIdQuery(instructorId));

            if (risposta != null) //lo studente e' stato trovato nel DB
                return Ok(risposta);

            return NotFound();
           
        }


        [HttpDelete("{instructorId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int instructorId, [FromHeader(Name = "x-request-id")] string requestId)
        {
            bool risposta = false;
            //81a130d2-502f-4cf1-a376-63edeb000e9f esempio

            if (Guid.TryParse(requestId, out Guid id) && id != Guid.Empty)
            {
                var command = new DeleteInstructorByIdCommand(instructorId);

                var identifiedCommand = new IdentifiedCommand<DeleteInstructorByIdCommand, bool>(command, id);

                risposta = await _mediator.Send(identifiedCommand);

            }

            if (risposta)
                return Ok();

            return NotFound();

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(InstructorDTO InstructorDTO, [FromHeader(Name = "x-request-id")] string requestId) //, [FromServices] InstructorDTO student) //[FromBody]
        {
            bool risposta = false;

            //81a130d2-502f-4cf1-a376-63edeb000e9f esempio
            if (Guid.TryParse(requestId, out Guid id) && id != Guid.Empty && ModelState.IsValid)
            {

                var student = new AddInstructorCommand(InstructorDTO);

                var identifiedCommand = new IdentifiedCommand<AddInstructorCommand, bool>(student, id);

                risposta = await _mediator.Send(identifiedCommand);

            }

            if (risposta)
                return Ok();

            return NotFound();

        }

        [HttpPut("{instructorId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int instructorId, InstructorDTO InstructorDTO, [FromHeader(Name = "x-request-id")] string requestId)
        {
            bool risposta = false;

            if (Guid.TryParse(requestId, out Guid id) && id != Guid.Empty && instructorId > 0)
            {

                var student = new UpdateInstructorCommand(InstructorDTO, instructorId);

                var identifiedCommand = new IdentifiedCommand<UpdateInstructorCommand, bool>(student, id);

                risposta = await _mediator.Send(identifiedCommand);

            }

            if (risposta)
                return Ok();

            return NotFound();

        }
    }
}
