using Microsoft.EntityFrameworkCore;

namespace Vet.DAL
{
    public class VetContext : DbContext
    {

        public VetContext() { }

        public VetContext(DbContextOptions<VetContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer();
        }

        public DbSet<RazaMascota> RazaMascotas { get; set; } //Lista de Razas

        public DbSet<TipoMascota> TipoMascotas { get; set; } //Lista de Tipos

        public DbSet<Rol> Rols { get; set; } //Lista de Rol

        public DbSet<Usuario> Usuarios { get; set; } //Lista de Usuario

        public DbSet<Padecimiento> Padecimientos { get; set; } //Lista Padecimiento

        public DbSet<PadecimientoMascota> PadecimientoMascotas { get; set; } //Lista de Padecimiento y Mascotas

        public DbSet<Vacuna> Vacunas { get; set; } //Lista de Vacunas

        public DbSet<VacunaMascota> VacunaMascotas { get; set; } //Lista de VacunasMascotas

        public DbSet<Medicamento> Medicamentos { get; set; } //Lista de Medicamentos

        public DbSet<CitaMedicamento> CitaMedicamentos { get; set; } //Lista de CitaMedicamentos

        public DbSet<Cita> Citas { get; set; } //Lista de Medicamentos

        public DbSet<Mascota> Mascotas { get; set; } //Lista de CitaMedicamentos


    }
}
