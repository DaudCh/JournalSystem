using JournalSystem.Core.Entities;
using JournalSystem.Core.Repositories;
using JournalSystem.Repositories.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace JournalSystem.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationDbContext _context;

        public AccountRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Account>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Account.ToListAsync(cancellationToken);
        }

        public async Task<Account> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            if (id <= 0) throw new ArgumentException("Invalid account ID", nameof(id));

            return await _context.Account.FindAsync(id, cancellationToken);
        }

        public async Task AddAsync(Account account, CancellationToken cancellationToken = default)
        {
            if (account == null) throw new ArgumentNullException(nameof(account));

            await _context.Account.AddAsync(account, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Account account, CancellationToken cancellationToken = default)
        {
            if (account == null) throw new ArgumentNullException(nameof(account));

            _context.Account.Update(account);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            if (id <= 0) throw new ArgumentException("Invalid account ID", nameof(id));

            var account = await _context.Account.FindAsync(id, cancellationToken);
            if (account != null)
            {
                _context.Account.Remove(account);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
