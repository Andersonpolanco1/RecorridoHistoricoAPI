using AutoMapper;
using EdecanesV2.Models;
using EdecanesV2.Models.EstadoDtos;

namespace EdecanesV2.Profiles
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
