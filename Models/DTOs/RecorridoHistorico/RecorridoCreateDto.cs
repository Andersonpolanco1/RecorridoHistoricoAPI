namespace EdecanesV2.Models.DTOs.RecorridoHistorico
{
    public class RecorridoCreateDto
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

        public int TipoRecorridoId { get; set; }

        public int HorarioId { get; set; }
    }
}
