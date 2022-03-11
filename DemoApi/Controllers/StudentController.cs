using DemoApi.Application.Commands;
using DemoApi.Application.Commands.StudentCommand;
using DemoApi.Application.DTO;
using DemoApi.Application.Queries.StudentQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;
//using System.Web.Http;

namespace DemoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IMediator _mediator;        

        public StudentController(IMediator mediator)
        {
            _mediator = mediator;            
        }

        [HttpGet]
        [ProducesResponseType(typeof(StudentDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get()
        {
            var risposta = await _mediator.Send(new GetStudentListQuery());

            if (risposta.IsSuccess && risposta.Result != null) //lo studente e' stato trovato nel DB
                return Ok(risposta.Result);

            if (risposta.IsSuccess && risposta.Result == null) //lo studente non e' stato trovato nel DB, MA la richiesta non ha prodotto errori interni
                return NotFound();

            return BadRequest();                        

        }


        [HttpGet("{studentId:int}")]
        [ProducesResponseType(typeof(StudentDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int studentId)
        {
            var risposta = await _mediator.Send(new GetStudentByIdQuery2(studentId));

            if (risposta.IsSuccess && risposta.Result != null) //lo studente e' stato trovato nel DB
                return Ok(risposta.Result);

            if (risposta.IsSuccess && risposta.Result == null) //lo studente non e' stato trovato nel DB, MA la richiesta non ha prodotto errori interni
                return NotFound();

            return BadRequest();
        }


        [HttpDelete("{studentId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int studentId, [FromHeader(Name = "x-request-id")] string requestId)
        {
            bool risposta = false;
            //81a130d2-502f-4cf1-a376-63edeb000e9f esempio

            if(Guid.TryParse(requestId, out Guid id) && id != Guid.Empty)
            {
                var command = new DeleteStudentByIdCommand(studentId);

                var identifiedCommand = new IdentifiedCommand<DeleteStudentByIdCommand, bool>(command, id);

                risposta = await _mediator.Send(identifiedCommand);            
                                
            }
            
            if(risposta)
                return Ok();            
                
            return NotFound();
            
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(StudentDTO studentDTO, [FromHeader(Name = "x-request-id")] string requestId) //, [FromServices] StudentDTO student) //[FromBody]
        {
            bool risposta = false;

            //81a130d2-502f-4cf1-a376-63edeb000e9f esempio
            if (Guid.TryParse(requestId, out Guid id) && id != Guid.Empty && ModelState.IsValid)            
            {
                
                var student = new AddStudentCommand(studentDTO);
                
                var identifiedCommand = new IdentifiedCommand<AddStudentCommand, bool>(student, id);

                risposta = await _mediator.Send(identifiedCommand); 
                
            }

            if(risposta)
                    return Ok();           

            return NotFound();             
            
        }

        [HttpPut("{studentId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int studentId, StudentDTO studentDTO,[FromHeader(Name ="x-request-id")] string requestId)
        {
            bool risposta = false;

            if (Guid.TryParse(requestId, out Guid id) && id != Guid.Empty && studentId > 0)
            {

                var student = new UpdateStudentCommand(studentDTO, studentId);

                var identifiedCommand = new IdentifiedCommand<UpdateStudentCommand, bool>(student, id);

                risposta = await _mediator.Send(identifiedCommand);

            }

            if (risposta)
                return Ok();

            return NotFound();

        }

    }
}

/*        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var risposta = await _mediator.Send(new GetStudentByIdQuery(id));

                return Ok(risposta);

            }catch (Exception ex)
            {
                return NotFound();
            }            


        }*/

/*        [HttpGet("{id}")]        
        public async Task<StudentDTO> Get(int id)
        {

            var risposta = await _mediator.Send(new GetStudentByIdQuery(id));

            if (string.IsNullOrEmpty(risposta.FirstName))
            {

                HttpResponseMessage messaggio = new HttpResponseMessage(HttpStatusCode.NotFound);
                messaggio.Content = new StringContent("PROVA");// $"L'utente con id:{id} non e' presente nel DB.");

                throw new System.Web.Http.HttpResponseException(messaggio); //per evitare l'ambiguita'

            }

            return risposta;
        }*/


/*        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {

            var risposta = await _mediator.Send(new GetStudentByIdQuery(id));

            if (string.IsNullOrEmpty(risposta.FirstName))
            {

                HttpResponseMessage messaggio = new HttpResponseMessage(HttpStatusCode.NotFound);
                messaggio.Content = new StringContent($"L'utente con id:{id} non e' presente nel DB.");               

                throw new System.Web.Http.HttpResponseException(messaggio); //per evitare l'ambiguita'
            }

            return Ok();

            //return Request.CreateResponse(HttpStatusCode.OK, risposta);
}*/