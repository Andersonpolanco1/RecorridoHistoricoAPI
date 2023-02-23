namespace RecorridoHistoricoApi.Models.DTOs.DisponibilidadesDto
{
    public class FechasNoDisponiblesDto
    {
        public List<string> ManualesTemporales { get; set; } = new();
        public List<string> ManualesRecurrentes { get; set; } = new();
        public List<string> Llenas { get; set; } = new();
    }
}
