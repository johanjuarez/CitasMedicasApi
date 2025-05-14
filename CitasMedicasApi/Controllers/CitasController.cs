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
        public IQueryable<Citas> GetCitas()
        {
            return db.Citas;
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