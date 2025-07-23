using CitasMedicasApi.Conexion;
using CitasMedicasApi.Helpers;
using CitasMedicasApi.Models;
using CitasMedicasApi.Services;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace CitasMedicasApi.Controllers
{
    public class AuthController : ApiController
    {
        private readonly SistemaCitasEntities db = new SistemaCitasEntities();
        private readonly OtpService otpService = new OtpService();
        private readonly EmailService emailService = new EmailService();

        // Login: valida usuario y contraseña, genera y envía OTP al correo
        [HttpPost]
        [Route("api/Auth/Login")]
        public async Task<IHttpActionResult> Login([FromBody] Logueo logueo)
        {
            if (string.IsNullOrWhiteSpace(logueo.Usuario) || string.IsNullOrWhiteSpace(logueo.Contraseña))
                return BadRequest("Usuario y contraseña son requeridos.");

            var usuario = db.Usuarios.FirstOrDefault(u => u.Usuario == logueo.Usuario);
            if (usuario == null || !PasswordHash.Verificar(logueo.Contraseña, usuario.PasswordHash, usuario.PasswordSalt))
                return Unauthorized();

            // Genera código OTP válido 5 minutos y envía al correo del usuario
            var codigo = otpService.GenerarCodigo(usuario.Usuario);
            await emailService.EnviarCodigoValidacionAsync(usuario.Correo, codigo);

            // Crear respuesta con datos del usuario
            var loginResponse = new LoginResponse
            {
                IdUsuario = usuario.UsuarioId,
                Nombre = usuario.Usuario,
                RolId = usuario.RolId
            };

            return Ok(loginResponse);
        }


        [HttpPost]
        [Route("api/Auth/VerificacionCodigo")]
        public IHttpActionResult ValidarOtp([FromBody] OtpRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Usuario) || string.IsNullOrWhiteSpace(request.Codigo))
                return BadRequest("Usuario y código OTP son requeridos.");

            bool esValido = otpService.VerificarCodigo(request.Usuario, request.Codigo);
            if (!esValido)
                return BadRequest("Código OTP inválido o expirado.");

            var usuario = db.Usuarios.FirstOrDefault(u => u.Usuario == request.Usuario);
            if (usuario == null)
                return NotFound();

            return Ok(new
            {
                Mensaje = "OTP válido, login exitoso",
                IdUsuario = usuario.UsuarioId,
                Nombre = usuario.Usuario,
                RolId = usuario.RolId
            });
        }
    }
}
