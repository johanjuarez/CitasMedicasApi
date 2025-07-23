using CitasMedicasApi.Conexion;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CitasMedicasApi.Factory
{
    public abstract class RolesCreator
    {
        public abstract Roles CrearRol(string nombre, string descripcion);
    }

}