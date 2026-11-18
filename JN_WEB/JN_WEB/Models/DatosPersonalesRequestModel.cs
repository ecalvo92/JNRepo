using System.ComponentModel.DataAnnotations;

namespace JN_WEB.Models
{
    public class DatosPersonalesRequestModel
    {
        [Required(ErrorMessage = "La identificación es obligatoria")]
        public string Identificacion { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre completo es obligatorio")]
        public string NombreCompleto { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo electrónico es obligatorio")]
        public string CorreoElectronico { get; set; } = string.Empty;

        public int IdUsuario { get; set; }
    }
}
