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
    [Table("Citas")]
    public class Cita
    {
        [Key]
        [DisplayName("Numero de Cita")]
        public int IdCita { get; set; }

        [Required]
        [ForeignKey("Mascota")]
        [DisplayName("Nombre de la Mascota")]
        public int IdMascota { get; set; }

        //Solo se agregar un campo de Veterinario porque se duplica en el ComboBox 
        //Se hace una condicional en la segunda opcion de Combobox para el secundario para que no muestre el Vet que esta asignado de primario.
        [Required]
        [ForeignKey("Usuario")]
        [DisplayName("Veterinario Principal")]
        public int IdUsuarioPrincipal { get; set; }

        [Required]
        [DisplayName("Veterinario Secundario")]
        public int IdUsuarioSecundario { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        [DisplayName("Descripción")]
        public string Descripcion { get; set; }

        [Required]
        [DisplayName("Diagnóstico")]
        public string Diagnostico { get; set; }

        [DisplayName("Estado de la Cita")]
        public string? EstadoCita { get; set; }


        //Propiedad Nullable
        public Mascota? Mascota { get; set; }

        public Usuario? Usuario { get; set; }



        //ICollection de Relaciones
        public ICollection<CitaMedicamento> CitaMedicamentos { get; set; } = new List<CitaMedicamento>();
    }
}
