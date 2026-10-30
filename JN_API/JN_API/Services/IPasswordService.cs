namespace JN_API.Services
{
    public interface IPasswordService
    {
        string Cifrar(string userId, TimeSpan vigencia);
        string Descifrar(string token);
        Task EnviarCorreo(string to, string subject, string htmlBody);
    }
}
