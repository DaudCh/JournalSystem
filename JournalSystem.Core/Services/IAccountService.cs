using JournalSystem.Core.DTOS.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JournalSystem.Core.Services
{
    public interface IAccountService
    {
        Task<IEnumerable<AccountResponseDTO>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<AccountResponseDTO> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task AddAsync(CreateAccountDTO accountDTO, CancellationToken cancellationToken = default);
        Task UpdateAsync(UpdateAccountDTO accountDTO, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}
