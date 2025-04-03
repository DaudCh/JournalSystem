using AutoMapper;
using JournalSystem.Core.DTOS.Dimension;
using JournalSystem.Core.Entities;
using JournalSystem.Core.Repositories;
using JournalSystem.Core.Services;


namespace JournalSystem.Services
{
    public class DimensionService : IDimensionService
    {
        private readonly IDimensionRepository _dimensionRepository;
        private readonly IMapper _mapper;

        public DimensionService(IDimensionRepository dimensionRepository, IMapper mapper)
        {
            _dimensionRepository = dimensionRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DimensionResponseDTO>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var dimensions = await _dimensionRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<DimensionResponseDTO>>(dimensions);
        }

        public async Task<DimensionResponseDTO> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            if (id <= 0) throw new ArgumentException("Invalid dimension ID", nameof(id));

            var dimension = await _dimensionRepository.GetByIdAsync(id, cancellationToken);
            if (dimension == null) return null;

            return _mapper.Map<DimensionResponseDTO>(dimension);
        }

        public async Task AddAsync(CreateDimensionDTO dimensionDTO, CancellationToken cancellationToken = default)
        {
            if (dimensionDTO == null) throw new ArgumentNullException(nameof(dimensionDTO));

            var dimension = _mapper.Map<Dimension>(dimensionDTO);
            await _dimensionRepository.AddAsync(dimension, cancellationToken);
        }

        public async Task UpdateAsync(UpdateDimensionDTO dimensionDTO, CancellationToken cancellationToken = default)
        {
            if (dimensionDTO == null) throw new ArgumentNullException(nameof(dimensionDTO));

            var dimension = _mapper.Map<Dimension>(dimensionDTO);
            await _dimensionRepository.UpdateAsync(dimension, cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            if (id <= 0) throw new ArgumentException("Invalid dimension ID", nameof(id));

            await _dimensionRepository.DeleteAsync(id, cancellationToken);
        }
    }
}
