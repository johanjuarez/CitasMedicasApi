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
    public class EstatusCitaController : ApiController
    {
        private SistemaCitasEntities db = new SistemaCitasEntities();

        // GET: api/Roles
        public IQueryable<EstatusCita> GetEstatusCitas()
        {
            return db.EstatusCita;
        }

        // GET: api/Roles/5
        [ResponseType(typeof(Roles))]
        public IHttpActionResult GetEstatusCita(int id)
        {
            EstatusCita estatusCita = db.EstatusCita.Find(id);
            if (estatusCita == null)
            {
                return NotFound();
            }

            return Ok(estatusCita);
        }

        // PUT: api/Roles/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEstatusCita(int id, EstatusCita estatusCita)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != estatusCita.EstatusId)
            {
                return BadRequest();
            }

            db.Entry(estatusCita).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstatusExist(id))
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
        [ResponseType(typeof(EstatusCita))]
        public IHttpActionResult PostEstatusCita(EstatusCita estatusCita)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EstatusCita.Add(estatusCita);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = estatusCita.EstatusId }, estatusCita);
        }

        // DELETE: api/Roles/5
        [ResponseType(typeof(Roles))]
        public IHttpActionResult DeleteRoles(int id)
        {
            EstatusCita estatusCita = db.EstatusCita.Find(id);
            if (estatusCita == null)
            {
                return NotFound();
            }

            db.EstatusCita.Remove(estatusCita);
            db.SaveChanges();

            return Ok(estatusCita);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EstatusExist(int id)
        {
            return db.EstatusCita.Count(e => e.EstatusId == id) > 0;
        }
    }
}