using System.ComponentModel.DataAnnotations;

namespace JN_WEB.Models
{
    public class ChangePasswordRequestModel
    {
        [Required(ErrorMessage = "La contraseña nueva es obligatoria")]
        public string ContrasennaNueva { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña de confirmación es obligatoria")]
        [Compare(nameof(ContrasennaNueva), ErrorMessage = "Las contraseñas no coinciden")]
        public string ConfirmarContrasenna { get; set; } = string.Empty;

        public string UsuarioId { get; set; } = string.Empty;
    }
}
