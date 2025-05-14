using System.Linq;
using System.Web.Http;
using CitasMedicasApi.Conexion;

namespace CitasMedicasApi.Controllers
{
    [RoutePrefix("api/busqueda")] // Prefijo para los metodos
    public class BusquedaController : ApiController
    {
        private SistemaCitasEntities db = new SistemaCitasEntities();

        // GET: api/busqueda/pacientes?query=parametro
        [HttpGet]
        [Route("pacientes")]
        public IHttpActionResult BuscarPacientes(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return BadRequest("El parámetro 'query' es obligatorio.");
            }

            //Realiza la busqueda por apellidos o por nombre
            var resultados = db.Pacientes
                .Where(p =>
                    p.Nombre.ToLower().Contains(query.ToLower()) ||
                    p.ApellidoPaterno.ToLower().Contains(query.ToLower()) ||
                    p.ApellidoMaterno.ToLower().Contains(query.ToLower())) 
                .Select(p => new
                {
                    p.PacienteId,
                    p.Nombre,
                    p.ApellidoPaterno,
                    p.ApellidoMaterno,
                    p.FechaNacimiento,
                    p.Sexo,
                    p.Correo,
                    p.Telefono
                })
                .ToList();

            if (!resultados.Any())
            {
                return NotFound();
            }

            return Ok(resultados);
        }

        [HttpGet]
        [Route("departamentos")]
        public IHttpActionResult BuscarDepartamentos(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return BadRequest("El parámetro 'query' es obligatorio.");
            }

            // Realiza la búsqueda por nombre de departamento que contenga los caracteres de la búsqueda
            var resultados = db.Departamentos
                .Where(p => p.Nombre.ToLower().Contains(query.ToLower()))  // Filtra por nombre que contenga el query
                .Select(p => new
                {
                    p.DepartamentoId,
                    p.Nombre,
                })
                .ToList();

            // Verifica si no hay resultados
            if (!resultados.Any())
            {
                return Ok(new { mensaje = "Ningún elemento coincide con la búsqueda" });  // Retorna un mensaje de "sin resultados"
            }

            return Ok(resultados);  // Retorna los resultados encontrados
        }



    }
}
