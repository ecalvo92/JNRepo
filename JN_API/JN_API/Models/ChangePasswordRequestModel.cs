using System.ComponentModel.DataAnnotations;

namespace JN_API.Models
{
    public class ChangePasswordRequestModel
    {
        [Required]
        public string ContrasennaNueva { get; set; } = string.Empty;

        [Required]
        [Compare(nameof(ContrasennaNueva))]
        public string ConfirmarContrasenna { get; set; } = string.Empty;

        [Required]
        public string UsuarioId { get; set; } = string.Empty;
    }
}
