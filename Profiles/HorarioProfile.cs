using AutoMapper;
using RecorridoHistoricoApi.Models;
using RecorridoHistoricoApi.Models.DTOs.Horarios;
using Newtonsoft.Json.Linq;
using System;
using static RecorridoHistoricoApi.Models.Horario;

namespace RecorridoHistoricoApi.Profiles
{
    public class HorarioProfile : Profile
    {
        public HorarioProfile()
        {
            CreateMap<DiaSemana?, DiaSemana>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<Horario, HorarioDto>();

            try
            {
                CreateMap<HorarioCreateDto, Horario>()
                    .ForMember(dest => dest.Hora, opts => opts.MapFrom(src => src.Hora));
            }
            catch (Exception)
            {
                throw;
            }

            CreateMap<HorarioUpdateDto, Horario>()
                .ForAllMembers(opt =>
                opt.Condition((src, dest, value) => value != null)); ;
        }
    }
}
