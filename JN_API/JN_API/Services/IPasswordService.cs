namespace JN_API.Services
{
    public interface IPasswordService
    {
        string Cifrar(string userId, TimeSpan vigencia);
        string Descifrar(string token);
    }
}
