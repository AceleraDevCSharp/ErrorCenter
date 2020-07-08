using ErrorCenter.Services.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ErrorCenter.Persistence.EF.Mappings
{
    public class ErrorLogMapping : IEntityTypeConfiguration<ErrorLog>
    {
        public void Configure(EntityTypeBuilder<ErrorLog> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Environment)
                .IsRequired();

            builder.Property(x => x.Level)
                .IsRequired();

            builder.Property(x => x.Title)
                .IsRequired();

            builder.Property(x => x.Details)
                .IsRequired();

            builder.Property(x => x.Origin)
                .IsRequired();
        }
    }
}