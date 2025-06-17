using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CitasMedicasApi.Models.DTOS
{
    public class CrearNotaDTO
    {
        public int CitaId { get; set; }
        public int PacienteId { get; set; }
        public int MedicoId { get; set; }
    }
}