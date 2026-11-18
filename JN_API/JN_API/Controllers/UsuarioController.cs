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

        [HttpPost]
        [Route("ConsultarUsuario")]
        public IActionResult ConsultarUsuario(ConsultarUsuarioRequestModel model)
        {
            using var context = new SqlConnection(_configuration.GetValue<string>("Variables:ConnectionString"));

            var parameters = new DynamicParameters();
            parameters.Add("@UsuarioId", model.IdUsuario);

            var response = context.QueryFirstOrDefault<UsuarioResponse>("sp_ConsultarIdUsuario", parameters);

            if (response == null)
                return NotFound(new UsuarioResponse { Mensaje = "No se validó la información correctamente" });

            response.Contrasenna = string.Empty;
            return Ok(response);
        }

        [HttpPut]
        [Route("ActualizarDatosPersonales")]
        public IActionResult ActualizarDatosPersonales(ActualizarDatosPersonalesRequestModel model)
        {
            using var context = new SqlConnection(_configuration.GetValue<string>("Variables:ConnectionString"));

            var parameters = new DynamicParameters();
            parameters.Add("@UsuarioId", model.IdUsuario);
            parameters.Add("@CorreoElectronico", model.CorreoElectronico);
            parameters.Add("@Identificacion", model.Identificacion);
            parameters.Add("@NombreCompleto", model.NombreCompleto);

            var response = context.Execute("sp_ActualizarDatosPersonales", parameters);

            if (response <= 0)
                return UnprocessableEntity(new UsuarioResponse { Mensaje = "No se actualizó la información correctamente" });

            return Ok(new UsuarioResponse { Mensaje = "Se actualizó la información correctamente" });
        }
        
    }
}
