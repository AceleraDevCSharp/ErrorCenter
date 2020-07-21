using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

using ErrorCenter.Persistence.EF.Models;

namespace ErrorCenter.Persistence.EF.Mappings {
  public class UserMapping : IEntityTypeConfiguration<User> {
    public void Configure(EntityTypeBuilder<User> builder) {
      builder.HasMany(x => x.ErrorLogs)
          .WithOne(x => x.User)
          .HasForeignKey(x => x.IdUser);

      builder.Property(x => x.CreatedAt).IsRequired().HasDefaultValue(DateTime.Now);
      builder.Property(x => x.UpdatedAt).IsRequired().HasDefaultValue(DateTime.Now);
      builder.Property(x => x.DeletedAt).IsRequired(false);
    }
  }
}
