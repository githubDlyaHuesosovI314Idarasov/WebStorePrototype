using AutoMapper;
using DAL.Models;
using WebStorePrototype.Server.Models.DTO_s;

namespace WebStorePrototype.Server.Models.Mapping
{
    public class StockProfile : Profile
    {
        public StockProfile() {
        
            CreateMap<Stock, StockDTO>()
                .ForMember(
                    dest => dest.ProductId, 
                    opt => opt.MapFrom(src => src.ProductId)
                )
                .ForMember(
                    dest => dest.LocationId,
                    opt => opt.MapFrom(src => src.LocationId)
                )
                .ForMember(
                    dest => dest.Quantity,
                    opt => opt.MapFrom(src => src.Quantity)
                );

        }
    }
}
