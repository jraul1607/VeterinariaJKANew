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

        public DbSet<Padecimiento> Padecimientos { get; set; } //Lista Padecimiento

        public DbSet<PadecimientoMascota> PadecimientoMascotas { get; set; } //Lista de Padecimiento y Mascotas

        public DbSet<Vacuna> Vacunas { get; set; } //Lista de Vacunas

        public DbSet<VacunaMascota> VacunaMascotas { get; set; } //Lista de VacunasMascotas

        public DbSet<Medicamento> Medicamentos { get; set; } //Lista de Medicamentos

        public DbSet<CitaMedicamento> CitaMedicamentos { get; set; } //Lista de CitaMedicamentos

        public DbSet<Cita> Citas { get; set; } //Lista de Medicamentos

        public DbSet<Mascota> Mascotas { get; set; } //Lista de CitaMedicamentos

        public DbSet<ApplicationUser> ApplicationUser { get; set; } 

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<Mascota>()
        //        .HasOne(m => m.Dueno)
        //        .WithMany(u => u.Mascotas)
        //        .HasForeignKey(m => m.DuenoId)
        //        .OnDelete(DeleteBehavior.Restrict);

        //    modelBuilder.Entity<ApplicationUser>()
        //        .HasMany(u => u.Mascotas)
        //        .WithOne(m => m.Dueno)
        //        .OnDelete(DeleteBehavior.Restrict);

        //    modelBuilder.Entity<Cita>()
        //        .HasOne(c => c.Dueno)
        //        .WithMany(u => u.Citas)
        //        .HasForeignKey(c => c.DuenoId)
        //        .OnDelete(DeleteBehavior.Restrict);

        //    modelBuilder.Entity<Cita>()
        //        .HasOne(c => c.VeterinarioPrincipal)
        //        .WithMany()
        //        .HasForeignKey(c => c.VeterinarioPrincipalId)
        //        .OnDelete(DeleteBehavior.Restrict);

        //    modelBuilder.Entity<Cita>()
        //        .HasOne(c => c.VeterinarioSecundario)
        //        .WithMany()
        //        .HasForeignKey(c => c.VeterinarioSecundarioId)
        //        .OnDelete(DeleteBehavior.Restrict);
        //}

    }
}
