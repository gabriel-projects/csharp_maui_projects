using Api.GRRInnovations.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Linq;
using Api.GRRInnovations.Interfaces.Models;

namespace Api.GRRInnovations.Infrastructure.Context
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        { }

        public DbSet<Schedule> Schedules { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            DefaultModelSetup<Schedule>(modelBuilder);
        }

        public override int SaveChanges()
        {
            AdjustChanges();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            int result;
            try
            {
                AdjustChanges();
                result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            }
            catch (Exception)
            {
                RollBack();
                throw;
            }

            return result;
        }

        private void AdjustChanges()
        {
            var changesv3 = ChangeTracker.Entries<BaseModel>().Where(p => p.State == EntityState.Modified || p.State == EntityState.Added);

            foreach (var entry in changesv3)
            {
                entry.Property(p => p.UpdatedAt).CurrentValue = DateTime.UtcNow;

                if (entry.State == EntityState.Added)
                {
                    entry.Property(p => p.CreatedAt).CurrentValue = DateTime.UtcNow;
                }
            }
        }

        public void RollBack()
        {
            var changedEntries = ChangeTracker.Entries()
                .Where(x => x.State != EntityState.Unchanged).ToList();

            foreach (var entry in changedEntries)
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.CurrentValues.SetValues(entry.OriginalValues);
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Unchanged;
                        break;
                }
            }
        }

        public void DefaultModelSetup<T>(ModelBuilder modelBuilder, bool registerKey = true) where T : BaseModel
        {
            modelBuilder.Entity<T>().Property(m => m.CreatedAt).IsRequired();
            modelBuilder.Entity<T>().Property(m => m.UpdatedAt).IsRequired();

            modelBuilder.Entity<T>().HasKey(m => m.Uid);
            modelBuilder.Entity<T>().Property((m) => m.Uid).IsRequired().HasValueGenerator<GuidValueGenerator>();
        }
    }
}
