using System.ComponentModel.DataAnnotations;

namespace JN_WEB.Models
{
    public class RegisterRequestModel
    {
        [Required]
        public string CorreoElectronico { get; set; } = string.Empty;
        [Required]
        public string Contrasenna { get; set; } = string.Empty;
        [Required]
        public string Identificacion { get; set; } = string.Empty;
        [Required]
        public string NombreCompleto { get; set; } = string.Empty;
    }
}
