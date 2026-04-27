using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderBurger.API.Models;

namespace OrderBurger.API.Data;

public sealed class OrderConfiguration
{
   public void Configure(EntityTypeBuilder<Order> builder )
   {
      builder.ToTable("Orders");
      builder.HasKey(x => x.Id);
      builder.Property(x => x.Id).ValueGeneratedOnAdd();
      builder.Property(x => x.DateCreated).IsRequired();
      builder.Property(x => x.ConsumerName).IsRequired();
      builder.Property(x => x.Status).IsRequired();
      
      builder.Navigation(o => o.Items)
         .HasField("_items")
         .UsePropertyAccessMode(PropertyAccessMode.Field)
         .AutoInclude();
      
      builder.HasMany(o => o.Items)
         .WithOne()
         .HasForeignKey(x => x.OrderId)
         .OnDelete(DeleteBehavior.Cascade);      
      
      builder.HasIndex(x => x.Id).IsUnique();
      builder.HasIndex(x => x.ConsumerName);
      builder.HasIndex(x => x.Status);
      
      builder.Ignore(x => x.SubTotal);
      builder.Ignore(x => x.Total);
      builder.Ignore(x => x.Discount);

   }    
    
}