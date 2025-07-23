using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CitasMedicasApi.Models
{
    public class OtpRequest
    {
        public string Usuario { get; set; }
        public string Codigo { get; set; }
    }
}