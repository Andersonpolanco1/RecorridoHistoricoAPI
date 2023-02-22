using AutoMapper;
using EdecanesV2.Models;
using EdecanesV2.Models.DTOs.TandaDtos;

namespace EdecanesV2.Profiles
{
    public class TandaProfile : Profile
    {
        public TandaProfile()
        {
            CreateMap<TandaCreateDto, Tanda>();
            CreateMap<Tanda, TandaDto>().ReverseMap();
        }
    }
}
