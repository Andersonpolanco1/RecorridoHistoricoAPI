namespace RecorridoHistoricoApi.Models.DTOs.RecorridoHistorico
{
    public class RecorridoCalendarioDashboardDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int CantidadVisitantes { get; set; } 
        public string FechaVisita { get; set; } 
        public string FechaCreacion { get; set; }
        public string Hora { get; set; } 
        public int EstadoId { get; set; }
        public string EstadoDescripcion { get; set; } 
        public int TipoSolicitudId { get; set; }
        public string TipoSolicitudDescripcion { get; set; }
    }
}
