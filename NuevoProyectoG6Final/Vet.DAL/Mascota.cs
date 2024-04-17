using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Vet.DAL
{
    [Table("Mascotas")]
    public class Mascota
    {
        [Key]
        public int IdMascota { get; set; }

        [DisplayName("Nombre de la Mascota")]
        public string Nombre { get; set; }


        public int IdTipoMascota { get; set; }

        [ForeignKey("RazaMascota")]
        [DisplayName("Raza de Mascota")]
        public int IdRazaMascota { get; set; }

        public string Genero { get; set; }

        public int Edad { get; set; }

        public int Peso { get; set; }
        public string Imagen { get; set; }

        public byte[] Imagen2 { get; set; }


        [DisplayName("Nombre de Usuario Creador")]
        public string UsuarioCreacionId { get; set; }

        [DisplayName("Nombre del Dueño")]
        public string DuenoId { get; set; }

        [DisplayName("Fecha de Creacion")]
        public DateTime FechaCreacion { get; set; }
        [DisplayName("Fecha de Creacion")]
        public string FechaCreacionFormateada
        {
            get
            {
                return FechaCreacion.ToString("dd/MM/yyyy HH:mm tt");
            }
        }


        [DisplayName("Fecha de Modificacion")]
        public DateTime FechaModificacion { get; set; }
        [DisplayName("Fecha de Modificacion")]

        public string FechaModificacionFormateada
        {
            get
            {
                return FechaModificacion.ToString("dd/MM/yyyy HH:mm tt");
            }
        }

        
        public bool Estado { get; set; }


        //Foreign Keys Nullable 


        public RazaMascota? RazaMascota { get; set; }

        public ApplicationUser? UsuarioCreacion { get; set; }
        public ApplicationUser? Dueno { get; set; }


        //ICollection

        public ICollection<VacunaMascota> VacunaMascotas { get; set; } = new List<VacunaMascota>();

        public ICollection<Cita> Citas { get; set; } = new List<Cita>();

        public ICollection<PadecimientoMascota> PadecimientoMascotas { get; set; } = new List<PadecimientoMascota>();

    }
}
