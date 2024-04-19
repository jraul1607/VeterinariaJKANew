using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vet.DAL
{
    [Table("AspNetUsers")]
    public class ApplicationUser: IdentityUser
    {
        [Required]
        [MaxLength(100)]
        [DisplayName("Nombre de Usuario")]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(100)]
        [DisplayName("Primer Apellido")]
        public string PrimerApellido { get; set; }

        [Required]
        [MaxLength(100)]
        [DisplayName("Segundo Apellido")]
        public string SegundoApellido { get; set; }

        public string Imagen { get; set; }

        public byte[] Imagen2 { get; set; }

        [DisplayName("Ultima Fecha de Conexion")]
        public DateTime UltimaFechaConexion { get; set; }

        [Required]
        [DefaultValue(true)]
        public bool Estado { get; set; }

        //public ICollection<Mascota> Mascotas { get; set; } = new List<Mascota>();

        //public ICollection<Cita> Citas { get; set; } = new List<Cita>();
    }
}
