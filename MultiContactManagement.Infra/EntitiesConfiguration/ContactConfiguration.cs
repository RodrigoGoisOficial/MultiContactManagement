using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MultiContactManagement.Domain.Entities;

namespace MultiContactManagement.Infra.EntitiesConfiguration
{
    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CountryCode).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Number).HasMaxLength(15).IsRequired();
        }
    }
}
