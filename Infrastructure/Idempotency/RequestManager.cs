using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Idempotency
{
    public class RequestManager : IRequestManager
    {
        private readonly SchoolContext _context;

        public RequestManager(SchoolContext context) //bisogna fare il controllo nel caso sia nullo
        {
            _context = context;
        }

        public async Task CreateRequestForCommandAsync<T>(Guid id)
        {
            bool controllo = await ExistsAsync(id); //controllo se l'ID e' gia' presente o meno

            if (controllo)
                throw new Exception("ID gia' presente"); //implementare errore personalizzato

            ClientRequest request = new ClientRequest() { Id = id , Name=typeof(T).Name, Time = DateTime.UtcNow}; //Creo una nuova request univoca grazie a GUID

            _context.Add(request);

            await _context.SaveChangesAsync();
        }

        //Controlla se la richiesta e' gia' presente o meno in memoria. Torna TRUE se la richiesta non e' presente, FALSE se la richiesta e' gia' presente
        public async Task<bool> ExistsAsync(Guid id)
        {
            var request = await _context.FindAsync<ClientRequest>(id);

            return request != null; 
        }
    }
}
