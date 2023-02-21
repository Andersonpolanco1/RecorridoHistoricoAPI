using static EdecanesV2.Models.Horario;

namespace EdecanesV2.Utils
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
