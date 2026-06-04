using AutoMapper;
using DAL.Models;
using WebStorePrototype.Server.Models.DTO_s;

namespace WebStorePrototype.Server.Models.Mapping
{
    public class FavoriteProductProfile : Profile
    {
        public FavoriteProductProfile() {

            CreateMap<FavoriteProduct, FavoriteProductDTO>()
                .ForMember(
                    dest => dest.ProductName,
                    opt => opt.MapFrom(src => src.Product!.Name)
                )
                .ForMember(
                    dest => dest.ThumbnailUrl,
                    opt => opt.MapFrom(src => src.Product!.Images.FirstOrDefault() != null ? src.Product.Images.FirstOrDefault() : null)
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
                    dest => dest.IsInStock,
                    opt => opt.MapFrom(src => src.Product!.IsInStock)
                );
        }
    }
}
