using System.Linq;
using System.Web.Http;
using System.Web.UI.WebControls;
using CitasMedicasApi.Conexion;
using CitasMedicasApi.Models;

namespace CitasMedicasApi.Controllers
{
    public class AuthController : ApiController
    {
        private SistemaCitasEntities db = new SistemaCitasEntities();

        [HttpPost]
        [Route("api/Auth/Login")]
        public IHttpActionResult Login([FromBody] Logueo logueo
            )
        {
            if (logueo == null || string.IsNullOrEmpty(logueo.Usuario) || string.IsNullOrEmpty(logueo.Contraseña))
                return BadRequest("Correo y contraseña son requeridos.");

            var usuario = db.Usuarios.FirstOrDefault(u => u.Usuario == logueo.Usuario && u.Contraseña == logueo.Contraseña);
            if (usuario == null)
                return Unauthorized();

            return Ok("Login exitoso");
        }
    }

    
}
