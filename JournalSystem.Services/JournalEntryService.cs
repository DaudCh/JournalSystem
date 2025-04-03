using AutoMapper;
using JournalSystem.Core.DTOS.JournalEntry;
using JournalSystem.Core.DTOS.JournalLines;
using JournalSystem.Core.Entities;
using JournalSystem.Core.Repositories;
using JournalSystem.Core.Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace JournalSystem.Services
{
    public class JournalEntryService : IJournalEntryService
    {
        private readonly IJournalEntryRepository _journalEntryRepository;
        private readonly IMapper _mapper;

        public JournalEntryService(IJournalEntryRepository journalEntryRepository,IMapper mapper)
        {
            _journalEntryRepository = journalEntryRepository;
           
            _mapper = mapper;
        }

        public async Task<IEnumerable<JournalEntryDTO>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var journalEntries = await _journalEntryRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<JournalEntryDTO>>(journalEntries);
        }

        public async Task<JournalEntryDTO> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            if (id <= 0) throw new ArgumentException("Invalid journal entry ID", nameof(id));

            var journalEntry = await _journalEntryRepository.GetByIdAsync(id, cancellationToken);
            return _mapper.Map<JournalEntryDTO>(journalEntry);
        }

        public async Task AddAsync(CreateJournalEntryDTO journalEntryDTO, CancellationToken cancellationToken = default)
        {
            if (journalEntryDTO == null) throw new ArgumentNullException(nameof(journalEntryDTO));

            var journalEntry = _mapper.Map<JournalEntry>(journalEntryDTO);
            await _journalEntryRepository.AddAsync(journalEntry, cancellationToken);
        }

        public async Task UpdateAsync(UpdateJournalEntryDTO journalEntryDTO, CancellationToken cancellationToken = default)
        {
            if (journalEntryDTO == null) throw new ArgumentNullException(nameof(journalEntryDTO));

            var journalEntry = _mapper.Map<JournalEntry>(journalEntryDTO);
            await _journalEntryRepository.UpdateAsync(journalEntry, cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            if (id <= 0) throw new ArgumentException("Invalid journal entry ID", nameof(id));

            var journalEntry = await _journalEntryRepository.GetByIdAsync(id, cancellationToken);
            if (journalEntry != null)
            {
                await _journalEntryRepository.DeleteAsync(id, cancellationToken);
            }
        }

        public async Task<JournalEntryWithLinesDTO> GetJournalEntryWithLinesAsync(int id, CancellationToken cancellationToken = default)
        {
            if (id <= 0) throw new ArgumentException("Invalid journal entry ID", nameof(id));

            var journalEntry = await _journalEntryRepository.GetJournalEntryWithLinesAsync(id, cancellationToken);
            if (journalEntry == null) return null;
            var journalEntryWithLinesDTO = _mapper.Map<JournalEntryWithLinesDTO>(journalEntry);

            return journalEntryWithLinesDTO;
        }

    }
}
