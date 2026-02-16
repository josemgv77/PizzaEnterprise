using AutoMapper;
using PizzaEnterprise.Application.DTOs;
using PizzaEnterprise.Domain.Entities;
using PizzaEnterprise.Domain.ValueObjects;

namespace PizzaEnterprise.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Order, OrderDto>()
            .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount.Amount))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.DeliveryAddress, opt => opt.MapFrom(src => src.DeliveryAddress))
            .ForMember(dest => dest.Items, opt => opt.Ignore());

        CreateMap<OrderItem, OrderItemDto>()
            .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice.Amount))
            .ForMember(dest => dest.Subtotal, opt => opt.MapFrom(src => src.Subtotal.Amount))
            .ForMember(dest => dest.PizzaName, opt => opt.Ignore());

        CreateMap<Pizza, PizzaDto>()
            .ForMember(dest => dest.BasePrice, opt => opt.MapFrom(src => src.BasePrice.Amount))
            .ForMember(dest => dest.Size, opt => opt.MapFrom(src => src.Size.ToString()));

        CreateMap<Customer, CustomerDto>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.DefaultAddress, opt => opt.MapFrom(src => src.DefaultAddress));

        CreateMap<Address, AddressDto>()
            .ReverseMap()
            .ConvertUsing(src => Address.Create(src.Street, src.City, src.State, src.ZipCode, src.Country));
    }
}
