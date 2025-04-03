using JournalSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JournalSystem.Core.Repositories
{
    public interface ICurrencyRepository
    {
        Task<IEnumerable<Currency>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Currency> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task AddAsync(Currency currency, CancellationToken cancellationToken = default);
        Task UpdateAsync(Currency currency, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}
