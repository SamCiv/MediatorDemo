using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Web.Http;
using System.Net;
using DemoApi.Application.Commands.StudentCommand;
using Domain.AggregatesModel.StudentAggregate;

namespace DemoApi.Application.Handler.StudentHandler
{
    public class DeleteStudentByIdHandler : IRequestHandler<DeleteStudentByIdCommand, bool>
    {
        private readonly IStudentRepository _studentRepository;

        public DeleteStudentByIdHandler(IStudentRepository studentRepository){
                    
            _studentRepository = studentRepository;
        }   

        public async Task<bool> Handle(DeleteStudentByIdCommand request, CancellationToken cancellationToken)
        {
            bool result = false;

            var studente = await _studentRepository.GetAsync(request.StudentId); //_context.Students.Where(s => s.ID == request.Id).FirstOrDefault();

            if (studente != null) //lo studente e' stato trovato
            {
                _studentRepository.Delete(studente);
                
                await _studentRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

                result = true;                
            }

            return result;

            //throw new HttpResponseException(HttpStatusCode.NotFound);
            //return result;
        }
    }


}
