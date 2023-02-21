using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EdecanesV2.Models.DTOs.RecorridoHistorico
{
    public class RecorridoCreateDto
    {
        [StringLength(100, ErrorMessage = "La cantidad de caracteres de {0} debe estar entre {2} y {1}.", MinimumLength = 2)]
        public string Nombres { get; set; }


        [StringLength(100, ErrorMessage = "La cantidad de caracteres de {0} debe estar entre {2} y {1}.", MinimumLength = 2)]
        public string Apellidos { get; set; }


        [DataType(DataType.EmailAddress)]
        public string Correo { get; set; }


        public string Cedula { get; set; }


        [DataType(DataType.PhoneNumber)]
        public string Telefono { get; set; }

        public int CantidadVisitantes { get; set; }


        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime FechaVisita { get; set; }


        [StringLength(120, ErrorMessage = "La cantidad de caracteres de {0} debe estar entre {2} y {1}.", MinimumLength = 2)]
        public string Institucion { get; set; }


        [StringLength(40, ErrorMessage = "La cantidad de caracteres de {0} debe estar entre {2} y {1}.", MinimumLength = 2)]
        public string Idioma { get; set; }

        public int TipoRecorridoId { get; set; }
        public int HorarioId { get; set; }
    }
}
