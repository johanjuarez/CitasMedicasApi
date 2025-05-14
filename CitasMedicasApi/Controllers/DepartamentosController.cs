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
    public class DepartamentosController : ApiController
    {
        private SistemaCitasEntities db = new SistemaCitasEntities();

        // GET: api/Departamentos
        public IQueryable<Departamentos> GetDepartamentos()
        {
            return db.Departamentos;
        }

        // GET: api/Departamentos/5
        [ResponseType(typeof(Departamentos))]
        public IHttpActionResult GetDepartamentos(int id)
        {
            Departamentos departamentos = db.Departamentos.Find(id);
            if (departamentos == null)
            {
                return NotFound();
            }

            return Ok(departamentos);
        }

        // PUT: api/Departamentos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDepartamentos(int id, Departamentos departamentos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != departamentos.DepartamentoId)
            {
                return BadRequest();
            }

            db.Entry(departamentos).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartamentosExists(id))
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

        // POST: api/Departamentos
        [ResponseType(typeof(Departamentos))]
        public IHttpActionResult PostDepartamentos(Departamentos departamentos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Departamentos.Add(departamentos);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = departamentos.DepartamentoId }, departamentos);
        }

        // DELETE: api/Departamentos/5
        [ResponseType(typeof(Departamentos))]
        public IHttpActionResult DeleteDepartamentos(int id)
        {
            Departamentos departamentos = db.Departamentos.Find(id);
            if (departamentos == null)
            {
                return NotFound();
            }

            db.Departamentos.Remove(departamentos);
            db.SaveChanges();

            return Ok(departamentos);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DepartamentosExists(int id)
        {
            return db.Departamentos.Count(e => e.DepartamentoId == id) > 0;
        }
    }
}