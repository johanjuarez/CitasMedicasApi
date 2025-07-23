using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CitasMedicasApi.Models
{
    public class EmailConfig
    {
        public string SmtpHost { get; set; } = "smtp.gmail.com";
        public int SmtpPort { get; set; } = 587;
        public string FromEmail { get; set; } = "johangonzalezjuarez08@gmail.com";
        public string FromPassword { get; set; } = "tucontraseña";
    }
}