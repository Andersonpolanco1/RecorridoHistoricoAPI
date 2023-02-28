using RecorridoHistoricoApi.Models.Base;
using RecorridoHistoricoApi.Utils;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecorridoHistoricoApi.Models
{
    [Table("Empleados")]
    public class Empleado : EntityBase
    {
        [StringLength(50, ErrorMessage = "La cantidad de caracteres de {0} debe estar entre {2} y {1}.", MinimumLength = 2)]
        public string UserName { get; set; }

        [StringLength(100, ErrorMessage = "La cantidad de caracteres de {0} debe estar entre {2} y {1}.", MinimumLength = 2)]
        public string Nombres { get; set; }


        [StringLength(100, ErrorMessage = "La cantidad de caracteres de {0} debe estar entre {2} y {1}.", MinimumLength = 2)]
        public string Apellidos { get; set; }


        [RegularExpression(Util.REGEX_CEDULA, ErrorMessage = "Por favor, digite un número de cédula correcto")]
        public string Cedula { get; set; }
    }
}
