using AutoMapper;
using RecorridoHistoricoApi.Models;
using RecorridoHistoricoApi.Models.DTOs.FechasManuales;
using RecorridoHistoricoApi.Utils;
using System.Globalization;

namespace RecorridoHistoricoApi.Profiles
{
    public class FechaManualProfile : Profile
    {
        public FechaManualProfile()
        {
            CreateMap<NuevaFechaManualDto, FechaManual>()
                .ForMember(dest => dest.Comentario, opts => opts.MapFrom(src => Util.ToCapitalizeCase(src.Comentario)));


            CreateMap<FechaManual, NuevaFechaManualDto>();
            


            CreateMap<ActualizarFechaManualDto, FechaManual>()
                 .ForMember(dest => dest.Comentario, opts => opts.MapFrom(src => Util.ToCapitalizeCase(src.Comentario)))

                .ForAllMembers(opt =>
                opt.Condition((src, dest, value) => value != null));

        }
    }
}
