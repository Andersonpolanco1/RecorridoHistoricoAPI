using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RecorridoHistoricoApi.Utils;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecorridoHistoricoApi.Models.DTOs.Dashboard
{
    public class RecorridoDashboardUpdateDto
    {
        public int Id { get; set; }


        [DataType(DataType.EmailAddress)]
        [RegularExpression(Util.REGEX_CORERO, ErrorMessage = "Por favor, digite un correo válido.")]
        public string? Correo { get; set; } = null;


        [RegularExpression(Util.REGEX_CEDULA, ErrorMessage = "Por favor, digite un número de cédula correcto")]
        public string? Cedula { get; set; } = null;



        [Range(10, 300, ErrorMessage = "Valor de {0} debe estar entre {1} y {2}.")]
        public int? CantidadVisitantes { get; set; } = null;


        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime? FechaVisita { get; set; } = null;


        [StringLength(40, ErrorMessage = "La cantidad de caracteres de {0} debe estar entre {2} y {1}.", MinimumLength = 2)]
        public string? Idioma { get; set; } = null;


        public int? EstadoId { get; set; } = null;
        public int? TipoRecorridoId { get; set; } = null;
        public int? HorarioId { get; set; } = null;
        public int? AdignadoAId { get; set; } = null;
    }
}
