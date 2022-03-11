using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Web.Http;
using System.Net;
using Domain.AggregatesModel.InstructorAggregate;
using DemoApi.Application.Commands.InstructorCommand;

namespace DemoApi.Application.Handler.InstructorHandler
{
    public class DeleteInstructorByIdHandler : IRequestHandler<DeleteInstructorByIdCommand, bool>
    {
        private readonly IInstructorRepository _instructorRepository;

        public DeleteInstructorByIdHandler(IInstructorRepository instructorRepository)
        {
                    
            _instructorRepository = instructorRepository;
        }   

        public async Task<bool> Handle(DeleteInstructorByIdCommand request, CancellationToken cancellationToken)
        {
            bool result = false;

            var instructor = await _instructorRepository.GetAsync(request.InstructorId); 

            if (instructor != null)
            {
                _instructorRepository.Delete(instructor);
                
                await _instructorRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

                result = true;                
            }

            return result;

            //throw new HttpResponseException(HttpStatusCode.NotFound);
            //return result;
        }
    }


}
