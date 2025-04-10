using AutoMapper;
using JournalSystem.Core.DTOS.JournalEntry;
using JournalSystem.Core.Entities;
using JournalSystem.Core.Repositories;
using JournalSystem.Core.Services;


namespace JournalSystem.Services
{
    public class JournalEntryService : IJournalEntryService
    {
        private readonly IJournalEntryRepository _journalEntryRepository;
        private readonly IJournalLineRepository _journalLineRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ICostCenterRepository _costCenterRepository;
        private readonly IDimensionRepository _dimensionRepository;
        private readonly IMapper _mapper;

        public JournalEntryService(IJournalEntryRepository journalEntryRepository, IAccountRepository accountRepository, ICostCenterRepository costCenterRepository, IDimensionRepository dimensionRepository, IJournalLineRepository journalLineRepository, IMapper mapper)
        {
            _journalEntryRepository = journalEntryRepository;
            _journalLineRepository = journalLineRepository;
            _accountRepository = accountRepository;
            _dimensionRepository = dimensionRepository;
            _costCenterRepository = costCenterRepository;

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

        public async Task AddAsync(CreateJournalEntryDTO journalEntryDTO, CancellationToken cancellationToken)
        {
            if (journalEntryDTO == null) throw new ArgumentNullException(nameof(journalEntryDTO));
            var entry = _mapper.Map<JournalEntry>(journalEntryDTO);


            await _journalEntryRepository.AddAsync(entry);
        }

        public async Task UpdateAsync(UpdateJournalEntryDTO journalEntryDTO, CancellationToken cancellationToken = default)
        {
            if (journalEntryDTO == null) throw new ArgumentNullException(nameof(journalEntryDTO));

            var existingEntry = await _journalEntryRepository.GetByIdAsync(journalEntryDTO.Id, cancellationToken);
            if (existingEntry == null) throw new InvalidOperationException("Journal entry not found.");

            _mapper.Map(journalEntryDTO, existingEntry); 
            await _journalEntryRepository.UpdateAsync(existingEntry, cancellationToken);
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
