using JournalSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JournalSystem.Core.Repositories
{
    public interface IJournalEntryRepository
    {
        Task<IEnumerable<JournalEntry>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<JournalEntry> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task AddAsync(JournalEntry journalEntry, CancellationToken cancelelationToken = default);
        Task UpdateAsync(JournalEntry journalEntry, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task<JournalEntry> GetJournalEntryWithLinesAsync(int id, CancellationToken cancellationToken = default);
        Task AddJournalLinesAsync(IEnumerable<JournalLine> journalLines, CancellationToken cancellationToken);
    }

}
