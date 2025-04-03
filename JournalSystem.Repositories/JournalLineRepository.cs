using JournalSystem.Core.Entities;
using JournalSystem.Core.Repositories;
using JournalSystem.Repositories.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace JournalSystem.Repositories
{
    public class JournalLineRepository : IJournalLineRepository
    {
        private readonly ApplicationDbContext _context;

        public JournalLineRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<JournalLine>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.JournalLines.ToListAsync(cancellationToken);
        }

        public async Task<JournalLine> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "ID must be greater than zero.");
            }

            var journalLine = await _context.JournalLines.FindAsync(id);

            if (journalLine == null)
            {
                throw new KeyNotFoundException($"Journal line with ID {id} not found.");
            }

            return journalLine;
        }

        public async Task AddAsync(JournalLine journalLine, CancellationToken cancellationToken = default)
        {
            if (journalLine == null)
            {
                throw new ArgumentNullException(nameof(journalLine));
            }

            await _context.JournalLines.AddAsync(journalLine, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(JournalLine journalLine, CancellationToken cancellationToken = default)
        {
            if (journalLine == null)
            {
                throw new ArgumentNullException(nameof(journalLine));
            }

            _context.JournalLines.Update(journalLine);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "ID must be greater than zero.");
            }

            var journalLine = await _context.JournalLines.FindAsync(id);
            if (journalLine == null)
            {
                throw new KeyNotFoundException($"Journal line with ID {id} not found.");
            }

            _context.JournalLines.Remove(journalLine);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<JournalLine>> GetLinesByJournalEntryIdAsync(int journalEntryId, CancellationToken cancellationToken = default)
        {
            if (journalEntryId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(journalEntryId), "Journal Entry ID must be greater than zero.");
            }

            return await _context.JournalLines
                .Where(jl => jl.JournalEntryId == journalEntryId)
                .ToListAsync(cancellationToken);
        }
    }
}
