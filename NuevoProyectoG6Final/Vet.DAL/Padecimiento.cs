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
    [Table("Padecimientos")]
    public class Padecimiento
    {

        [Key]
        public int IdPadecimiento { get; set; }
        [DisplayName("Nombre de Padecimiento")]
        public string Nombre { get; set; }

        //ICollections
        public ICollection<PadecimientoMascota> PadecimientoMascotas { get; set; } = new List<PadecimientoMascota>();
    }
}
