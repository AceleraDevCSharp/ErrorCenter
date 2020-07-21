using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using ErrorCenter.Persistence.EF.Models;
using ErrorCenter.Persistence.EF.Mappings;

namespace ErrorCenter.Persistence.EF.Context {
  public class ErrorCenterDbContext : IdentityDbContext<User, Environment, string> {
    public DbSet<ErrorLog> ErrorLogs { get; set; }

    public ErrorCenterDbContext(DbContextOptions<ErrorCenterDbContext> options) : base(options) {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
      modelBuilder.ApplyConfigurationsFromAssembly(typeof(ErrorCenterDbContext).Assembly);
      modelBuilder.ApplyConfiguration(new UserMapping());

      base.OnModelCreating(modelBuilder);

      modelBuilder.Entity<IdentityRole>().HasData(
        new IdentityRole {
          Name = "Development",
          NormalizedName = "DEVELOPMENT"
        },
        new IdentityRole {
          Name = "Homologation",
          NormalizedName = "HOMOLOGATION"
        },
        new IdentityRole {
          Name = "Production",
          NormalizedName = "PRODUCTION"
        }
      );
    }
  }
}
