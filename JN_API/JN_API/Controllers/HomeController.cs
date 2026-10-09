using Dapper;
using JN_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace JN_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController(IConfiguration _configuration) : ControllerBase
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

            var parameters = new DynamicParameters();
            parameters.Add("@CorreoElectronico", model.CorreoElectronico);

            var response = context.QueryFirstOrDefault<UsuarioResponse>("sp_ConsultarCorreo", parameters);

            return Ok(response);
        }
    }
}
