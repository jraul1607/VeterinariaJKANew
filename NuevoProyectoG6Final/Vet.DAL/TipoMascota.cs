using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vet.DAL
{
    [Table("TipoMascotas")]
    public class TipoMascota
    {
        [Key]
        public int IdTipoMascota { get; set; }

        [DisplayName("Tipo de Animal")]
        public string Nombre { get; set; }

        public bool Estado { get; set; }

        public ICollection<RazaMascota> RazaMascotas { get; set; } = new List<RazaMascota>();

        

    }
}
