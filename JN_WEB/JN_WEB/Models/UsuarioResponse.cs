namespace JN_WEB.Models
{
    public class UsuarioResponse
    {
        public string Mensaje { get; set; } = string.Empty;
        public int IdUsuario { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public string Identificacion { get; set; } = string.Empty;
        public string CorreoElectronico { get; set; } = string.Empty;
        public bool Estado { get; set; }
        public short IdRol { get; set; }
        public string NombreRol { get; set; } = string.Empty;
    }
}
