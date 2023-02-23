namespace RecorridoHistoricoApi.Models.DTOs.DisponibilidadesDto
{
    public class FechasNoDisponibles
    {
        public List<DateTime> ManualesTemporales { get; set; } = new();
        public List<DateTime> ManualesRecurrentes { get; set; } = new();
        public List<DateTime> Llenas { get; set; } = new();

        public bool IsEmpty() =>
             (!ManualesTemporales.Any()) && (!ManualesRecurrentes.Any()) && (!Llenas.Any());
    }
}
