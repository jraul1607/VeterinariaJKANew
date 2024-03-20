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
    [Table("RazaMascotas")]
    public class RazaMascota
    {
        [Key]
        public int IdRazaMascota { get; set; }

        [ForeignKey("TipoMascota")]
        [DisplayName("Tipo de Mascota")]
        public int IdTipoMascota { get; set; }

        [DisplayName("Raza de la Mascota")]
        public string Nombre { get; set; }

        public bool Estado { get; set; }

        public TipoMascota? TipoMascota { get; set; }

        public ICollection<Mascota> Mascotas { get; set; } = new List<Mascota>();
    }
}
