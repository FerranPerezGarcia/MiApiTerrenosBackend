using Microsoft.EntityFrameworkCore;
using MiApiTareasBackend.Models;

namespace MiApiTareasBackend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Terrain> Terrains { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Forzar nombre de tabla en minúsculas (opcional si usas snake_case global)
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<Terrain>().ToTable("terrains");

            // Registrar la extensión de PostgreSQL para generar UUIDs
            modelBuilder.HasPostgresExtension("uuid-ossp");

            // Configurar la relación explícita para Terrenos
            modelBuilder.Entity<Terrain>()
                .HasOne(t => t.User)         // Un Terrain tiene un User
                .WithMany()                  // Un User puede tener muchos Terrains
                .HasForeignKey(t => t.UserId) // La clave foránea es UserId
                .OnDelete(DeleteBehavior.Cascade); // Mapea tu "on delete CASCADE"

            base.OnModelCreating(modelBuilder);
        }
    }
}
