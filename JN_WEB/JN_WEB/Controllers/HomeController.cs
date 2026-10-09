using JN_WEB.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;

namespace JN_WEB.Controllers
{
    public class HomeController(HttpClient _httpClient, IConfiguration _configuration) : Controller
    {
        #region Inicio de Sesión

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Login(LoginRequestModel model)
        {
            using var client = _httpClient;
            var url = _configuration.GetValue<string>("Variables:ApiBaseUrl") + "Home/Login";

            var response = client.PostAsJsonAsync(url, model).Result;

            return View();
        }

        #endregion

        #region Registro de usuarios

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Register(RegisterRequestModel model)
        {
            using var client = _httpClient;
            var url = _configuration.GetValue<string>("Variables:ApiBaseUrl") + "Home/Register";

            var response = client.PostAsJsonAsync(url, model).Result;

            if(response.StatusCode == HttpStatusCode.OK)
                return RedirectToAction("Login", "Home");

            var datos = response.Content.ReadFromJsonAsync<UsuarioResponse>().Result;
            ViewBag.Mensaje = datos?.Mensaje;

            return View();
        }

        #endregion

        #region Olvido de Contraseña

        [HttpGet]
        public IActionResult Forgot()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Forgot(ForgotRequestModel model)
        {
            using var client = _httpClient;
            var url = _configuration.GetValue<string>("Variables:ApiBaseUrl") + "Home/Forgot";

            var response = client.PostAsJsonAsync(url, model).Result;

            return View();
        }

        #endregion

        public IActionResult Index()
        {
            return View();
        }
    }
}
