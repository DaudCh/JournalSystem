using JournalSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace JournalSystem.Repositories.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-4F8B9BO\\MSSQLSERVER01;Initial Catalog=JournalDb;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
            }
        }

        public DbSet<Account> Account { get; set; }
        public DbSet<CostCenter> CostCenter { get; set; }
        public DbSet<Currency> Currency { get; set; }
        public DbSet<Dimension> Dimensions { get; set; }
        public DbSet<JournalEntry> JournalEntries { get; set; }
        public DbSet<JournalLine> JournalLines { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Account>().HasKey(a => a.Id);
            modelBuilder.Entity<CostCenter>().HasKey(cc => cc.Id);
            modelBuilder.Entity<Currency>().HasKey(c => c.Id);
            modelBuilder.Entity<Dimension>().HasKey(d => d.Id);
            modelBuilder.Entity<JournalEntry>().HasKey(je => je.Id);
            modelBuilder.Entity<JournalLine>().HasKey(jl => jl.Id);


            modelBuilder.Entity<JournalEntry>()
                .Property(je => je.Id)
                .ValueGeneratedOnAdd();


            modelBuilder.Entity<JournalEntry>()
                .HasMany(je => je.JournalLines)
                .WithOne(jl => jl.JournalEntry)
                .HasForeignKey(jl => jl.JournalEntryId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<JournalEntry>()
                .HasOne(je => je.Currency)
                .WithMany()
                .HasForeignKey(je => je.CurrencyId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<JournalLine>()
                .HasOne(jl => jl.Account)
                .WithMany(a => a.JournalLines)
                .HasForeignKey(jl => jl.AccountId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<JournalLine>()
                .HasOne(jl => jl.CostCenter)
                .WithMany(cc => cc.JournalLines)
                .HasForeignKey(jl => jl.CostCenterId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<JournalLine>()
                .HasOne(jl => jl.Dimension)
                .WithMany(d => d.JournalLines)
                .HasForeignKey(jl => jl.DimensionId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
