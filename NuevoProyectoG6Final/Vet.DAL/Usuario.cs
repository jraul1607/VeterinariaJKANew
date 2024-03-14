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
    [Table("Usuarios")]
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }

        [ForeignKey("Rol")]
        [DisplayName("Rol")]
        public int IdRol { get; set; }

        [DisplayName("Nombre de Usuario")]
        public string NombreUsuario { get; set; }

        [DisplayName("Contraseña")]
        public string Contrasena { get; set; }

        public string Imagen { get; set; }

        [DisplayName("Ultima Fecha de Conexion")]
        public DateTime UltimaFechaConexion { get; set; }

        public bool Estado { get; set; }

        public Rol? Rol { get; set; }

        public ICollection<Mascota> Mascotas { get; set; } = new List<Mascota>();
    }
}
