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
    [Table("VacunaMascotas")]
    public class VacunaMascota
    {
        [Key]
        [DisplayName("Vacuna de Mascota")]
        public int IdVacunaMascota { get; set; }

        [ForeignKey("Vacuna")]
        [DisplayName("Nombre de Vacuna")]
        public int IdVacuna { get; set; }

        [ForeignKey("Mascota")]
        [DisplayName("Nombre de Mascota")]
        public int IdMascota { get; set; }

        [DisplayName("Fecha de Vacuna")]
        public DateTime FechaVacuna { get; set; }

       

        //Nullable Relaciones
        public Vacuna? Vacuna { get; set; }
        public Mascota? Mascota { get; set; }
    }
}
