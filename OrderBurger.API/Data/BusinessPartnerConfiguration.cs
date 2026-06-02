using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderBurger.API.Models;

namespace OrderBurger.API.Data;

public sealed class BusinessPartnerConfiguration : IEntityTypeConfiguration<BusinessPartner>
{
    public void Configure(EntityTypeBuilder<BusinessPartner> builder)
    {
        builder.ToTable("BusinessPartners");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Name).IsRequired().HasMaxLength(150);
        builder.Property(x => x.DocumentNumber).IsRequired().HasMaxLength(20);
        builder.Property(x => x.DocumentType).IsRequired();
        builder.Property(x => x.Type).IsRequired();

        builder.HasIndex(x => x.Id).IsUnique();
        builder.HasIndex(x => x.DocumentNumber);
        builder.HasIndex(x => x.Name);
        builder.HasIndex(x => x.Type);
    }
}
