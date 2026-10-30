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
            if(!ModelState.IsValid)
                return View(model);

            using var client = _httpClient;
            var url = _configuration.GetValue<string>("Variables:ApiBaseUrl") + "Home/Login";

            var response = client.PostAsJsonAsync(url, model).Result;
            var datos = response.Content.ReadFromJsonAsync<UsuarioResponse>().Result;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                HttpContext.Session.SetInt32("Autenticado", 1);
                HttpContext.Session.SetString("Nombre", datos!.NombreCompleto);
                return RedirectToAction("Index", "Home");
            }
            
            ViewBag.Mensaje = datos?.Mensaje;

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
            if (!ModelState.IsValid)
                return View(model);

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
            if (!ModelState.IsValid)
                return View();

            using var client = _httpClient;
            var url = _configuration.GetValue<string>("Variables:ApiBaseUrl") + "Home/Forgot";

            var response = client.PostAsJsonAsync(url, model).Result;

            if (response.StatusCode == HttpStatusCode.OK)
                return RedirectToAction("Login", "Home");

            var datos = response.Content.ReadFromJsonAsync<UsuarioResponse>().Result;
            ViewBag.Mensaje = datos?.Mensaje;

            return View();
        }

        #endregion

        #region Cambiar Contraseña

        [HttpGet]
        public IActionResult ChangePassword(string id)
        {
            HttpContext.Session.SetString("UsuarioId", id);
            return View();
        }

        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordRequestModel model)
        {
            if (!ModelState.IsValid)
                return View();

            string usuarioId = HttpContext.Session.GetString("UsuarioId")!;

            if (string.IsNullOrEmpty(usuarioId))
                ViewBag.Mensaje = "El enlace no es válido o ya expiró";

            model.UsuarioId = usuarioId;

            using var client = _httpClient;
            var url = _configuration.GetValue<string>("Variables:ApiBaseUrl") + "Home/ChangePassword";

            var response = client.PutAsJsonAsync(url, model).Result;

            if (response.StatusCode == HttpStatusCode.OK)
                return RedirectToAction("Login", "Home");

            var datos = response.Content.ReadFromJsonAsync<UsuarioResponse>().Result;
            ViewBag.Mensaje = datos?.Mensaje;

            return View();
        }

        #endregion

        [HttpGet]
        public IActionResult CerrarSesion()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Home");
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
