using AutoMapper;
using EdecanesV2.Models;
using EdecanesV2.Models.DTOs.FechasManuales;

namespace EdecanesV2.Profiles
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
