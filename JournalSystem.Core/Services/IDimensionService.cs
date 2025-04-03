using JournalSystem.Core.DTOS.Dimension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JournalSystem.Core.Services
{
    public interface IDimensionService
    {
        Task<IEnumerable<DimensionResponseDTO>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<DimensionResponseDTO> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task AddAsync(CreateDimensionDTO dimensionDTO, CancellationToken cancellationToken = default);
        Task UpdateAsync(UpdateDimensionDTO dimensionDTO, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}
