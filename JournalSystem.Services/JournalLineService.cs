using AutoMapper;
using JournalSystem.Core.DTOS.JournalLines;
using JournalSystem.Core.Repositories;
using JournalSystem.Core.Services;

namespace JournalSystem.Services
{
    public class JournalLineService : IJournalLineService
    {
        private readonly IJournalLineRepository _journalLineRepository;
        private readonly Mapper _mapper;
        public JournalLineService(IJournalLineRepository journalLineRepository , Mapper mapper)
        {
            _journalLineRepository = journalLineRepository;
            _mapper = mapper;
        }

        public async Task AddAsync(CreateJournalLineDTO journalLineDTO, CancellationToken cancellationToken = default)
        {
            if (journalLineDTO == null)
            {
                throw new ArgumentNullException(nameof(journalLineDTO));
            }
            var journalLine = _mapper.Map<JournalLine>(journalLineDTO);
            await _journalLineRepository.AddAsync(journalLine,cancellationToken);
        }

        public async Task UpdateAsync(UpdateJournalLineDTO journalLineDTO, CancellationToken cancellationToken = default)
        {
            if(journalLineDTO == null)
            {
                throw new ArgumentNullException(nameof(journalLineDTO));
            }
            var journalLine =_mapper.Map<JournalLine>(journalLineDTO);
            await _journalLineRepository.UpdateAsync(journalLine, cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var journalLine = await _journalLineRepository.GetByIdAsync(id);
            if (id == 0)
            {
                throw new ArgumentNullException(nameof(id));
            }
            await _journalLineRepository.DeleteAsync(id, cancellationToken);
        }

        public async Task<IEnumerable<JournalLineDTO>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var journalLine = await _journalLineRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<List<JournalLineDTO>>(journalLine);
        }

       public async Task<JournalLineDTO> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            if(id == 0)
            {
                throw new ArgumentNullException(nameof(id));
            }
            var journalLine = await _journalLineRepository.GetByIdAsync(id);
            return _mapper.Map<JournalLineDTO>(journalLine);
        }

     public async Task<IEnumerable<JournalLineDTO>> GetLinesByJournalEntryIdAsync(int journalEntryId, CancellationToken cancellationToken = default)
        {
            var journalLine = await _journalLineRepository.GetLinesByJournalEntryIdAsync(journalEntryId, cancellationToken);
            return _mapper.Map<IEnumerable<JournalLineDTO>>(journalLine);
        }
    }
   

}
