using JournalSystem.Core.DTOS.CostCentre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JournalSystem.Core.Services
{
    public interface ICostCenterService
    {
        Task<IEnumerable<CostCenterResponseDTO>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<CostCenterResponseDTO> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task AddAsync(CreateCostCenterDTO costCenterDTO, CancellationToken cancellationToken = default);
        Task UpdateAsync(UpdateCostCenterDTO costCenterDTO, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}
