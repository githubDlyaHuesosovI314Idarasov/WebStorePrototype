using DAL.Models;
using AutoMapper;
using WebStorePrototype.Server.Models.DTO_s;

namespace WebStorePrototype.Server.Models.Mapping
{
    public class ComparedProductProfile : Profile
    {
        public ComparedProductProfile() {

            CreateMap<ComparedProduct, ComparedProductDTO>()
                .ForMember(
                    dest => dest.ProductName,
                    opt => opt.MapFrom(src => src.Product!.Name)
                )
                .ForMember(
                    dest => dest.Price,
                    opt => opt.MapFrom(src => src.Product!.Price)
                )
                .ForMember(
                    dest => dest.DiscountedPrice,
                    opt => opt.MapFrom(src => src.Product!.DiscountedPrice)
                )
                .ForMember(
                    dest => dest.ThumbnailUrl,
                    opt => opt.MapFrom(src => src.Product!.Images.FirstOrDefault() != null ? src.Product.Images.FirstOrDefault()!.Url : null)
                );

        }
    }
}
