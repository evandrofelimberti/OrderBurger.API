using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderBurger.API.Models;

namespace OrderBurger.API.Data;

public sealed class InventoryTransactionItemConfiguration : IEntityTypeConfiguration<InventoryTransactionItem>
{
    public void Configure(EntityTypeBuilder<InventoryTransactionItem> builder)
    {
        builder.ToTable("InventoryTransactionItems");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.InventoryTransactionId).IsRequired();
        builder.Property(x => x.ProductId).IsRequired();
        builder.Property(x => x.TransactionType).IsRequired();
        builder.Property(x => x.Date).IsRequired();

        builder.Property(x => x.UnitPrice).HasColumnType("decimal(18,2)");
        builder.Property(x => x.Quantity).HasColumnType("decimal(18,2)");

        builder.HasOne(i => i.Product)
            .WithMany()
            .HasForeignKey(i => i.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.Id).IsUnique();
        builder.HasIndex(x => x.InventoryTransactionId);
        builder.HasIndex(x => x.ProductId);

        builder.Ignore(x => x.Total);
    }
}
