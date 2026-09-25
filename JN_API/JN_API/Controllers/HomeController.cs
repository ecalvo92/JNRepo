using JN_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace JN_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        [HttpPost]
        [Route("Login")]
        public IActionResult Login(UsuarioModel model)
        {
            return Ok(model);
        }
    }
}
