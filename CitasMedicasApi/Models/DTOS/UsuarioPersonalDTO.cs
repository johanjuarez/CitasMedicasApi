using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitasMedicasApi.Models.DTOS
{
    public class UsuarioPersonalDTO
    {
        public int UsuarioId { get; set; }
        public string NombreUsuario { get; set; }
        public string NombreReal { get; set; }
        public string ApellidoPaterno { get; set; }
        public int RolUsuario { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Contrasena { get; set; }

        public string Correo { get; set; }
        public string ImagenPerfil { get; set; } 
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Especialidad { get; set; }
        public DateTime FechaRegistro { get; set; }
    }

}
