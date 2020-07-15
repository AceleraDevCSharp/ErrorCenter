using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

using ErrorCenter.Persistence.EF.Models;

namespace ErrorCenter.Persistence.EF.Mappings
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder.HasMany(x => x.ErrorLogs)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.IdUser);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Password)
                .IsRequired();

            builder.Property(x => x.Email)
                .IsRequired();

            builder.Property(x => x.Environment)
                .IsRequired();
        }
    }
}
