using CitasMedicasApi.Conexion;
using CitasMedicasApi.Models.DTOS;
using CitasMedicasFront.Models.DTOS;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace CitasMedicasApi.Controllers
{
    public class DepartamentosController : ApiController
    {
        private SistemaCitasEntities db = new SistemaCitasEntities();

        // GET: api/Departamentos
        [HttpGet]
        [ResponseType(typeof(EncryptedDto))]
        public IHttpActionResult GetDepartamentos()
        {
            var departamentos = db.Departamentos.ToList();

            var dtoList = departamentos.Select(d => new DepartamentoDto
            {
                DepartamentoId = d.DepartamentoId,
                Nombre = d.Nombre,
                Descripcion = d.Descripcion,
                FechaRegistro = d.FechaRegistro
            }).ToList();

            var json = JsonConvert.SerializeObject(dtoList);
            var cifrado = Encriptado.Encriptar(json);

            return Ok(new EncryptedDto { Data = cifrado });
        }

        // GET: api/Departamentos/5
        [HttpGet]
        [ResponseType(typeof(EncryptedDto))]
        public IHttpActionResult GetDepartamentos(int id)
        {
            var d = db.Departamentos.Find(id);
            if (d == null)
                return NotFound();

            var dto = new DepartamentoDto
            {
                DepartamentoId = d.DepartamentoId,
                Nombre = d.Nombre,
                Descripcion = d.Descripcion,
                FechaRegistro = d.FechaRegistro
            };

            var json = JsonConvert.SerializeObject(dto);
            var cifrado = Encriptado.Encriptar(json);

            return Ok(new EncryptedDto { Data = cifrado });
        }

        // PUT: api/Departamentos/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDepartamentos(int id, [FromBody] EncryptedDto encryptedDto)
        {
            var json = Encriptado.Desencriptar(encryptedDto.Data);
            var dto = JsonConvert.DeserializeObject<DepartamentoDto>(json);

            if (id != dto.DepartamentoId)
                return BadRequest();

            var entidad = new Departamentos
            {
                DepartamentoId = dto.DepartamentoId,
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                FechaRegistro = dto.FechaRegistro
            };

            db.Entry(entidad).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartamentosExists(id))
                    return NotFound();
                else
                    throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Departamentos
        [HttpPost]
        [ResponseType(typeof(string))]
        public IHttpActionResult PostDepartamentos([FromBody] EncryptedDto encryptedDto)
        {
            var json = Encriptado.Desencriptar(encryptedDto.Data);
            var dto = JsonConvert.DeserializeObject<DepartamentoDto>(json);

            var entidad = new Departamentos
            {
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                FechaRegistro = dto.FechaRegistro
            };

            db.Departamentos.Add(entidad);
            db.SaveChanges();

            return Ok("Departamento creado");
        }

        // DELETE: api/Departamentos/5
        [HttpDelete]
        [ResponseType(typeof(string))]
        public IHttpActionResult DeleteDepartamentos(int id)
        {
            var departamento = db.Departamentos.Find(id);
            if (departamento == null)
                return NotFound();

            db.Departamentos.Remove(departamento);
            db.SaveChanges();

            return Ok("Departamento eliminado");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();
            base.Dispose(disposing);
        }

        private bool DepartamentosExists(int id)
        {
            return db.Departamentos.Any(e => e.DepartamentoId == id);
        }
    }
}
