using Dapper;
using JN_API.Models;
using JN_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace JN_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController(IConfiguration _configuration, IPasswordService _password) : ControllerBase
    {
        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginRequestModel model)
        {
            using var context = new SqlConnection(_configuration.GetValue<string>("Variables:ConnectionString"));

            var parameters = new DynamicParameters();
            parameters.Add("@CorreoElectronico", model.CorreoElectronico);
            parameters.Add("@Contrasenna", model.Contrasenna);

            var response = context.QueryFirstOrDefault<UsuarioResponse>("sp_IniciarSesion", parameters);

            if(response == null)
                return NotFound(new UsuarioResponse { Mensaje = "No se validó la información correctamente" });

            return Ok(response);
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegisterRequestModel model)
        {
            using var context = new SqlConnection(_configuration.GetValue<string>("Variables:ConnectionString"));

            var parameters = new DynamicParameters();
            parameters.Add("@Identificacion", model.Identificacion);
            parameters.Add("@NombreCompleto", model.NombreCompleto);
            parameters.Add("@CorreoElectronico", model.CorreoElectronico);
            parameters.Add("@Contrasenna", model.Contrasenna);         

            var response = context.Execute("sp_RegistrarUsuario", parameters);

            if(response <= 0)
                return UnprocessableEntity(new UsuarioResponse { Mensaje = "No se registró la información correctamente" });

            return Ok(new UsuarioResponse { Mensaje = "Se registró la información correctamente" });
        }

        [HttpPost]
        [Route("Forgot")]
        public IActionResult Forgot(ForgotRequestModel model)
        {
            using var context = new SqlConnection(_configuration.GetValue<string>("Variables:ConnectionString"));

            //Validar que el usuario exista
            var parameters = new DynamicParameters();
            parameters.Add("@CorreoElectronico", model.CorreoElectronico);

            var response = context.QueryFirstOrDefault<UsuarioResponse>("sp_ConsultarCorreo", parameters);

            if (response == null)
                return NotFound(new UsuarioResponse { Mensaje = "No se validó la información correctamente" });

            //Mecanismo de recuperación
            var usuarioId = _password.Cifrar(response.IdUsuario.ToString(), TimeSpan.FromHours(1));
            var linkCorreo = _configuration.GetValue<string>("Variables:LinkForgot") + usuarioId;

            //Enviar correo electrónico

            return Ok(response);
        }

        [HttpPut]
        [Route("ChangePassword")]
        public IActionResult ChangePassword(ChangePasswordRequestModel model)
        {
            string usuarioId = _password.Descifrar(model.UsuarioId);

            if (string.IsNullOrEmpty(usuarioId))
                return UnprocessableEntity(new UsuarioResponse { Mensaje = "El enlace no es válido o ya expiró" });

            using var context = new SqlConnection(_configuration.GetValue<string>("Variables:ConnectionString"));

            var parameters = new DynamicParameters();
            parameters.Add("@UsuarioId", usuarioId);
            parameters.Add("@Contrasenna", model.ContrasennaNueva);
            var response = context.Execute("sp_ActualizarContrasenna", parameters);

            if (response <= 0)
                return UnprocessableEntity(new UsuarioResponse { Mensaje = "No se actualizó la información correctamente" });

            return Ok(new UsuarioResponse { Mensaje = "Se actualizó la información correctamente" });
        }

    }
}
