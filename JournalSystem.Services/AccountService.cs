using AutoMapper;
using JournalSystem.Core.DTOS.Account;
using JournalSystem.Core.Entities;
using JournalSystem.Core.Repositories;
using JournalSystem.Core.Services;


namespace JournalSystem.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public AccountService(IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AccountResponseDTO>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var accounts = await _accountRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<AccountResponseDTO>>(accounts);
        }

        public async Task<AccountResponseDTO> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            if (id <= 0) throw new ArgumentException("Invalid account ID", nameof(id));

            var account = await _accountRepository.GetByIdAsync(id, cancellationToken);
            if (account == null) return null;

            return _mapper.Map<AccountResponseDTO>(account);
        }

        public async Task AddAsync(CreateAccountDTO accountDTO, CancellationToken cancellationToken = default)
        {
            if (accountDTO == null) throw new ArgumentNullException(nameof(accountDTO));

            var account = _mapper.Map<Account>(accountDTO);
            await _accountRepository.AddAsync(account, cancellationToken);
        }

        public async Task UpdateAsync(UpdateAccountDTO accountDTO, CancellationToken cancellationToken = default)
        {
            if (accountDTO == null) throw new ArgumentNullException(nameof(accountDTO));

            var account = _mapper.Map<Account>(accountDTO);
            await _accountRepository.UpdateAsync(account, cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            if (id <= 0) throw new ArgumentException("Invalid account ID", nameof(id));

            await _accountRepository.DeleteAsync(id, cancellationToken);
        }
    }
}
