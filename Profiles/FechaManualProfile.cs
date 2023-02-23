using AutoMapper;
using RecorridoHistoricoApi.Models;
using RecorridoHistoricoApi.Models.DTOs.FechasManuales;

namespace RecorridoHistoricoApi.Profiles
{
    public class FechaManualProfile : Profile
    {
        public FechaManualProfile()
        {
            CreateMap<NuevaFechaManualDto, FechaManual>();
            CreateMap<FechaManual, NuevaFechaManualDto>(); 


            CreateMap<ActualizarFechaManualDto, FechaManual>()
                .ForAllMembers(opt =>
                opt.Condition((src, dest, value) => value != null));

        }
    }
}
