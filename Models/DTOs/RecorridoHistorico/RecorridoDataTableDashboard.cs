using System.ComponentModel.DataAnnotations;

namespace RecorridoHistoricoApi.Models.DTOs.RecorridoHistorico
{
    public class RecorridoDataTableDashboard
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string TipoRecorridoDescripcion { get; set; }
        public string FechaCreacion { get; set; }
        public string FechaVisita { get; set; }
        public string Institucion { get; set; }
        public string EstadoDescripcion { get; set; }
    }
}
