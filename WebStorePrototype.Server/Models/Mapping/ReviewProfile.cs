using AutoMapper;
using Contracts;
using DAL.Models;
using WebStorePrototype.Server.Models.DTO_s;

namespace WebStorePrototype.Server.Models.Mapping
{
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            CreateMap<Review, ReviewDTO>()
                .ForMember
                (
                    dest => dest.Rating,
                    opt => opt.MapFrom(src => src.Rating)
                )
                .ForMember(
                    dest => dest.Comment,
                    opt => opt.MapFrom(src => src.UserComment.Text)
                )
                .ForMember(
                    dest => dest.ProductId,
                    opt => opt.MapFrom(src => src.ProductId)
                )
                .ForMember(
                    dest => dest.ProductName,
                    opt => opt.MapFrom(src => src.Product!.Name)
                );

            CreateMap<Review, ReviewNotify>()
                .ForMember
                (
                    dest => dest.Title,
                    opt => opt.MapFrom(src => src.Product!.Name)
                )
                .ForMember(
                    dest => dest.ThumbnailUrl,
                    opt => opt.MapFrom(src => src.Product!.Images.First().Url)
                )
                .ForMember(
                    dest => dest.Text,
                    opt => opt.MapFrom(src => src.UserComment.Text)
                );
        }
    }
}
