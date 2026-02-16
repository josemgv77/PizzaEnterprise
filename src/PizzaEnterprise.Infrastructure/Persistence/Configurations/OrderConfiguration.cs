using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PizzaEnterprise.Domain.Entities;
using PizzaEnterprise.Domain.Enums;

namespace PizzaEnterprise.Infrastructure.Persistence.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");

        builder.HasKey(o => o.Id);

        builder.Property(o => o.OrderNumber)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(o => o.CustomerId)
            .IsRequired();

        builder.Property(o => o.DeliveryPersonId);

        builder.Property(o => o.PaymentId);

        builder.Property(o => o.Status)
            .IsRequired()
            .HasConversion<string>();

        builder.OwnsOne(o => o.TotalAmount, money =>
        {
            money.Property(m => m.Amount)
                .HasColumnName("TotalAmount_Amount")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            money.Property(m => m.Currency)
                .HasColumnName("TotalAmount_Currency")
                .HasMaxLength(3)
                .IsRequired();
        });

        builder.OwnsOne(o => o.DeliveryAddress, address =>
        {
            address.Property(a => a.Street)
                .HasColumnName("DeliveryAddress_Street")
                .HasMaxLength(200)
                .IsRequired();

            address.Property(a => a.City)
                .HasColumnName("DeliveryAddress_City")
                .HasMaxLength(100)
                .IsRequired();

            address.Property(a => a.State)
                .HasColumnName("DeliveryAddress_State")
                .HasMaxLength(100)
                .IsRequired();

            address.Property(a => a.ZipCode)
                .HasColumnName("DeliveryAddress_ZipCode")
                .HasMaxLength(20)
                .IsRequired();

            address.Property(a => a.Country)
                .HasColumnName("DeliveryAddress_Country")
                .HasMaxLength(100)
                .IsRequired();
        });

        builder.HasMany(o => o.Items)
            .WithOne()
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Ignore(o => o.DomainEvents);
    }
}
