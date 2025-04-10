using JournalSystem.Core.Entities;
using JournalSystem.Core.Repositories;
using JournalSystem.Repositories.Context;
using Microsoft.EntityFrameworkCore;


namespace JournalSystem.Repositories
{
    public class JournalEntryRepository : IJournalEntryRepository
    {
        private readonly ApplicationDbContext _context;
        public JournalEntryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

     public async Task AddAsync(JournalEntry journalEntry, CancellationToken cancelelationToken = default)
        {
            if(journalEntry == null) throw new ArgumentNullException(nameof(journalEntry));
            await _context.JournalEntries.AddAsync(journalEntry);
            await _context.SaveChangesAsync(cancelelationToken);
        }

        public async Task UpdateAsync(JournalEntry journalEntry, CancellationToken cancellationToken = default)
        {
            if (journalEntry == null)
            {
                throw new ArgumentNullException(nameof(journalEntry));
            }
            _context.Entry(journalEntry).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }   

            public async Task<IEnumerable<JournalEntry>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var Journal = await _context.JournalEntries
                         .Include(je => je.Currency) 
                         .Include(je => je.JournalLines) 
                         .ToListAsync(cancellationToken);
            return Journal;
        }

        public async Task<JournalEntry> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.JournalEntries
                .Include(je => je.Currency)
                .Include(je => je.JournalLines)
                .FirstOrDefaultAsync(je => je.Id == id, cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            if (id == 0)
            {
                throw new ArgumentException("Invalid journal entry ID", nameof(id));
            }

            var journal = await _context.JournalEntries.FindAsync(id);
            _context.JournalEntries.Remove(journal);
            await _context.SaveChangesAsync(cancellationToken);
        }
     public async Task<JournalEntry> GetJournalEntryWithLinesAsync(int id, CancellationToken cancellationToken = default)
        {
            if(id == 0)
            {
                throw new ArgumentException(nameof(id));
            }
            var journal = await _context.JournalEntries
                         .Include(j => j.JournalLines) 
                         .FirstOrDefaultAsync(j => j.Id == id, cancellationToken); 
                         return journal;
        }
        public async Task AddJournalLinesAsync(IEnumerable<JournalLine> journalLines, CancellationToken cancellationToken)
        {
            await _context.JournalLines.AddRangeAsync(journalLines, cancellationToken); 
            await _context.SaveChangesAsync(cancellationToken);
        }

    }
}
