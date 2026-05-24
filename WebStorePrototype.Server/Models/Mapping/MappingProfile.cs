using AutoMapper;
using DAL.Models;
using WebStorePrototype.Server.Models.DTO_s;

namespace WebStorePrototype.Server.Models.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {

            CreateMap<ViewedProduct, ViewedProductDTO>()
                .ForMember(
                    dest => dest.ProductName,
                    opt => opt.MapFrom(src => src.Product.Name)
                )
                .ForMember(
                    dest => dest.ThumbnailUrl,
                    opt => opt.MapFrom(src => src.Product.Images.FirstOrDefault() != null ? src.Product.Images.FirstOrDefault()!.Url : null)
                )
                .ForMember(
                    dest => dest.Price,
                    opt => opt.MapFrom(src => src.Product.Price)
                )
                .ForMember(
                    dest => dest.DiscountedPrice,
                    opt => opt.MapFrom(src => src.Product.DiscountedPrice)
                )
                .ForMember(
                    dest => dest.IsInStock,
                    opt => opt.MapFrom(src => src.Product.IsInStock)
                );

            CreateMap<CartProduct, CartProductDTO>()
                .ForMember(
                    dest => dest.ProductName,
                    opt => opt.MapFrom(src => src.Product!.Name)
                )
                .ForMember(
                    dest => dest.ThumbnailUrl,
                    opt => opt.MapFrom(src => src.Product!.Images.FirstOrDefault() != null ? src.Product.Images.FirstOrDefault()!.Url : null)
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

            CreateMap<Product, ProductDTO>()
                .ForMember(
                    dest => dest.ThumbnailUrl,
                    opt => opt.MapFrom(src => src.Images.FirstOrDefault() != null ? src.Images.FirstOrDefault()!.Url : null)
                )
                .ForMember(
                    dest => dest.CategoryName,
                    opt => opt.MapFrom(src => src.Category.Name)
                )
               .ForMember(
                    dest => dest.AverageRating,
                    opt => opt.MapFrom(src => src.Reviews.Any() ? (Int64)src.Reviews.Average(r => r.Rating) : 0)
               )
               .ForMember(
                    dest => dest.ReviewCount,
                    opt => opt.MapFrom(src => src.Reviews.Count())
                )
               .ForMember(
                    dest => dest.DiscountedPrice,
                    opt => opt.MapFrom(src => src.DiscountedPrice)
                )
               .ForMember(
                    dest => dest.Price,
                    opt => opt.MapFrom(src => src.Price)
                )
               .ForMember(
                    dest => dest.SKU,
                    opt => opt.MapFrom(src => src.SKU)
                )
               .ForMember(
                    dest => dest.Brand,
                    opt => opt.MapFrom(src => src.Brand)
                )
               .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(src => src.Name)
                )
               .ForMember(
                    dest => dest.IsInStock,
                    opt => opt.MapFrom(src => src.IsInStock)
                );

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
                    opt => opt.MapFrom(src =>src.Product!.DiscountedPrice)
                )
                .ForMember(
                    dest => dest.IsInStock,
                    opt => opt.MapFrom(src => src.Product!.IsInStock)
                );
        }
    }
}
