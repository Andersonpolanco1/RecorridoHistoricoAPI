namespace RecorridoHistoricoApi.Models.DTOs.RecorridoHistorico
{
    public class RecorridoReadDto
    {
        public int Id { get; set; }

        public string Nombres { get; set; }

        public string Apellidos { get; set; }

        public int CantidadVisitantes { get; set; }

        public DateTime FechaVisita { get; set; }

        public string Institucion { get; set; }
    }
}
