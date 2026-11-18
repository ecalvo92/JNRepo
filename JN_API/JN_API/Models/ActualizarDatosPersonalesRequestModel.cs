using System.ComponentModel.DataAnnotations;

namespace JN_API.Models
{
    public class ActualizarDatosPersonalesRequestModel
    {
        [Required]
        public string CorreoElectronico { get; set; } = string.Empty;
        [Required]
        public int IdUsuario { get; set; }
        [Required]
        public string Identificacion { get; set; } = string.Empty;
        [Required]
        public string NombreCompleto { get; set; } = string.Empty;
    }
}
