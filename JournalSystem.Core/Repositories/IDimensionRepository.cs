using JournalSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JournalSystem.Core.Repositories
{
    public interface IDimensionRepository
    {
        Task<IEnumerable<Dimension>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Dimension> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task AddAsync(Dimension dimension, CancellationToken cancellationToken = default);
        Task UpdateAsync(Dimension dimension, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);

    }
}
