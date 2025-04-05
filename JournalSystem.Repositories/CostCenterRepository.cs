using JournalSystem.Core.Entities;
using JournalSystem.Core.Repositories;
using JournalSystem.Repositories.Context;
using Microsoft.EntityFrameworkCore;


namespace JournalSystem.Repositories
{
    public class CostCenterRepository : ICostCenterRepository
    {
        private readonly ApplicationDbContext _context;

        public CostCenterRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CostCenter>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.CostCenter.ToListAsync(cancellationToken);
        }

        public async Task<CostCenter> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            if (id <= 0) throw new ArgumentException("Invalid cost center ID", nameof(id));

            return await _context.CostCenter.FindAsync(id, cancellationToken);
        }

        public async Task AddAsync(CostCenter costCenter, CancellationToken cancellationToken = default)
        {
            if (costCenter == null) throw new ArgumentNullException(nameof(costCenter));

            await _context.CostCenter.AddAsync(costCenter, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(CostCenter costCenter, CancellationToken cancellationToken = default)
        {
            if (costCenter == null) throw new ArgumentNullException(nameof(costCenter));

            _context.CostCenter.Update(costCenter);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            if (id <= 0) throw new ArgumentException("Invalid cost center ID", nameof(id));

            var costCenter = await _context.CostCenter.FindAsync(id, cancellationToken);
            if (costCenter != null)
            {
                _context.CostCenter.Remove(costCenter);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

    }
}
