using JN_WEB.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace JN_WEB.Controllers
{
    public class HomeController(HttpClient _httpClient) : Controller
    {
        #region Login

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Login(UsuarioModel model)
        {
            using var client = _httpClient;
            var url = "https://localhost:7145/api/Home/Login";

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
