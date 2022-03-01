using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoLibrary.Commands.StudentCommand;
using DemoLibrary.Context;
using System.Web.Http;
using System.Net;

namespace DemoLibrary.Handler.StudentHandler
{
    public class DeleteStudentByIdHandler : IRequestHandler<DeleteStudentByIdCommand, bool>
    {
        private ISchoolContext _context;

        public DeleteStudentByIdHandler(ISchoolContext context){
                    
            _context = context;
        }   

        public async Task<bool> Handle(DeleteStudentByIdCommand request, CancellationToken cancellationToken)
        {
            bool result = false;
            
                var studente = _context.Students.Where(s => s.ID == request.Id).FirstOrDefault();

            if (studente != null)
            {
                _context.Students.Remove(studente);

                await _context.SaveChangesAsync();

                result = true;
            }

            throw new HttpResponseException(HttpStatusCode.NotFound);
            //return result;
        }
    }

/*    public class DeleteStudentByIdCommandExceptionHandler : RequestExceptionHandler<SaveUserCommand, MembershipCreateStatus, Exception>
    {
        protected override void Handle(SaveUserCommand request, Exception exception, RequestExceptionHandlerState<MembershipCreateStatus> state)
        {
            state.SetHandled(MembershipCreateStatus.Failure); //handled
                                                              //request.Message = exception.Message + " Not Handled";
        }
    }*/
}
