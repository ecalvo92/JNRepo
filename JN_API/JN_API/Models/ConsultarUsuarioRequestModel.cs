using System.ComponentModel.DataAnnotations;

namespace JN_API.Models
{
    public class ConsultarUsuarioRequestModel
    {
        [Required]
        public int IdUsuario { get; set; }
    }
}
