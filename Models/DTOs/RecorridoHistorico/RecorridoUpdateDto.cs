using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EdecanesV2.Models.DTOs.RecorridoHistorico
{
    public class RecorridoUpdateDto
    {
        public int Id { get; set; }

        [StringLength(100, ErrorMessage = "La cantidad de caracteres de {0} debe estar entre {2} y {1}.", MinimumLength = 2)]
        public string? Nombres { get; set; }


        [StringLength(100, ErrorMessage = "La cantidad de caracteres de {0} debe estar entre {2} y {1}.", MinimumLength = 2)]
        public string? Apellidos { get; set; }



        [DataType(DataType.PhoneNumber)]
        public string? Telefono { get; set; }

        public int? CantidadVisitantes { get; set; } = null;


        public DateTime? FechaVisita { get; set; } = null;


        [StringLength(120, ErrorMessage = "La cantidad de caracteres de {0} debe estar entre {2} y {1}.", MinimumLength = 2)]
        public string? Institucion { get; set; }

        [StringLength(40, ErrorMessage = "La cantidad de caracteres de {0} debe estar entre {2} y {1}.", MinimumLength = 2)]
        public string? Idioma { get; set; }


        public DateTime? FechaCulminacion { get; set; } = null;

        public int? EstadoId { get; set; } = null;
        public int? TipoRecorridoId { get; set; } = null;
        public int? HorarioId { get; set; } = null;
    }
}
