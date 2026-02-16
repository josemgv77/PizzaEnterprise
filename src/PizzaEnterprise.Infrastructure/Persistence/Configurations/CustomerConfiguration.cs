using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PizzaEnterprise.Domain.Entities;

namespace PizzaEnterprise.Infrastructure.Persistence.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.LastName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.Email)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(c => c.PhoneNumber)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(c => c.IsActive)
            .IsRequired();

        builder.OwnsOne(c => c.DefaultAddress, address =>
        {
            address.Property(a => a.Street)
                .HasColumnName("Address_Street")
                .HasMaxLength(200);

            address.Property(a => a.City)
                .HasColumnName("Address_City")
                .HasMaxLength(100);

            address.Property(a => a.State)
                .HasColumnName("Address_State")
                .HasMaxLength(100);

            address.Property(a => a.ZipCode)
                .HasColumnName("Address_ZipCode")
                .HasMaxLength(20);

            address.Property(a => a.Country)
                .HasColumnName("Address_Country")
                .HasMaxLength(100);
        });

        builder.Ignore(c => c.FullName);
        builder.Ignore(c => c.DomainEvents);
    }
}
