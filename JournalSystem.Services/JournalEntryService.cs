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

        public async Task AddAsync(CreateJournalEntryDTO dto, CancellationToken cancellationToken = default)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var entry = _mapper.Map<JournalEntry>(dto);
            await _journalEntryRepository.AddAsync(entry, cancellationToken);

            if (entry.Id == 0)
            {
                throw new Exception("JournalEntry Id is not populated after save.");
            }
            var journalLines = dto.JournalLines.Select(lineDto => new JournalLine
            {
                JournalEntryId = entry.Id,
                AccountId = lineDto.AccountId,
                CostCenterId = lineDto.CostCenterId,
                DimensionId = lineDto.DimensionId,
                Description = lineDto.Description,
                Reference = lineDto.Reference,
                Debit = lineDto.Debit,
                Credit = lineDto.Credit
            }).ToList();

            Console.WriteLine($"Adding {journalLines.Count} journal lines.");
            await _journalEntryRepository.AddJournalLinesAsync(journalLines, cancellationToken);
            Console.WriteLine("Journal lines added successfully.");
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
