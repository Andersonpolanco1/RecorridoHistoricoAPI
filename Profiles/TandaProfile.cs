using AutoMapper;
using RecorridoHistoricoApi.Models;
using RecorridoHistoricoApi.Models.DTOs.TandaDtos;

namespace RecorridoHistoricoApi.Profiles
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
