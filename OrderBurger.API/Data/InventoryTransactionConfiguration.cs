using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderBurger.API.Models;

namespace OrderBurger.API.Data;

public sealed class InventoryTransactionConfiguration : IEntityTypeConfiguration<InventoryTransaction>
{
    public void Configure(EntityTypeBuilder<InventoryTransaction> builder)
    {
        builder.ToTable("InventoryTransactions");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.TransactionType).IsRequired();
        builder.Property(x => x.DateCreated).IsRequired();
        builder.Property(x => x.BusinessPartnerId).IsRequired();
        builder.Property(x => x.Status).IsRequired();
        builder.Property(x => x.Observations).HasMaxLength(500);

        builder.Navigation(o => o.Items)
            .HasField("_items")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .AutoInclude();

        builder.HasMany(o => o.Items)
            .WithOne()
            .HasForeignKey(x => x.InventoryTransactionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.BusinessPartner)
            .WithMany()
            .HasForeignKey(x => x.BusinessPartnerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.Id).IsUnique();
        builder.HasIndex(x => x.TransactionType);
        builder.HasIndex(x => x.Status);
        builder.HasIndex(x => x.BusinessPartnerId);

        builder.Ignore(x => x.SubTotal);
        builder.Ignore(x => x.Total);
        builder.Ignore(x => x.Discount);
    }
}
