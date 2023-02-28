using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using RecorridoHistoricoApi.Utils;

namespace RecorridoHistoricoApi.Models.DTOs.RecorridoHistorico
{
    public class RecorridoUpdateDto
    {
        public int Id { get; set; }

        [StringLength(100, ErrorMessage = "La cantidad de caracteres de {0} debe estar entre {2} y {1}.", MinimumLength = 2)]
        public string? Nombres { get; set; }


        [StringLength(100, ErrorMessage = "La cantidad de caracteres de {0} debe estar entre {2} y {1}.", MinimumLength = 2)]
        public string? Apellidos { get; set; }


        [RegularExpression(Util.REGEX_PHONE, ErrorMessage = "Por favor, digite un número de teléfono correcto.")]
        public string? Telefono { get; set; }


        [Range(10, 300, ErrorMessage = "Valor de {0} debe estar entre {1} y {2}.")]
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
        public int? AsignadoAId { get; set; } = null;

    }
}
