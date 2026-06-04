using AutoMapper;
using DAL.Models;
using WebStorePrototype.Server.Models.DTO_s;

namespace WebStorePrototype.Server.Models.Mapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile() {

            CreateMap<Product, ProductDTO>()
                .ForMember(
                    dest => dest.ThumbnailUrl,
                    opt => opt.MapFrom(src => src.Images.FirstOrDefault() != null ? src.Images.FirstOrDefault()!.Url : null)
                )
                .ForMember(
                    dest => dest.SubcategoryName,
                    opt => opt.MapFrom(src => src.Subcategory!.Name)
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
        }

    }
}
