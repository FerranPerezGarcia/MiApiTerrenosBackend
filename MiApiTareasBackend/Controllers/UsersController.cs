using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiApiTareasBackend.Data;
using MiApiTareasBackend.Models;

namespace MiApiTareasBackend.Controllers
{
    [Route("api/[controller]")] // Define la URL base: /api/Users
    [ApiController]
    public class UsersController : ControllerBase
    {
        // El contexto de la base de datos que se inyecta
        private readonly AppDbContext _context;

        // Inyección de Dependencias
        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        // -----------------------------------------------------------------
        // Endpoint: GET (Obtener todos los usuarios)
        // Ruta: GET /api/Users
        // -----------------------------------------------------------------
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            // Verifica que el DbSet sea el correcto (Users) y usa ToListAsync() para la consulta
            var users = await _context.Users.ToListAsync();

            if (!users.Any())
            {
                return NotFound("No se encontraron usuarios en la base de datos.");
            }

            return Ok(users);
        }

        // Aquí irían los métodos para Registro (POST), Login (POST/JWT), etc.

        // Ejemplo de POST para registrar un nuevo usuario
        [HttpPost]
        public async Task<ActionResult<User>> RegisterUser(User user)
        {
            // Lógica simple: Hash de contraseña y validación irían aquí

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Devuelve el nuevo usuario
            return CreatedAtAction(nameof(GetUsers), new { id = user.Id }, user);
        }
    }
}
