using Microsoft.EntityFrameworkCore;
using PizzaEnterprise.Domain.Entities;
using PizzaEnterprise.Domain.Enums;
using PizzaEnterprise.Domain.ValueObjects;

namespace PizzaEnterprise.Infrastructure.Persistence.Seeds;

public static class DataSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        if (await context.Customers.AnyAsync() || 
            await context.Pizzas.AnyAsync() || 
            await context.DeliveryPersons.AnyAsync())
        {
            return;
        }

        // Seed Customers
        var customers = new List<Customer>
        {
            new()
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                PhoneNumber = "+1-555-0101",
                DefaultAddress = Address.Create(
                    "123 Main Street",
                    "New York",
                    "NY",
                    "10001",
                    "USA"
                ),
                IsActive = true
            },
            new()
            {
                FirstName = "Jane",
                LastName = "Smith",
                Email = "jane.smith@example.com",
                PhoneNumber = "+1-555-0102",
                DefaultAddress = Address.Create(
                    "456 Oak Avenue",
                    "Los Angeles",
                    "CA",
                    "90001",
                    "USA"
                ),
                IsActive = true
            },
            new()
            {
                FirstName = "Michael",
                LastName = "Johnson",
                Email = "michael.johnson@example.com",
                PhoneNumber = "+1-555-0103",
                DefaultAddress = Address.Create(
                    "789 Pine Road",
                    "Chicago",
                    "IL",
                    "60601",
                    "USA"
                ),
                IsActive = true
            }
        };

        await context.Customers.AddRangeAsync(customers);

        // Seed Pizzas
        var pizzas = new List<Pizza>
        {
            new()
            {
                Name = "Margherita",
                Description = "Classic tomato sauce, fresh mozzarella, basil, and olive oil",
                Size = PizzaSize.Small,
                BasePrice = Money.Create(8.99m),
                IsAvailable = true,
                ImageUrl = "/images/pizzas/margherita.jpg"
            },
            new()
            {
                Name = "Pepperoni",
                Description = "Tomato sauce, mozzarella, and plenty of pepperoni slices",
                Size = PizzaSize.Medium,
                BasePrice = Money.Create(12.99m),
                IsAvailable = true,
                ImageUrl = "/images/pizzas/pepperoni.jpg"
            },
            new()
            {
                Name = "Hawaiian",
                Description = "Tomato sauce, mozzarella, ham, and pineapple chunks",
                Size = PizzaSize.Large,
                BasePrice = Money.Create(15.99m),
                IsAvailable = true,
                ImageUrl = "/images/pizzas/hawaiian.jpg"
            },
            new()
            {
                Name = "Supreme",
                Description = "Loaded with pepperoni, sausage, bell peppers, onions, and mushrooms",
                Size = PizzaSize.Large,
                BasePrice = Money.Create(17.99m),
                IsAvailable = true,
                ImageUrl = "/images/pizzas/supreme.jpg"
            },
            new()
            {
                Name = "Vegetarian",
                Description = "Fresh vegetables including bell peppers, onions, mushrooms, olives, and tomatoes",
                Size = PizzaSize.Medium,
                BasePrice = Money.Create(11.99m),
                IsAvailable = true,
                ImageUrl = "/images/pizzas/vegetarian.jpg"
            }
        };

        await context.Pizzas.AddRangeAsync(pizzas);

        // Seed Delivery Persons
        var deliveryPersons = new List<DeliveryPerson>
        {
            new()
            {
                FirstName = "Carlos",
                LastName = "Rodriguez",
                PhoneNumber = "+1-555-0201",
                VehiclePlate = "ABC-1234",
                IsAvailable = true,
                IsActive = true
            },
            new()
            {
                FirstName = "Maria",
                LastName = "Garcia",
                PhoneNumber = "+1-555-0202",
                VehiclePlate = "XYZ-5678",
                IsAvailable = true,
                IsActive = true
            }
        };

        await context.DeliveryPersons.AddRangeAsync(deliveryPersons);

        await context.SaveChangesAsync();
    }
}
