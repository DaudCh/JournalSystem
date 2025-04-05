using JournalSystem.Core.Entities;
using JournalSystem.Core.Repositories;
using JournalSystem.Repositories.Context;
using Microsoft.EntityFrameworkCore;


namespace JournalSystem.Repositories
{
    public class DimensionRepository : IDimensionRepository
    {
        private readonly ApplicationDbContext _context;

        public DimensionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Dimension>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Dimensions.ToListAsync(cancellationToken);
        }

        public async Task<Dimension> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            if (id <= 0) throw new ArgumentException("Invalid Dimension ID", nameof(id));

            var dimension = await _context.Dimensions.FindAsync(id, cancellationToken);
            if (dimension == null) throw new KeyNotFoundException("Dimension not found");

            return dimension;
        }

        public async Task AddAsync(Dimension dimension, CancellationToken cancellationToken = default)
        {
            if (dimension == null) throw new ArgumentNullException(nameof(dimension));

            await _context.Dimensions.AddAsync(dimension, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Dimension dimension, CancellationToken cancellationToken = default)
        {
            if (dimension == null) throw new ArgumentNullException(nameof(dimension));

            _context.Dimensions.Update(dimension);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var dimension = await GetByIdAsync(id, cancellationToken);
            _context.Dimensions.Remove(dimension);
            await _context.SaveChangesAsync(cancellationToken);
        }

    }
}
