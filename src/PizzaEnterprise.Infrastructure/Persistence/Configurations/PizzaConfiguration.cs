using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PizzaEnterprise.Domain.Entities;
using PizzaEnterprise.Domain.Enums;

namespace PizzaEnterprise.Infrastructure.Persistence.Configurations;

public class PizzaConfiguration : IEntityTypeConfiguration<Pizza>
{
    public void Configure(EntityTypeBuilder<Pizza> builder)
    {
        builder.ToTable("Pizzas");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.Description)
            .HasMaxLength(500);

        builder.Property(p => p.Size)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(p => p.IsAvailable)
            .IsRequired();

        builder.Property(p => p.ImageUrl)
            .HasMaxLength(500);

        builder.OwnsOne(p => p.BasePrice, money =>
        {
            money.Property(m => m.Amount)
                .HasColumnName("BasePrice_Amount")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            money.Property(m => m.Currency)
                .HasColumnName("BasePrice_Currency")
                .HasMaxLength(3)
                .IsRequired();
        });

        builder.Ignore(p => p.DomainEvents);
    }
}
