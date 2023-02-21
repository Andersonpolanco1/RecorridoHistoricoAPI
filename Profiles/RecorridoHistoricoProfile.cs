using AutoMapper;
using EdecanesV2.Models;
using EdecanesV2.Models.DTOs.RecorridoHistorico;

namespace EdecanesV2.Profiles
{
    public class RecorridoHistoricoProfile : Profile
    {
        public RecorridoHistoricoProfile()
        {
            CreateMap<RecorridoCreateDto, RecorridoHistorico>(); 
            CreateMap<RecorridoHistorico, RecorridoReadDto>(); 
        }
    }
}
