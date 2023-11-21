using InfoTrack.Refactoring.Application.Contracts;
using InfoTrack.Refactoring.Domain.Common;
using InfoTrack.Refactoring.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InfoTrack.Refactoring.Persistence
{
    public class InfoTrackDbContext: DbContext
    {
        private readonly ILoggedInUserService? _loggedInUserService;

        public InfoTrackDbContext(DbContextOptions<InfoTrackDbContext> options)
          : base(options)
        {
        }
        //public InfoTrackDbContext(DbContextOptions<InfoTrackDbContext> options, ILoggedInUserService loggedInUserService)
        //    : base(options)
        //{
        //    _loggedInUserService = loggedInUserService;
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.LogTo(Console.WriteLine);
                

        public DbSet<Candidate> Candidates { get; set; }

        public DbSet<Position> Positions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(InfoTrackDbContext).Assembly);

            modelBuilder.Entity<Candidate>().Property(b => b.CandidateId).UseIdentityColumn(1,1);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        //entry.Entity.CreatedBy = _loggedInUserService.UserId;
                        entry.Entity.CreatedBy = "Raj";
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        //entry.Entity.LastModifiedBy = _loggedInUserService.UserId;
                        entry.Entity.LastModifiedBy = "Raj";
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
