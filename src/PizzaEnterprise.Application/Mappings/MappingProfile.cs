using AutoMapper;
using PizzaEnterprise.Application.DTOs;
using PizzaEnterprise.Domain.Entities;
using PizzaEnterprise.Domain.ValueObjects;

namespace PizzaEnterprise.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Money, decimal>().ConvertUsing(src => src.Amount);

        CreateMap<Order, OrderDto>()
            .ForCtorParam("TotalAmount", opt => opt.MapFrom(src => src.TotalAmount.Amount))
            .ForCtorParam("Status", opt => opt.MapFrom(src => src.Status.ToString()));

        CreateMap<OrderItem, OrderItemDto>()
            .ForCtorParam("UnitPrice", opt => opt.MapFrom(src => src.UnitPrice.Amount))
            .ForCtorParam("Subtotal", opt => opt.MapFrom(src => src.Subtotal.Amount))
            .ForCtorParam("PizzaName", opt => opt.MapFrom(src => string.Empty));

        CreateMap<Pizza, PizzaDto>()
            .ForCtorParam("BasePrice", opt => opt.MapFrom(src => src.BasePrice.Amount))
            .ForCtorParam("Size", opt => opt.MapFrom(src => src.Size.ToString()));

        CreateMap<Customer, CustomerDto>()
            .ForCtorParam("FullName", opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));

        CreateMap<Address, AddressDto>()
            .ReverseMap()
            .ConvertUsing(src => Address.Create(src.Street, src.City, src.State, src.ZipCode, src.Country));
    }
}
