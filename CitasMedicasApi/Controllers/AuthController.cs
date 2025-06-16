using System.Linq;
using System.Web.Http;
using CitasMedicasApi.Conexion;
using CitasMedicasApi.Models;

namespace CitasMedicasApi.Controllers
{
    public class AuthController : ApiController
    {
        private readonly SistemaCitasEntities db = new SistemaCitasEntities();

        [HttpPost]
        [Route("api/Auth/Login")]
        public IHttpActionResult Login([FromBody] Logueo logueo)
        {
            if (logueo?.Usuario == null || string.IsNullOrEmpty(logueo.Contraseña))
                return BadRequest("Correo y contraseña son requeridos.");

            var usuario = db.Usuarios
                            .FirstOrDefault(u => u.Usuario == logueo.Usuario && u.Contraseña == logueo.Contraseña);

            if (usuario == null)
                return Unauthorized();

            return Ok(new LoginResponse
            {
                IdUsuario = usuario.UsuarioId,
                Nombre = usuario.Usuario,
                RolId = usuario.RolId,
                Mensaje = "Login exitoso"
            }); 
        }
    }
}
