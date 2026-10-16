namespace JN_API.Models
{
    public class UsuarioResponse : ResponseApi
    {
        public int IdUsuario { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
    }
}
