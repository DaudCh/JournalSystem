using AutoMapper;
using JournalSystem.Core.DTOS.CostCentre;
using JournalSystem.Core.Entities;
using JournalSystem.Core.Repositories;
using JournalSystem.Core.Services;


namespace JournalSystem.Services
{
    public class CostCentreService : ICostCenterService
    {
        private readonly ICostCenterRepository _costCenterRepository;
        private readonly IMapper _mapper;

        public CostCentreService(ICostCenterRepository costCenterRepository, IMapper mapper)
        {
            _costCenterRepository = costCenterRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CostCenterResponseDTO>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var costCenters = await _costCenterRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<CostCenterResponseDTO>>(costCenters);
        }

        public async Task<CostCenterResponseDTO> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            if (id <= 0) throw new ArgumentException("Invalid cost center ID", nameof(id));

            var costCenter = await _costCenterRepository.GetByIdAsync(id, cancellationToken);
            if (costCenter == null) return null;

            return _mapper.Map<CostCenterResponseDTO>(costCenter);
        }

        public async Task AddAsync(CreateCostCenterDTO costCenterDTO, CancellationToken cancellationToken = default)
        {
            if (costCenterDTO == null) throw new ArgumentNullException(nameof(costCenterDTO));

            var costCenter = _mapper.Map<CostCenter>(costCenterDTO);
            await _costCenterRepository.AddAsync(costCenter, cancellationToken);
        }

        public async Task UpdateAsync(UpdateCostCenterDTO costCenterDTO, CancellationToken cancellationToken = default)
        {
            if (costCenterDTO == null) throw new ArgumentNullException(nameof(costCenterDTO));

            var costCenter = _mapper.Map<CostCenter>(costCenterDTO);
            await _costCenterRepository.UpdateAsync(costCenter, cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            if (id <= 0) throw new ArgumentException("Invalid cost center ID", nameof(id));

            await _costCenterRepository.DeleteAsync(id, cancellationToken);
        }
    }
}
