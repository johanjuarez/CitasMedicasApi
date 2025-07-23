using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace CitasMedicasApi.Services
{
    public class EmailService
    {
        private readonly string correoOrigen = "johangonzalezjuarez08@gmail.com";
        private readonly string claveCorreo = "wewq ubsn xyvb rmzj"; 

        public async Task EnviarCodigoValidacionAsync(string destinatario, string codigo)
        {
            var mensaje = new MailMessage
            {
                From = new MailAddress(correoOrigen, "Citas Médicas"),
                Subject = "Código de validación",
                Body = $"Tu código de verificación es: {codigo}",
                IsBodyHtml = false
            };

            mensaje.To.Add(destinatario);

            using (var smtp = new SmtpClient("smtp.gmail.com", 587))
            {
                smtp.Credentials = new NetworkCredential(correoOrigen, claveCorreo);
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(mensaje);
            }
        }
    }
}
