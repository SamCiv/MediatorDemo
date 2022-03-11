using DemoApi.Application.Commands.StudentCommand;
using Domain.AggregatesModel.StudentAggregate;
using MediatR;

namespace DemoApi.Application.Handler.StudentHandler
{

    public class AddStudentCommandHandler : IRequestHandler<AddStudentCommand, bool>
    {
        
        private readonly IStudentRepository _repository; //nel Repository sono contenute le azioni che possono esserre eseguite

        public AddStudentCommandHandler(IStudentRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {
            //creo lo studente, utilizzando il costruttore dell'Aggregate. Qualsiasi altra operazione deve essere gestita dall'Aggregate
            Student student = new Student(request.Student.FirstName, request.Student.LastName, request.Student.EnrollmentDate);

            _repository.Add(student); //Chiamo la funzione del repository che si occupa di aggiungere lo studente

            await _repository.UnitOfWork.SaveEntitiesAsync(); // Salvo lo studente nel DB

            return true;

        }


        /*public async Task<bool> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {
            //questo non dovrei farlo!
            var stud = _context.Students.Where(s => s.FirstMidName == request.Student.FirstName && s.LastName == request.Student.LastName).FirstOrDefault();

            if (stud != null)
               throw new AddStudentCommandException("Lo studente risulta essere già registrato!");
                       
            //Creo il nuovo studente e gl iassegno i parametri della Request
            Student student = new();
            student.FirstMidName = request.Student.FirstName;
            student.LastName = request.Student.LastName;
            student.EnrollmentDate = request.Student.EnrollmentDate;

            _context.Add(student);
            
            await _context.SaveChangesAsync();

            return true;// ResultQC<bool>.Success(true);
           
            //return ResultQC<bool>.Failure(true);    
            
            
        }*/
    }
}
