using AutoMapper;
using RecorridoHistoricoApi.Models;
using RecorridoHistoricoApi.Models.DTOs.TandaDtos;
using RecorridoHistoricoApi.Utils;

namespace RecorridoHistoricoApi.Profiles
{
    public class TandaProfile : Profile
    {
        public TandaProfile()
        {
            CreateMap<TandaCreateDto, Tanda>()
                .ForMember(dest => dest.Descripcion, opts => opts.MapFrom(dest => Util.ToCapitalizeCase(dest.Descripcion)));

            CreateMap<TandaDto, Tanda>()
                .ForMember(dest => dest.Descripcion, opts => opts.MapFrom(dest => Util.ToCapitalizeCase(dest.Descripcion)));

            CreateMap<Tanda, TandaDto>();
            
        }
    }
}
