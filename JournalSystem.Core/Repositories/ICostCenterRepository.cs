using JournalSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JournalSystem.Core.Repositories
{
    public interface ICostCenterRepository
    {
        Task<IEnumerable<CostCenter>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<CostCenter> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task AddAsync(CostCenter costCenter, CancellationToken cancellationToken = default);
        Task UpdateAsync(CostCenter costCenter, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);

    }
}
