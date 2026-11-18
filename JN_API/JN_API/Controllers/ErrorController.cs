using Dapper;
using JN_API.Services;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace JN_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorController(IConfiguration _configuration, IPasswordService _password) : ControllerBase
    {
        [Route("CapturarError")]
        public IActionResult CapturarError()
        {
            var ex = HttpContext.Features.Get<IExceptionHandlerFeature>();

            using var context = new SqlConnection(_configuration.GetValue<string>("Variables:ConnectionString"));

            var parameters = new DynamicParameters();
            parameters.Add("@IdUsuario", _password.ObtenerConsecutivoToken());
            parameters.Add("@Error", ex!.Error);
            parameters.Add("@Origen", ex!.Path);

            context.Execute("sp_RegistrarError", parameters);

            return StatusCode(StatusCodes.Status500InternalServerError, "Se presentó un inconveniente técnico");
        }
    }
}
