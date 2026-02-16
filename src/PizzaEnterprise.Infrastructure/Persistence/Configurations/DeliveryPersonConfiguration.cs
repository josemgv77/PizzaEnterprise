using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PizzaEnterprise.Domain.Entities;

namespace PizzaEnterprise.Infrastructure.Persistence.Configurations;

public class DeliveryPersonConfiguration : IEntityTypeConfiguration<DeliveryPerson>
{
    public void Configure(EntityTypeBuilder<DeliveryPerson> builder)
    {
        builder.ToTable("DeliveryPersons");

        builder.HasKey(d => d.Id);

        builder.Property(d => d.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(d => d.LastName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(d => d.PhoneNumber)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(d => d.VehiclePlate)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(d => d.IsAvailable)
            .IsRequired();

        builder.Property(d => d.IsActive)
            .IsRequired();

        builder.Ignore(d => d.FullName);
        builder.Ignore(d => d.DomainEvents);
    }
}
