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
            var (datosPersonales, mensaje) = ConsultarDatosPersonales();

            if (datosPersonales != null)
                return View(new PerfilUsuarioViewModel { DatosPersonales = datosPersonales });

            ViewBag.Mensaje = mensaje;
            return View(new PerfilUsuarioViewModel());
        }

        [HttpPost]
        public IActionResult CambiarContrasenna([Bind(Prefix = "Contrasenna")] ChangePasswordRequestModel model)
        {
            if (!ModelState.IsValid)
                return View(new PerfilUsuarioViewModel { Contrasenna = model, DatosPersonales = ConsultarDatosPersonales().Datos ?? new() });

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

            return View(new PerfilUsuarioViewModel { DatosPersonales = ConsultarDatosPersonales().Datos ?? new() });
        }

        #endregion

        #region Actualizar Datos Personales

        [HttpPost]
        public IActionResult ActualizarDatosPersonales([Bind(Prefix = "DatosPersonales")] DatosPersonalesRequestModel model)
        {
            if (!ModelState.IsValid)
                return View("CambiarContrasenna", new PerfilUsuarioViewModel { DatosPersonales = model });

            model.IdUsuario = HttpContext.Session.GetInt32("ConsecutivoUsuario").GetValueOrDefault();

            using var client = _httpClient;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
            var url = _configuration.GetValue<string>("Variables:ApiBaseUrl") + "Usuario/ActualizarDatosPersonales";

            var response = client.PutAsJsonAsync(url, model).Result;

            var datos = response.Content.ReadFromJsonAsync<UsuarioResponse>().Result;
            ViewBag.Mensaje = datos?.Mensaje;

            if (response.StatusCode == HttpStatusCode.OK)
                HttpContext.Session.SetString("Nombre", model.NombreCompleto);

            return View("CambiarContrasenna", new PerfilUsuarioViewModel { DatosPersonales = model });
        }

        #endregion

        private (DatosPersonalesRequestModel? Datos, string? Mensaje) ConsultarDatosPersonales()
        {
            var model = new ConsultarUsuarioRequestModel
            {
                IdUsuario = HttpContext.Session.GetInt32("ConsecutivoUsuario").GetValueOrDefault()
            };

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
            var url = _configuration.GetValue<string>("Variables:ApiBaseUrl") + "Usuario/ConsultarUsuario";

            var response = _httpClient.PostAsJsonAsync(url, model).Result;
            var datos = response.Content.ReadFromJsonAsync<UsuarioResponse>().Result;

            if (response.StatusCode != HttpStatusCode.OK)
                return (null, datos?.Mensaje);

            return (new DatosPersonalesRequestModel
            {
                Identificacion = datos!.Identificacion,
                NombreCompleto = datos.NombreCompleto,
                CorreoElectronico = datos.CorreoElectronico
            }, null);
        }
    }
}
