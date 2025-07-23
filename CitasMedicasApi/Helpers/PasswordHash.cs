using CitasMedicasApi.Models;
using System.Security.Cryptography;
using System.Text;

namespace CitasMedicasApi.Helpers
{
    public static class PasswordHash
    {
        public static HashSalt Crear(string password)
        {
            using (var hmac = new HMACSHA512())
            {
                return new HashSalt
                {
                    Salt = hmac.Key,
                    Hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password))
                };
            }
        }

        public static bool Verificar(string password, byte[] hashGuardado, byte[] saltGuardado)
        {
            using (var hmac = new HMACSHA512(saltGuardado))
            {
                var hashComputado = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < hashComputado.Length; i++)
                {
                    if (hashComputado[i] != hashGuardado[i])
                        return false;
                }
            }
            return true;
        }
    }
}
