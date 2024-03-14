using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vet.DAL
{
    [Table("CitaMedicamentos")]
    public class CitaMedicamento
    {
        [Key]
        public int IdCitaMedicamento { get; set; }

        [ForeignKey("Cita")]
        [DisplayName("Numero de Cita")]
        public int IdCita { get; set; }

        [ForeignKey("Medicamento")]
        [DisplayName("Medicamento")]
        public int IdMedicamento { get; set; }

        public string Dosis { get; set; }

        public Medicamento? Medicamento { get; set; }

        public Cita? Cita { get; set; }
    }
}
