using JournalSystem.Core.DTOS.JournalEntry;
using JournalSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JournalSystem.Core.Services
{
    public interface IJournalEntryService
    {
        Task<IEnumerable<JournalEntryDTO>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<JournalEntryDTO> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task AddAsync(CreateJournalEntryDTO journalEntryDTO, CancellationToken cancellationToken = default);
        Task UpdateAsync(UpdateJournalEntryDTO journalEntryDTO, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task<JournalEntryWithLinesDTO> GetJournalEntryWithLinesAsync(int id, CancellationToken cancellationToken = default);
    }

}
