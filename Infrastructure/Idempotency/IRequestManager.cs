using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Idempotency
{
    public interface IRequestManager
    {
        Task<bool> ExistsAsync(Guid id);

        Task CreateRequestForCommandAsync<T>(Guid id);
    }
}
