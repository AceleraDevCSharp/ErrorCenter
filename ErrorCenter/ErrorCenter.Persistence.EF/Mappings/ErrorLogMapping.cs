using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using ErrorCenter.Persistence.EF.Models;
using Microsoft.AspNetCore.Identity;

namespace ErrorCenter.Persistence.EF.Mappings
{
    public class ErrorLogMapping : IEntityTypeConfiguration<ErrorLog>
    {
        public void Configure(EntityTypeBuilder<ErrorLog> builder)
        {

            builder.HasKey(e => e.Id);

            builder.HasOne(x => x.Environment)
            .WithMany("ErrorLogs")
            .HasForeignKey(x => x.EnvironmentID);

            builder.Property(e => e.Level)
                   .HasColumnType("varchar(30)")
                   .IsRequired();

            builder.Property(e => e.Title)
                   .HasColumnType("varchar(500)")
                   .IsRequired();

            builder.Property(e => e.Details)
                   .HasColumnType("varchar(1500)")
                   .IsRequired();

            builder.Property(e => e.Origin)
                   .HasColumnType("varchar(100)")
                   .IsRequired();


            builder.Property(e => e.CreatedAt)
                   .HasDefaultValueSql("getdate()")
                   .ValueGeneratedOnAdd();

            builder.Property(e => e.ArquivedAt);

            builder.Property(e => e.DeletedAt);
        }
    }
}