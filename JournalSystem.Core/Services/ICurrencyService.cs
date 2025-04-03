using JournalSystem.Core.DTOS.Currency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JournalSystem.Core.Services
{
    public interface ICurrencyService
    {
        Task<IEnumerable<CurrencyResponseDTO>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<CurrencyResponseDTO> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task AddAsync(CreateCurrencyDTO currencyDTO, CancellationToken cancellationToken = default);
        Task UpdateAsync(UpdateCurrencyDTO currencyDTO, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}
