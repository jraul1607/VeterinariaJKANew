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
    [Table("Medicamentos")]
    public class Medicamento
    {
        [Key]
        public int IdMedicamento { get; set; }
     
        [DisplayName("Nombre de Medicamento")]
        public string Nombre { get; set; }

        public string Marca { get; set; }

        public bool Estado { get; set; }


        //ICollection de Relaciones
        public ICollection<CitaMedicamento> CitaMedicamentos { get; set; } = new List<CitaMedicamento>();
    }
}
