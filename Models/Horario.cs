using RecorridoHistoricoApi.Models.Base;
using NuGet.Packaging.Signing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace RecorridoHistoricoApi.Models
{
    [Table("Horarios")]
    public class Horario : EntityBase
    {
        public enum DiaSemana { Lunes = 1, Martes = 2, Miércoles = 3, Jueves = 4, Viernes = 5, Sábado = 6, Domingo = 7 }


        [Required(ErrorMessage = "Por favor, envíe un día de la semana (1 = lunes, 7 = domingo).")]
        public DiaSemana Dia { get; set; }


        [Required]
        public string Hora { get; set; } 

        public int TandaId { get; set; }

        public Tanda? Tanda { get; set; }

        public List<Tipo> TiposRecorrido { get; set; } = new List<Tipo>();


        public string Descripcion()
        {
            return $"{Dia} {Hora}";
        }
    }
}
