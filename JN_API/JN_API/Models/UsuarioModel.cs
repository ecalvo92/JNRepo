using System.ComponentModel.DataAnnotations;

namespace JN_API.Models
{
    public class UsuarioModel
    {
        [Required]
        public string CorreoElectronico { get; set; } = string.Empty;
        [Required]
        public string Contrasenna { get; set; } = string.Empty;
    }
}
