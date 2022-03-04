using DemoLibrary.Commands.StudentCommand;
using DemoLibrary.Models;
using DemoLibrary.StudentQueries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
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
        public async Task<IActionResult> Get()
        {
            var risposta = await _mediator.Send(new GetStudentListQuery());

            if (risposta.IsSuccess && risposta.Result != null) //lo studente e' stato trovato nel DB
                return Ok(risposta.Result);

            if (risposta.IsSuccess && risposta.Result == null) //lo studente non e' stato trovato nel DB, MA la richiesta non ha prodotto errori interni
                return NotFound();

            return BadRequest();                        

        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var risposta = await _mediator.Send(new GetStudentByIdQuery2(id));

            if (risposta.IsSuccess && risposta.Result != null) //lo studente e' stato trovato nel DB
                return Ok(risposta.Result);

            if (risposta.IsSuccess && risposta.Result == null) //lo studente non e' stato trovato nel DB, MA la richiesta non ha prodotto errori interni
                return NotFound();

            return BadRequest();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            //bool success = false;
            
            var risposta = await _mediator.Send(new DeleteStudentByIdCommand(id));            

            if(risposta.IsSuccess && risposta.Result)
                return Ok();            
            if(risposta.IsSuccess && !risposta.Result)
                return NotFound();

            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Post(StudentDTO studentDTO) //, [FromServices] StudentDTO student) //[FromBody]
        {
            if (ModelState.IsValid)
            {
                var risposta = await _mediator.Send(new AddStudentCommand(studentDTO));
                if(risposta.IsSuccess && risposta.Result)
                    return Ok();                
            }

            /*if (ModelState.IsValid)
            {
                await _mediator.Send(new AddStudentCommand(studentDTO));
                return Ok();
            }*/

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