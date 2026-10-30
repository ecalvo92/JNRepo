using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System.Security.Cryptography;
using System.Text;

namespace JN_API.Services
{
    public class PasswordService(IConfiguration _configuration) : IPasswordService
    {
        private readonly byte[] Key = Convert.FromBase64String("sy+v61jJHH5tZTjth2g+PwKpfUiNXWA0ImlUWxikTKUedfPEC6zrkDSzsneTbfkTXJnaEchH7BcVBF4XkqIMDw==");

        public string Cifrar(string userId, TimeSpan vigencia)
        {
            var payload = Encoding.UTF8.GetBytes($"{userId}|{DateTime.UtcNow.Add(vigencia).Ticks}");
            using (var aes = Aes.Create())
            using (var hmac = new HMACSHA256(Key.AsSpan(32).ToArray()))
            {
                aes.Key = Key.AsSpan(0, 32).ToArray();
                aes.GenerateIV();
                var cifrado = aes.CreateEncryptor().TransformFinalBlock(payload, 0, payload.Length);

                var datos = new byte[16 + cifrado.Length + 32];
                Buffer.BlockCopy(aes.IV, 0, datos, 0, 16);
                Buffer.BlockCopy(cifrado, 0, datos, 16, cifrado.Length);
                Buffer.BlockCopy(hmac.ComputeHash(datos, 0, 16 + cifrado.Length), 0, datos, 16 + cifrado.Length, 32);

                return Convert.ToBase64String(datos).TrimEnd('=').Replace('+', '-').Replace('/', '_');
            }
        }

        public string Descifrar(string token)
        {
            try
            {
                var b64 = token.Replace('-', '+').Replace('_', '/');
                var datos = Convert.FromBase64String(b64.PadRight(b64.Length + (4 - b64.Length % 4) % 4, '='));
                if (datos.Length < 16 + 16 + 32) return string.Empty;

                int largoMac = datos.Length - 32;
                using (var hmac = new HMACSHA256(Key.AsSpan(32).ToArray()))
                {
                    var macEsperado = hmac.ComputeHash(datos, 0, largoMac);
                    int diff = 0;
                    for (int i = 0; i < 32; i++) diff |= macEsperado[i] ^ datos[largoMac + i];
                    if (diff != 0) return string.Empty;
                }

                using (var aes = Aes.Create())
                {
                    aes.Key = Key.AsSpan(0, 32).ToArray();
                    aes.IV = datos.AsSpan(0, 16).ToArray();
                    var plano = aes.CreateDecryptor().TransformFinalBlock(datos, 16, largoMac - 16);
                    var partes = Encoding.UTF8.GetString(plano).Split('|');

                    if (DateTime.UtcNow.Ticks > long.Parse(partes[1])) return string.Empty;
                    return partes[0];
                }
            }
            catch { return string.Empty; }
        }

        public async Task EnviarCorreo(string to, string subject, string htmlBody)
        {
            var cuenta = _configuration.GetValue<string>("Variables:CuentaCorreo") ?? string.Empty;
            var clave = _configuration.GetValue<string>("Variables:ClaveCorreo") ?? string.Empty;

            if (string.IsNullOrEmpty(clave))
                return;

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Web JN", cuenta));
            message.To.Add(MailboxAddress.Parse(to));
            message.Subject = subject;
            message.Body = new BodyBuilder { HtmlBody = htmlBody }.ToMessageBody();

            using var client = new SmtpClient();
            await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(cuenta, clave);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}