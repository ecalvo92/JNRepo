using JN_WEB.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http.Headers;

namespace JN_WEB.Controllers
{
    public class UsuarioController(HttpClient _httpClient, IConfiguration _configuration) : Controller
    {
        #region Cambiar Contraseña

        [HttpGet]
        public IActionResult CambiarContrasenna()
        {
            var model = new ConsultarUsuarioRequestModel
            {
                IdUsuario = HttpContext.Session.GetInt32("ConsecutivoUsuario").GetValueOrDefault()
            };

            using var client = _httpClient;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
            var url = _configuration.GetValue<string>("Variables:ApiBaseUrl") + "Usuario/ConsultarUsuario";

            var response = client.PostAsJsonAsync(url, model).Result;
            var datos = response.Content.ReadFromJsonAsync<UsuarioResponse>().Result;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var perfil = new PerfilUsuarioViewModel();
                perfil.DatosPersonales.Identificacion = datos!.Identificacion;
                perfil.DatosPersonales.NombreCompleto = datos!.NombreCompleto;
                perfil.DatosPersonales.CorreoElectronico = datos!.CorreoElectronico;

                return View(perfil);
            }

            ViewBag.Mensaje = datos?.Mensaje;
            return View();
        }

        [HttpPost]
        public IActionResult CambiarContrasenna([Bind(Prefix = "Contrasenna")] ChangePasswordRequestModel model)
        {
            if (!ModelState.IsValid)
                return View(new PerfilUsuarioViewModel { Contrasenna = model });

            model.UsuarioId = HttpContext.Session.GetInt32("ConsecutivoUsuario").ToString()!;

            //Hacer un hash de la contraseña
            model.ContrasennaNueva = BCrypt.Net.BCrypt.HashPassword(model.ContrasennaNueva);
            model.ConfirmarContrasenna = BCrypt.Net.BCrypt.HashPassword(model.ConfirmarContrasenna);

            using var client = _httpClient;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
            var url = _configuration.GetValue<string>("Variables:ApiBaseUrl") + "Usuario/ChangePassword";

            var response = client.PutAsJsonAsync(url, model).Result;

            var datos = response.Content.ReadFromJsonAsync<UsuarioResponse>().Result;
            ViewBag.Mensaje = datos?.Mensaje;

            return View(new PerfilUsuarioViewModel());
        }

        #endregion
    }
}
