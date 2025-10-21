using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MiApiTareasBackend.Models
{
    public class Terrain
    {
        [Key]
        // Se mapea a 'id' serial not null primary key
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        // Se mapea a 'name' character varying(255) not null
        public string Name { get; set; } = string.Empty;

        // Se mapea a 'matrix' jsonb, que almacena datos JSON. Usamos string para Jsonb en EF Core.
        public string? Matrix { get; set; }

        // Se mapea a 'max_height'
        public int? MaxHeight { get; set; }

        // Se mapea a 'resolution'
        public int? Resolution { get; set; }

        // Se mapea a 'created_at' timestamp
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        // -----------------------------------------------------------------
        // Clave Foránea a la tabla 'users'
        // -----------------------------------------------------------------

        // Se mapea a 'user_id' integer null foreign key
        public int? UserId { get; set; }

        // Propiedad de Navegación: Permite cargar el objeto User asociado (si existe)
        [JsonIgnore] // Previene ciclos infinitos al serializar a JSON
        public User? User { get; set; }
    }
}
