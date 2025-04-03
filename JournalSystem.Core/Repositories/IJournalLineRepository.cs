using JournalSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JournalSystem.Core.Repositories
{
    public interface IJournalLineRepository
    {
        Task<IEnumerable<JournalLine>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<JournalLine> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task AddAsync(JournalLine journalLine, CancellationToken cancellationToken = default);
        Task UpdateAsync(JournalLine journalLine, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<JournalLine>> GetLinesByJournalEntryIdAsync(int journalEntryId, CancellationToken cancellationToken = default);
    }
}
