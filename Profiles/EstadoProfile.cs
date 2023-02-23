using AutoMapper;
using RecorridoHistoricoApi.Models;
using RecorridoHistoricoApi.Models.DTOs.EstadoDtos;

namespace RecorridoHistoricoApi.Profiles
{
    public class EstadoProfile : Profile
    {
        public EstadoProfile()
        {
            CreateMap<EstadoCreateDto, Estado>();
            CreateMap<Estado, EstadoDto>().ReverseMap();
        }
    }
}
