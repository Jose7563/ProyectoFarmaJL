using Microsoft.AspNetCore.Mvc;

namespace ProyectoFarmacia.Controllers
{
    public class CloseSessionController : Controller
    {



        public IActionResult ClearSession()
        {
            // Acceder a la sesión
            HttpContext.Session.SetString("_User", "");

            // Cerrar la sesión
         

            // También puedes invalidar la cookie de autenticación si estás utilizando la autenticación de ASP.NET Core Identity
            // Ejemplo:
            // await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Portal", "Ecommerce"); // Redirigir a la página de inicio u otra página después de cerrar sesión
        }
    }
}
