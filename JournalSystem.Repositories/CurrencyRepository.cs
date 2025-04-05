using JournalSystem.Core.Entities;
using JournalSystem.Core.Repositories;
using JournalSystem.Repositories.Context;
using Microsoft.EntityFrameworkCore;


namespace JournalSystem.Repositories
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly ApplicationDbContext _context;

        public CurrencyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Currency>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Currency.ToListAsync(cancellationToken);
        }

        public async Task<Currency> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            if (id <= 0) throw new ArgumentException("Invalid currency ID", nameof(id));

            return await _context.Currency.FindAsync(id, cancellationToken);
        }

        public async Task AddAsync(Currency currency, CancellationToken cancellationToken = default)
        {
            if (currency == null) throw new ArgumentNullException(nameof(currency));

            await _context.Currency.AddAsync(currency, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Currency currency, CancellationToken cancellationToken = default)
        {
            if (currency == null) throw new ArgumentNullException(nameof(currency));

            _context.Currency.Update(currency);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            if (id <= 0) throw new ArgumentException("Invalid currency ID", nameof(id));

            var currency = await _context.Currency.FindAsync(id, cancellationToken);
            if (currency != null)
            {
                _context.Currency.Remove(currency);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
