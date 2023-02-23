using AutoMapper;
using RecorridoHistoricoApi.Models;
using RecorridoHistoricoApi.Models.DTOs.TipoRecorrido;
using System;

namespace RecorridoHistoricoApi.Profiles
{
    public class TipoProfile : Profile
    {
        public TipoProfile()
        {
            CreateMap<int?, int>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<bool?, bool>().ConvertUsing((src, dest) => src ?? dest);

            CreateMap<Tipo, TipoReadDto>();
            CreateMap<TipoCreateDto, Tipo>();

            CreateMap<TipoUpdateDto, Tipo>()
                .ForAllMembers(opt =>
                opt.Condition((src, dest, value) => value != null));
        }
    }
}
