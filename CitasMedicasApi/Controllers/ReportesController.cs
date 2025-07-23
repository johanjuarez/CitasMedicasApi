using System.Linq;
using System.Web.Http;
using CitasMedicasApi.Conexion;
using CitasMedicasApi.Models.DTOS;

namespace CitasMedicasApi.Controllers
{
    public class ReportesController : ApiController
    {
        private readonly SistemaCitasEntities db = new SistemaCitasEntities();
        [HttpGet]
        [Route("api/Reportes/CitaDetalle/{id}")]
        public IHttpActionResult GetCitaDetalle(int id)
        {
            var resultado = (from c in db.Citas
                             join p in db.Pacientes on c.PacienteId equals p.PacienteId
                             join m in db.Medicos on c.MedicoId equals m.MedicoId
                             join per in db.Personal on m.PersonalId equals per.PersonalId
                             join co in db.Consultorios on c.ConsultorioId equals co.ConsultorioId
                             where c.CitaId == id
                             select new CitaDetalleDto
                             {
                                 CitaId = c.CitaId,
                                 NombrePaciente = p.Nombre + " " + p.ApellidoPaterno + " " + p.ApellidoMaterno,
                                 Sexo = p.Sexo,
                                 FechaNacimiento = p.FechaNacimiento,
                                 Correo = p.Correo,
                                 Telefono = p.Telefono,
                                 NombreMedico = per.Nombre + " " + per.ApellidoPaterno + " " + per.ApellidoMaterno,
                                 Especialidad = m.Especialidad,
                                 FechaHora = c.FechaHora,
                                 FechaHoraFin = c.FechaHoraFin,
                                 Motivo = c.Motivo,
                                 NombreConsultorio = co.Nombre
                             }).FirstOrDefault();

            if (resultado == null)
                return NotFound();

            return Ok(resultado);
        }





    }
}
