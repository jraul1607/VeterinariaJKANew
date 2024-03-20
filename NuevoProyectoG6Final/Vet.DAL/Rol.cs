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
    [Table("Roles")]
    public class Rol
    {
        [Key]
        public int IdRol { get; set; }

        [DisplayName("Nombre del Rol")]
        public string Tipo { get; set; }

        public bool Estado { get; set; }

        public ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
    }
}
