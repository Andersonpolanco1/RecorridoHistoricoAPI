using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using RecorridoHistoricoApi.Models.Base;
using RecorridoHistoricoApi.Utils;

namespace RecorridoHistoricoApi.Models
{
    [Table("RecorridosHistorico")]
    public class RecorridoHistorico : EntityBase
    {
        [StringLength(100, ErrorMessage = "La cantidad de caracteres de {0} debe estar entre {2} y {1}.", MinimumLength = 2)]
        public string Nombres { get; set; }


        [StringLength(100, ErrorMessage = "La cantidad de caracteres de {0} debe estar entre {2} y {1}.", MinimumLength = 2)]
        public string Apellidos { get; set; }

        [DataType(DataType.EmailAddress)]
        [RegularExpression(Util.REGEX_CORERO, ErrorMessage = "Por favor, digite un correo válido.")]
        public string Correo { get; set; }


        [RegularExpression(Util.REGEX_CEDULA,ErrorMessage ="Por favor, digite un número de cédula correcto")]
        public string Cedula { get; set; }


        [RegularExpression(Util.REGEX_PHONE, ErrorMessage = "Por favor, digite un número de teléfono correcto")]
        public string Telefono { get; set; }

        [Range(10, 300, ErrorMessage = "Valor de {0} debe estar entre {1} y {2}.")]
        public int CantidadVisitantes { get; set; }


        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime FechaVisita { get; set; }


        [StringLength(120, ErrorMessage = "La cantidad de caracteres de {0} debe estar entre {2} y {1}.", MinimumLength = 2)]
        public string Institucion { get; set; }

        [StringLength(40, ErrorMessage = "La cantidad de caracteres de {0} debe estar entre {2} y {1}.", MinimumLength = 2)]
        public string Idioma { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime? FechaCulminacion { get; set; }

        public int EstadoId { get; set; }
        public int TipoRecorridoId { get; set; }
        public int HorarioId { get; set; }

        public Tipo? TipoRecorrido { get; set; }
        public Estado? Estado { get; set; }
        public Horario? Horario { get; set; }
    }
}
