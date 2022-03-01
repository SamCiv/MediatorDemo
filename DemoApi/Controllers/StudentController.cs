using DemoLibrary.Commands.StudentCommand;
using DemoLibrary.Models;
using DemoLibrary.StudentQueries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
        public async Task<List<StudentDTO>> Get()
        {

            var risposta = await _mediator.Send(new GetStudentListQuery());

            return risposta;

        }

        [HttpGet("{id}")]
        public async Task<StudentDTO> Get(int id)
        {
                        
            var risposta = await _mediator.Send(new GetStudentByIdQuery(id));

            return risposta;
        }

        [HttpDelete("{id}")]


        public async Task<IActionResult> Delete(int id)
        {
            bool success = false;
            try
            {
                success = await _mediator.Send(new DeleteStudentByIdCommand(id));
            }
            catch (Exception ex)
            {

            }

            if(success)
                return Ok();

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Post(StudentDTO studentDTO) //, [FromServices] StudentDTO student) //[FromBody]
        {

            if (ModelState.IsValid)
            {
                await _mediator.Send(new AddStudentCommand(studentDTO));
            }

            return Ok();
        }
    }
}

