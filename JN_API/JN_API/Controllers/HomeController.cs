using JN_API.Models;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace JN_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginRequestModel model)
        {
            return Ok(model);
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegisterRequestModel model)
        {
            return Ok(model);
        }

        [HttpPost]
        [Route("Forgot")]
        public IActionResult Forgot(ForgotRequestModel model)
        {
            return Ok(model);
        }
    }
}
