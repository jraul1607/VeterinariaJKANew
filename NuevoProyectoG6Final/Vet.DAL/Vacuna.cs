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
    [Table("Vacunas")]
    public class Vacuna
    {
        [Key]
        public int IdVacuna { get; set; }

        [DisplayName("Nombre de Vacuna")]
        public string Nombre { get; set; }

        [DisplayName("Tipo de Vacuna")]
        public string TipoVacuna { get; set; }

        public string Producto { get; set; }


        //ICollections
        public ICollection<VacunaMascota> VacunaMascotas { get; set; } = new List<VacunaMascota>();
    }
}

