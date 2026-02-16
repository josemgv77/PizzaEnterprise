using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PizzaEnterprise.Domain.Entities;
using PizzaEnterprise.Domain.Enums;

namespace PizzaEnterprise.Infrastructure.Persistence.Configurations;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("Payments");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.OrderId)
            .IsRequired();

        builder.Property(p => p.PaymentMethod)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(p => p.Status)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(p => p.TransactionId)
            .HasMaxLength(100);

        builder.Property(p => p.CompletedAt);

        builder.OwnsOne(p => p.Amount, money =>
        {
            money.Property(m => m.Amount)
                .HasColumnName("Amount_Amount")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            money.Property(m => m.Currency)
                .HasColumnName("Amount_Currency")
                .HasMaxLength(3)
                .IsRequired();
        });

        builder.Ignore(p => p.DomainEvents);
    }
}
