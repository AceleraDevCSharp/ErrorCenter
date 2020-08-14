using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using ErrorCenter.Persistence.EF.Models;
using ErrorCenter.Persistence.EF.Mappings;

namespace ErrorCenter.Persistence.EF.Context
{
    public class ErrorCenterDbContext : IdentityDbContext<User, Environment, string>
    {

        public DbSet<ErrorLog> ErrorLogs { get; set; }

        public ErrorCenterDbContext(DbContextOptions<ErrorCenterDbContext> options) : base(options)
        {
        }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
      modelBuilder.ApplyConfigurationsFromAssembly(typeof(ErrorCenterDbContext).Assembly);
      modelBuilder.ApplyConfiguration(new UserMapping()); 
      modelBuilder.ApplyConfiguration(new ErrorLogMapping());

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Environment>().HasData(
              new Environment
              {
                  Name = "Development",
                  NormalizedName = "DEVELOPMENT"
              },
              new Environment
              {
                  Name = "Homologation",
                  NormalizedName = "HOMOLOGATION"
              },
              new Environment
              {
                  Name = "Production",
                  NormalizedName = "PRODUCTION"
              }
            );
        }
    }
}
