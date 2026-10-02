using JN_WEB.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace JN_WEB.Controllers
{
    public class HomeController(HttpClient _httpClient) : Controller
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
            var url = "https://localhost:7145/api/Home/Login";

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
            var url = "https://localhost:7145/api/Home/Register";

            var response = client.PostAsJsonAsync(url, model).Result;

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
            var url = "https://localhost:7145/api/Home/Forgot";

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
