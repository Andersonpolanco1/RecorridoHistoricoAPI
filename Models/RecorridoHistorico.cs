using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using EdecanesV2.Models.Base;

namespace EdecanesV2.Models
{
    [Table("RecorridosHistorico")]
    public class RecorridoHistorico : EntityBase
    {
        public string Nombres { get; set; }

        public string Apellidos { get; set; }

        public string Correo { get; set; }

        public string Cedula { get; set; }

        public string Telefono { get; set; }

        public int CantidadVisitantes { get; set; }

        public DateTime FechaVisita { get; set; }

        public string Institucion { get; set; }

        public string Idioma { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime? FechaCulminacion { get; set; }

        public int EstadoId { get; set; }

        public int TipoRecorridoHistoricoId { get; set; }


        public Estado? Estado { get; set; }
        public Tipo? TipoRecorridoHistorico { get; set; }
    }
}
