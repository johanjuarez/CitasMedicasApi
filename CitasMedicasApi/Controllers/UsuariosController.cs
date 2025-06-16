using CitasMedicasApi.Conexion;
using CitasMedicasApi.Models.DTOS;
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
    public class UsuariosController : ApiController
    {
        private SistemaCitasEntities db = new SistemaCitasEntities();

        // GET: api/Usuarios
        public IQueryable<Usuarios> GetUsuarios()
        {
            return db.Usuarios;
        }

        // GET: api/Usuarios/5
        [ResponseType(typeof(Usuarios))]
        public IHttpActionResult GetUsuarios(int id)
        {
            Usuarios usuarios = db.Usuarios.Find(id);
            if (usuarios == null)
            {
                return NotFound();
            }

            return Ok(usuarios);
        }

        // PUT: api/Usuarios/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUsuarios(int id, Usuarios usuarios)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != usuarios.UsuarioId)
            {
                return BadRequest();
            }

            db.Entry(usuarios).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuariosExists(id))
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

        // POST: api/Usuarios
        [ResponseType(typeof(Usuarios))]
        public IHttpActionResult PostUsuarios(Usuarios usuarios)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Usuarios.Add(usuarios);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = usuarios.UsuarioId }, usuarios);
        }

        // DELETE: api/Usuarios/5
        [ResponseType(typeof(Usuarios))]
        public IHttpActionResult DeleteUsuarios(int id)
        {
            Usuarios usuarios = db.Usuarios.Find(id);
            if (usuarios == null)
            {
                return NotFound();
            }

            db.Usuarios.Remove(usuarios);
            db.SaveChanges();

            return Ok(usuarios);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UsuariosExists(int id)
        {
            return db.Usuarios.Count(e => e.UsuarioId == id) > 0;
        }

        [HttpGet]
        [Route("api/Usuarios/GetUsuarioConPersonal/{usuarioId}")]
        public IHttpActionResult GetUsuarioConPersonal(int usuarioId)
        {
            var data = (from u in db.Usuarios
                        join p in db.Personal on u.PersonalId equals p.PersonalId
                        where u.UsuarioId == usuarioId
                        select new UsuarioPersonalDTO
                        {
                            UsuarioId = u.UsuarioId,
                            NombreUsuario = u.Usuario,
                            Correo = u.Correo,
                            ImagenPerfil = u.RutaImagen,
                            RolUsuario = u.RolId,
                            Telefono = p.Telefono,
                            Direccion = p.Direccion,
                            NombreReal = p.Nombre,
                            ApellidoMaterno = p.ApellidoMaterno,
                            ApellidoPaterno = p.ApellidoPaterno,
                            FechaRegistro = p.FechaContratacion
                        }).FirstOrDefault();

            if (data == null)
                return NotFound();

            return Ok(data);
        }

    }
}