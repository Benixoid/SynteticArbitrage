using AutoMapper;
using DataStorage.Database.Entity;
using DataStorage.Models.DTO;

namespace DataStorage.Configs
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<PriceDifference, PriceDifferenceDTO>().ReverseMap();
            CreateMap<Price, PriceDTO>().ReverseMap();
        }
    }
}
