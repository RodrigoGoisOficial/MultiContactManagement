using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MultiContactManagement.Domain.Entities;

namespace MultiContactManagement.Infra.EntitiesConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(100).IsRequired();
            builder.Property(x => x.IsAdmin).IsRequired();
        }
    }
}
