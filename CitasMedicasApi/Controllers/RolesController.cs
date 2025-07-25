﻿using CitasMedicasApi.Conexion;
using CitasMedicasApi.Factory;
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

namespace CitasMedicasApi.Controllers
{
    public class RolesController : ApiController
    {
        private SistemaCitasEntities db = new SistemaCitasEntities();

        // GET: api/Roles
        public IQueryable<Roles> GetRoles()
        {
            return db.Roles;
        }

        // GET: api/Roles/5
        [ResponseType(typeof(Roles))]
        public IHttpActionResult GetRoles(int id)
        {
            Roles roles = db.Roles.Find(id);
            if (roles == null)
            {
                return NotFound();
            }

            return Ok(roles);
        }

        // PUT: api/Roles/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRoles(int id, Roles roles)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != roles.RolId)
            {
                return BadRequest();
            }

            db.Entry(roles).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RolesExists(id))
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

        [ResponseType(typeof(Roles))]
        public IHttpActionResult PostRoles(Roles roles)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            RolesCreator creador = new RolesCreatorConcreto();

            Roles nuevoRol;
            try
            {
                nuevoRol = creador.CrearRol(roles.Nombre, roles.Descripcion);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            db.Roles.Add(nuevoRol);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = nuevoRol.RolId }, nuevoRol);
        }


        // DELETE: api/Roles/5
        [ResponseType(typeof(Roles))]
        public IHttpActionResult DeleteRoles(int id)
        {
            Roles roles = db.Roles.Find(id);
            if (roles == null)
            {
                return NotFound();
            }

            db.Roles.Remove(roles);
            db.SaveChanges();

            return Ok(roles);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RolesExists(int id)
        {
            return db.Roles.Count(e => e.RolId == id) > 0;
        }
    }
}