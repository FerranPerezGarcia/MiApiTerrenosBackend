using MiApiTareasBackend.Data;
using MiApiTareasBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace MiApiTareasBackend.Controllers
{
    [Route("api/[controller]")] // URL base: /api/Terrains
    [ApiController]
    public class TerrainsController : ControllerBase // Heredar de ControllerBase es crucial
    {
        private readonly AppDbContext _context;

        public TerrainsController(AppDbContext context)
        {
            _context = context;
        }

        // -----------------------------------------------------------------
        // Endpoint: GET /api/Terrains (Obtener todos los terrenos)
        // -----------------------------------------------------------------
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Terrain>>> GetTerrains()
        {
            // Consulta todos los terrenos, incluyendo la información del usuario relacionado (eager loading)
            var terrains = await _context.Terrains
                                         .Include(t => t.User) // Incluir la data del usuario
                                         .ToListAsync();

            if (!terrains.Any())
            {
                return NotFound("No se encontraron terrenos en la base de datos.");
            }

            // Devuelve la lista con código 200
            return Ok(terrains);
        }


        // -----------------------------------------------------------------
        // Endpoint: POST /api/Terrains (Crear un nuevo terreno)
        // -----------------------------------------------------------------
        [HttpPost]
        public async Task<ActionResult<Terrain>> PostTerrain(Terrain terrain)
        {
            // NOTA: Implementar validación de UserId antes de guardar en un entorno real.

            // Asignar la fecha de creación si no se proporciona (aunque ya está en el modelo)
            terrain.CreatedAt = DateTime.UtcNow;

            _context.Terrains.Add(terrain);
            await _context.SaveChangesAsync();

            // Devuelve el nuevo objeto Terrain con código 201 (Created)
            // Se usa nameof(GetTerrain) que sería un método GET por ID que no hemos creado aún.
            return CreatedAtAction(nameof(GetTerrains), new { id = terrain.Id }, terrain);
        }

        // NOTA: Para una API completa, se añadiría:
        // - GET /api/Terrains/{id}
        // - PUT /api/Terrains/{id}
        // - DELETE /api/Terrains/{id}
    }
}
