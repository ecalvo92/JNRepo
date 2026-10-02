using System.ComponentModel.DataAnnotations;

namespace JN_WEB.Models
{
    public class ForgotRequestModel
    {
        [Required]
        public string CorreoElectronico { get; set; } = string.Empty;
    }
}
