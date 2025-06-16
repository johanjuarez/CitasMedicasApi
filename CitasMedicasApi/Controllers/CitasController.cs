using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using CitasMedicasApi.Conexion;

namespace CitasMedicasApi.Controllers
{
    public class CitasController : ApiController
    {
        private SistemaCitasEntities db = new SistemaCitasEntities();

        // GET: api/Citas
        // GET: api/Citas
        [HttpGet]
        public IHttpActionResult GetCitas(int? usuarioId = null)
        {
            if (usuarioId == null)
                return Ok(db.Citas.ToList());

            var usuario = db.Usuarios.Find(usuarioId);
            if (usuario == null)
                return NotFound();

            if (usuario.RolId == 4) // Si es médico
            {
                var medico = db.Medicos.FirstOrDefault(m => m.PersonalId == usuario.PersonalId);
                if (medico == null)
                    return NotFound();

                var citasMedico = db.Citas.Where(c => c.MedicoId == medico.MedicoId).ToList();
                return Ok(citasMedico);
            }

            return Ok(db.Citas.ToList());
        }


        // GET: api/Citas/5
        [ResponseType(typeof(Citas))]
        public IHttpActionResult GetCitas(int id)
        {
            Citas citas = db.Citas.Find(id);
            if (citas == null)
            {
                return NotFound();
            }

            return Ok(citas);
        }

        // PUT: api/Citas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCitas(int id, Citas citas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != citas.CitaId)
            {
                return BadRequest();
            }

            db.Entry(citas).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CitasExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Citas
        [ResponseType(typeof(Citas))]
        public IHttpActionResult PostCitas(Citas citas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Citas.Add(citas);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = citas.CitaId }, citas);
        }

        // DELETE: api/Citas/5
        [ResponseType(typeof(Citas))]
        public IHttpActionResult DeleteCitas(int id)
        {
            Citas citas = db.Citas.Find(id);
            if (citas == null)
            {
                return NotFound();
            }

            db.Citas.Remove(citas);
            db.SaveChanges();

            return Ok(citas);
        }

        [HttpGet]
        [Route("api/Citas/Calendario/{usuarioId}")]
        public IHttpActionResult GetCitasCalendario(int usuarioId)
        {
            var usuario = db.Usuarios.Find(usuarioId);
            if (usuario == null)
                return NotFound();

            if (usuario.RolId == 4) // 2 = Médico
            {
                var medico = db.Medicos.FirstOrDefault(m => m.PersonalId == usuario.PersonalId);
                if (medico == null)
                    return NotFound();

                var citasMedico = db.Citas
                    .Where(c => c.MedicoId == medico.MedicoId)
                    .Select(c => new
                    {
                        title = "Cita médica",
                        start = c.FechaHora,
                        end = c.FechaHoraFin
                    }).ToList();

                return Ok(citasMedico);
            }

            // Si no es médico, devuelve todas las citas
            var todasCitas = db.Citas.Select(c => new
            {
                title = "Cita médica",
                start = c.FechaHora,
                end = c.FechaHoraFin
            }).ToList();

            return Ok(todasCitas);
        }


        [HttpGet]
        [Route("api/Citas/Medico/{usuarioId}")]
        public IHttpActionResult GetCitasPorMedico(int usuarioId)
        {
            var usuario = db.Usuarios.Find(usuarioId);
            if (usuario == null)
                return NotFound();

            var personalId = usuario.PersonalId;

            var medico = db.Medicos.FirstOrDefault(m => m.PersonalId == personalId);
            if (medico == null)
                return NotFound();

            var citasDelMedico = db.Citas
                .Where(c => c.MedicoId == medico.MedicoId)
                .ToList();

            return Ok(citasDelMedico);
        }




        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CitasExists(int id)
        {
            return db.Citas.Count(e => e.CitaId == id) > 0;
        }


    

    }
}