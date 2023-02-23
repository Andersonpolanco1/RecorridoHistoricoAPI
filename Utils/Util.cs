using System.Text.RegularExpressions;
using static RecorridoHistoricoApi.Models.Horario;

namespace RecorridoHistoricoApi.Utils
{
    public class Util
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


        // Regular expression used to validate a phone number.
        public const string motif = @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$";

        public static bool IsPhoneNumber(string number)
        {
            if (number != null) return Regex.IsMatch(number, motif);
            else return false;
        }

        public static string CleanPhoneNumber(string number)
            => new(number.Where(char.IsDigit).ToArray());

    }
}
