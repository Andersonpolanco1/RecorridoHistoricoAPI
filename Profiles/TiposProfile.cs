using AutoMapper;
using RecorridoHistoricoApi.Models;
using RecorridoHistoricoApi.Models.DTOs.TipoRecorrido;
using RecorridoHistoricoApi.Utils;
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
            CreateMap<TipoCreateDto, Tipo>()
                .ForMember(dest => dest.Nombre, opts => opts.MapFrom(dest => Util.ToTitleCase(dest.Nombre)))
                .ForMember(dest => dest.Descripcion, opts => opts.MapFrom(dest => Util.ToCapitalizeCase(dest.Descripcion)));


            CreateMap<TipoUpdateDto, Tipo>()
                .ForMember(dest => dest.Nombre, opts => opts.MapFrom(dest => Util.ToTitleCase(dest.Nombre)))
                .ForMember(dest => dest.Descripcion, opts => opts.MapFrom(dest => Util.ToCapitalizeCase(dest.Descripcion)))

                .ForAllMembers(opt =>
                opt.Condition((src, dest, value) => value != null));
        }
    }
}
