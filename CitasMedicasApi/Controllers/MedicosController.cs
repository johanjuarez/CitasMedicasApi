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
    public class MedicosController : ApiController
    {
        private SistemaCitasEntities db = new SistemaCitasEntities();

        // GET: api/Medicos
        public IQueryable<Medicos> GetMedicos()
        {
            return db.Medicos;
        }

        // GET: api/Medicos/5
        [ResponseType(typeof(Medicos))]
        public IHttpActionResult GetMedicos(int id)
        {
            Medicos medicos = db.Medicos.Find(id);
            if (medicos == null)
            {
                return NotFound();
            }

            return Ok(medicos);
        }

        // PUT: api/Medicos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMedicos(int id, Medicos medicos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != medicos.MedicoId)
            {
                return BadRequest();
            }

            db.Entry(medicos).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicosExists(id))
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

        // POST: api/Medicos
        [ResponseType(typeof(Medicos))]
        public IHttpActionResult PostMedicos(Medicos medicos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Medicos.Add(medicos);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = medicos.MedicoId }, medicos);
        }

        // DELETE: api/Medicos/5
        [ResponseType(typeof(Medicos))]
        public IHttpActionResult DeleteMedicos(int id)
        {
            Medicos medicos = db.Medicos.Find(id);
            if (medicos == null)
            {
                return NotFound();
            }

            db.Medicos.Remove(medicos);
            db.SaveChanges();

            return Ok(medicos);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MedicosExists(int id)
        {
            return db.Medicos.Count(e => e.MedicoId == id) > 0;
        }
    }
}