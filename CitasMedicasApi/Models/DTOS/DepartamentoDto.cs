using CitasMedicasApi.Conexion;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CitasMedicasApi.Models.DTOS
{
    public class DepartamentoDto
    {
        public int DepartamentoId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaRegistro { get; set; }
    }

}