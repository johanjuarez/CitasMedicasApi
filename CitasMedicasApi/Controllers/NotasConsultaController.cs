using CitasMedicasApi.Conexion;
using CitasMedicasApi.Models;
using CitasMedicasApi.Models.DTOS;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace CitasMedicasApi.Controllers
{
    public class NotasConsultaController : ApiController
    {
        private readonly SistemaCitasEntities db = new SistemaCitasEntities();

        public IQueryable<NotasConsulta> GetNotasConsulta()
        {
            return db.NotasConsulta;
        }
        // GET: api/Roles/5
        [ResponseType(typeof(NotasConsulta))]
        public IHttpActionResult GetNotasConsulta(int id)
        {
            NotasConsulta notasConsulta = db.NotasConsulta.Find(id);
            if (notasConsulta == null)
            {
                return NotFound();
            }

            return Ok(notasConsulta);
        }

        // PUT: api/Roles/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutNotasConsulta(int id, NotasConsulta notasConsulta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != notasConsulta.NotaId)
            {
                return BadRequest();
            }

            db.Entry(notasConsulta).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotasExist(id))
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

        // POST: api/Roles
        [ResponseType(typeof(NotasConsulta))]
        public IHttpActionResult PostNotasConsulta(NotasConsulta notasConsulta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.NotasConsulta.Add(notasConsulta);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = notasConsulta.NotaId }, notasConsulta);
        }

        // DELETE: api/Roles/5
        [ResponseType(typeof(NotasConsulta))]
        public IHttpActionResult DeleteNotasConsulta(int id)
        {
            NotasConsulta notasConsulta = db.NotasConsulta.Find(id);
            if (notasConsulta == null)
            {
                return NotFound();
            }

            db.NotasConsulta.Remove(notasConsulta);
            db.SaveChanges();

            return Ok(notasConsulta);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NotasExist(int id)
        {
            return db.NotasConsulta.Count(e => e.NotaId == id) > 0;
        }




        [HttpPost]
        [Route("api/NotasConsulta/CrearNota")]
        public IHttpActionResult CrearNota([FromBody] CrearNotaDTO datos)
        {
            if (datos == null)
                return BadRequest("Datos inválidos.");

            var nota = new NotasConsulta
            {
                CitaId = datos.CitaId,
                PacienteId = datos.PacienteId,
                MedicoId = datos.MedicoId,
                FechaRegistro = DateTime.Now,
            };

            db.NotasConsulta.Add(nota);
            db.SaveChanges();

            return Ok(new { notaId = nota.NotaId });
        }


    }
}
