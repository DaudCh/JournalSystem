using JournalSystem.Core.DTOS.JournalLines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JournalSystem.Core.Services
{
    public interface IJournalLineService
    {
        Task<IEnumerable<JournalLineDTO>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<JournalLineDTO> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task AddAsync(CreateJournalLineDTO journalLineDTO, CancellationToken cancellationToken = default);
        Task UpdateAsync(UpdateJournalLineDTO journalLineDTO, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<JournalLineDTO>> GetLinesByJournalEntryIdAsync(int journalEntryId, CancellationToken cancellationToken = default);
    }

}
