using System.Globalization;
using System.Text.RegularExpressions;
using static RecorridoHistoricoApi.Models.Horario;

namespace RecorridoHistoricoApi.Utils
{
    public class Util
    {
        public const string REGEX_PHONE = @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$";
        public const string REGEX_CEDULA = "^[0-9]{3}-?[0-9]{7}-?[0-9]{1}$";
        public const string REGEX_CORERO = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";


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

        public static bool IsValidPhoneNumber(string number)
        {
            if (number != null) return Regex.IsMatch(number, REGEX_PHONE);
            else return false;
        }

        public static string OnlyDigits(string number)
            => new(number.Where(char.IsDigit).ToArray());


        public static bool IsValidCedula(string number)
        {
            if (number != null) return Regex.IsMatch(number, REGEX_CEDULA);
            else return false;
        }

        public static string ToTitleCase(string text) =>
             CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text);

        public static string? ToCapitalizeCase(string text)
        {
            if (string.IsNullOrEmpty(text))
                return null;
            else if (text.Length == 1)
                return char.ToUpper(text[0]).ToString();
            else
                return (char.ToUpper(text[0]) + text.Substring(1));
        }
    }
}
