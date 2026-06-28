using AutoMapper;
using DAL.Models;
using WebStorePrototype.Server.Models.DTO_s;
using WebStorePrototype.Server.Models.Events.Order;

namespace WebStorePrototype.Server.Models.Mapping
{
    public class OrderProfile : Profile
    {
        public OrderProfile() {

            CreateMap<Order, OrderDTO>()
                .ForMember(
                    dest => dest.ProductIds,
                    opt => opt.MapFrom(src => src.Products.Select(op => op.Id))
                )
                .ForMember(
                    dest => dest.Quantity,
                    opt => opt.MapFrom(src => src.Products.Count())
                )
                .ForMember(
                    dest => dest.TotalAmount,
                    opt => opt.MapFrom(src => src.Products.Sum(p => p.DiscountedPrice))
                )
                .ForMember(
                    dest => dest.OrderNumber,
                    opt => opt.MapFrom(src => src.OrderNumber)
                )
                .ForMember(
                    dest => dest.Status,
                    opt => opt.MapFrom(src => src.Status)
                );

            CreateMap<Order, OrderCreated>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => src.Id)
                )
                .ForMember(
                    dest => dest.UserId,
                    opt => opt.MapFrom(src => src.UserId)
                )
                .ForMember(
                    dest => dest.Products,
                    opt => opt.MapFrom(src => src.Products)
                )
                .ForMember(
                    dest => dest.OrderNumber,
                    opt => opt.MapFrom(src => src.OrderNumber)
                )
                .ForMember(
                    dest => dest.Status,
                    opt => opt.MapFrom(src => src.Status)
                )
                .ForMember(
                   dest => dest.TotalAmount,
                    opt => opt.MapFrom(src => src.TotalAmount)
                )
                .ForMember(
                    dest => dest.WhenCreated,
                    opt => opt.MapFrom(src => src.OrderDate)
                );

        }
    }
}
