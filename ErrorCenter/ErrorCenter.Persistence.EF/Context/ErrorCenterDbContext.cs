using Microsoft.EntityFrameworkCore;

using ErrorCenter.Persistence.EF.Models;

namespace ErrorCenter.Persistence.EF.Context
{
    public class ErrorCenterDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<ErrorLog> ErrorLogs { get; set; }

        public ErrorCenterDbContext(DbContextOptions<ErrorCenterDbContext> options) : base(options)
        { 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ErrorCenterDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
