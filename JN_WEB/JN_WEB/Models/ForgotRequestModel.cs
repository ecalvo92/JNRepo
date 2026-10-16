using System.ComponentModel.DataAnnotations;

namespace JN_WEB.Models
{
    public class ForgotRequestModel
    {
        [Required(ErrorMessage = "El correo electrónico es obligatorio")]
        public string CorreoElectronico { get; set; } = string.Empty;
    }
}
