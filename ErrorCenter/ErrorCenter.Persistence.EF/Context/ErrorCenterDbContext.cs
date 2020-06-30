using ErrorCenter.Domain;
using Microsoft.EntityFrameworkCore;
using System;

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
            ConfigTableUser(modelBuilder);
            ConfigTableErrorLogs(modelBuilder);
        }

        protected void ConfigTableUser(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ErrorLog>()
                .HasOne<User>(x => x.User)
                .WithMany(x => x.ErrorLogs)
                .HasForeignKey(fk => fk.IdUser);

            modelBuilder.Entity<User>()
                .Property(x => x.Password)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(x => x.Email)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(x => x.Environment)
                .IsRequired();
        }

        protected void ConfigTableErrorLogs(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ErrorLog>()
                .Property(x => x.Environment)
                .IsRequired();

            modelBuilder.Entity<ErrorLog>()
                .Property(x => x.Environment)
                .IsRequired();

            modelBuilder.Entity<ErrorLog>()
                .Property(x => x.Title)
                .IsRequired();

            modelBuilder.Entity<ErrorLog>()
                .Property(x => x.Details)
                .IsRequired();

            modelBuilder.Entity<ErrorLog>()
                .Property(x => x.Origin)
                .IsRequired();

            modelBuilder.Entity<ErrorLog>()
                .Property(x => x.Level)
                .IsRequired();
        }

    }
}
