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
    [Table("PadecimientoMascotas")]
    public class PadecimientoMascota
    {
        [Key]
        public int IdPadecimientoMascota { get; set; }

        [ForeignKey("Mascota")]
        [DisplayName("Nombre de Mascota")]
        public int IdMascota { get; set; }

        [ForeignKey("Padecimiento")]
        [DisplayName("Nombre de Padecimiento")]
        public int IdPadecimiento { get; set; }

      

        public Mascota? Mascota { get; set; }
        
        public Padecimiento? Padecimiento { get; set; }
    }
}
