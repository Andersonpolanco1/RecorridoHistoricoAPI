using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RecorridoHistoricoApi.Models.DTOs.RecorridoHistorico
{
    public class RecorridoHistoricoDetailsDto
    {
        public int Id { get; set; }
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
        public int TipoRecorridoId { get; set; }
        public int HorarioId { get; set; }
    }
}
