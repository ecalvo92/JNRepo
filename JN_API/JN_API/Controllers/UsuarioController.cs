using Dapper;
using JN_API.Models;
using JN_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace JN_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController(IConfiguration _configuration) : ControllerBase
    {
        [HttpPut]
        [Route("ChangePassword")]
        public IActionResult ChangePassword(ChangePasswordRequestModel model)
        {
            using var context = new SqlConnection(_configuration.GetValue<string>("Variables:ConnectionString"));

            var parameters = new DynamicParameters();
            parameters.Add("@UsuarioId", model.UsuarioId);
            parameters.Add("@Contrasenna", model.ContrasennaNueva);
            var response = context.Execute("sp_ActualizarContrasenna", parameters);

            if (response <= 0)
                return UnprocessableEntity(new UsuarioResponse { Mensaje = "No se actualizó la información correctamente" });

            return Ok(new UsuarioResponse { Mensaje = "Se actualizó la información correctamente" });
        }
    }
}
