using CitasMedicasApi.Conexion;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CitasMedicasApi.Factory
{
    public class RolesCreatorConcreto : RolesCreator
    {
        public override Roles CrearRol(string nombre, string descripcion)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre es obligatorio.");

            if (string.IsNullOrWhiteSpace(descripcion))
                throw new ArgumentException("La descripción es obligatoria.");

            return new Roles
            {
                Nombre = nombre.Trim(),
                Descripcion = descripcion.Trim(),
                FechaRegistro = DateTime.Now
            };
        }
    }
}