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
    public class ConsultoriosController : ApiController
    {
        private SistemaCitasEntities db = new SistemaCitasEntities();

        // GET: api/Consultorios
        public IQueryable<Consultorios> GetConsultorios()
        {
            return db.Consultorios;
        }

        // GET: api/Consultorios/5
        [ResponseType(typeof(Consultorios))]
        public IHttpActionResult GetConsultorios(int id)
        {
            Consultorios consultorios = db.Consultorios.Find(id);
            if (consultorios == null)
            {
                return NotFound();
            }

            return Ok(consultorios);
        }

        // PUT: api/Consultorios/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutConsultorios(int id, Consultorios consultorios)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != consultorios.ConsultorioId)
            {
                return BadRequest();
            }

            db.Entry(consultorios).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConsultoriosExists(id))
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

        // POST: api/Consultorios
        [ResponseType(typeof(Consultorios))]
        public IHttpActionResult PostConsultorios(Consultorios consultorios)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Consultorios.Add(consultorios);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = consultorios.ConsultorioId }, consultorios);
        }

        // DELETE: api/Consultorios/5
        [ResponseType(typeof(Consultorios))]
        public IHttpActionResult DeleteConsultorios(int id)
        {
            Consultorios consultorios = db.Consultorios.Find(id);
            if (consultorios == null)
            {
                return NotFound();
            }

            db.Consultorios.Remove(consultorios);
            db.SaveChanges();

            return Ok(consultorios);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ConsultoriosExists(int id)
        {
            return db.Consultorios.Count(e => e.ConsultorioId == id) > 0;
        }
    }
}