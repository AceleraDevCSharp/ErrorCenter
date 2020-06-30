using ErrorCenter.Domain;
using Microsoft.EntityFrameworkCore;


namespace ErrorCenter.Persistence.EF.Context
{
    public class ErrorCenterDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<ErrorLog> ErrorLogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=ErrorCenter;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ErrorLog>()
                .HasOne<User>(x => x.User)
                .WithMany(x => x.ErrorLogs)
                .HasForeignKey(fk => fk.IdUser);
        }
    }
}
