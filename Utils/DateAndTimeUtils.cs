using static RecorridoHistoricoApi.Models.Horario;

namespace RecorridoHistoricoApi.Utils
{
    public class DateAndTimeUtils
    {
        public static DiaSemana ToEnumDiaSemana(DateTime fecha)
        {
            int dia = (int)fecha.DayOfWeek;

            return dia switch
            {
                0 => DiaSemana.Domingo,
                1 => DiaSemana.Lunes,
                2 => DiaSemana.Martes,
                3 => DiaSemana.Miércoles,
                4 => DiaSemana.Jueves,
                5 => DiaSemana.Viernes,
                6 => DiaSemana.Sábado,
                _ => default,
            };
        }
    }
}
