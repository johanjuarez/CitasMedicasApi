using System;
using System.Collections.Generic;

namespace CitasMedicasApi.Helpers
{
    public class OtpService
    {
        // Diccionario estático que almacena: clave = usuario, valor = (código OTP, fecha de expiración)
        private static readonly Dictionary<string, (string Codigo, DateTime Expira)> _otps 
            = new Dictionary<string, (string, DateTime)>();

        // Método para generar un código OTP para un usuario, válido por "minutos" (default 5)
        public string GenerarCodigo(string usuario, int minutos = 5)
        {
            var random = new Random();
            // Generamos un número aleatorio de 6 dígitos como código OTP
            var codigo = random.Next(100000, 999999).ToString();

            // Guardamos en el diccionario el código junto con la fecha de expiración
            _otps[usuario] = (codigo, DateTime.Now.AddMinutes(minutos));

            // Retornamos el código generado para enviarlo al usuario (por correo, SMS, etc)
            return codigo;
        }

        // Método para verificar si un código OTP es válido para un usuario
        public bool VerificarCodigo(string usuario, string codigo)
        {
            // Si no existe un código almacenado para ese usuario, no es válido
            if (!_otps.ContainsKey(usuario))
                return false;

            // Obtenemos el código y la fecha de expiración almacenados para ese usuario
            var (codigoGuardado, expira) = _otps[usuario];

            // Si el código ya expiró o el código ingresado no coincide, no es válido
            if (DateTime.Now > expira || codigoGuardado != codigo)
                return false;

            // Código válido: lo eliminamos para que no se pueda reutilizar
            _otps.Remove(usuario);

            // Retornamos true para indicar que el OTP es correcto y válido
            return true;
        }
    }
}
