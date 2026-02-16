using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PizzaEnterprise.Application.Behaviors;

namespace PizzaEnterprise.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        
        // Register MediatR
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(assembly);
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });
        
        // Register AutoMapper
        services.AddAutoMapper(assembly);
        
        // Register FluentValidation
        services.AddValidatorsFromAssembly(assembly);
        
        return services;
    }
}
