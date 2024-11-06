using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using any.Models;
using Microsoft.EntityFrameworkCore;

namespace any.Data
{
    public class anyContext : DbContext
    {
        public anyContext(DbContextOptions<anyContext> options)
            : base(options) { }

        public DbSet<any.Models.User> User { get; set; } = default!;

        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = new CancellationToken()
        )
        {
            AddTimestamps();
            return await base.SaveChangesAsync();
        }

        private void AddTimestamps()
        {
            var entities = ChangeTracker
                .Entries()
                .Where(x =>
                    x.Entity is BaseEntity
                    && (x.State == EntityState.Added || x.State == EntityState.Modified)
                );

            foreach (var entity in entities)
            {
                var now = DateTime.UtcNow;

                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).CreatedAt = now;
                }
                ((BaseEntity)entity.Entity).UpdatedAt = now;
            }
        }

        public DbSet<any.Models.Category> Category { get; set; } = default!;
        public DbSet<any.Models.Author> Author { get; set; } = default!;
        public DbSet<any.Models.Book> Book { get; set; } = default!;
        public DbSet<any.Models.Cart> Cart { get; set; } = default!;
    }
}
