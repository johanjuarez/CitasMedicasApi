﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CitasMedicasApi.Models
{
    public class LoginResponse
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public int RolId { get; set; }
        public string Mensaje { get; set; }
    }
}