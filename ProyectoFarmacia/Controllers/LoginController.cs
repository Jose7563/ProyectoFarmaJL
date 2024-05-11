using Microsoft.AspNetCore.Mvc;
using ProyectoFarmacia.Models;
using System.Data.SqlClient;
using System.Data;
using ProyectoFarmacia.DAO;

namespace ProyectoFarmacia.Controllers
{
    public class LoginController : Controller
    {
      
        LoginDAO lo = new LoginDAO();
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("_User") != null) return RedirectToAction("Portal","Ecommerce");

            return View(new UserModel());
        }
  
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("_User");
            return RedirectToAction("Portal", "Ecommerce");
        }

		public IActionResult LogoutAdmin()
		{
			HttpContext.Session.Remove("_User");
			return RedirectToAction("Index", "Login");
		}

		[HttpPost]
        public IActionResult Index(UserModel reg)
        {
            UserModel res = null;
            if (string.IsNullOrEmpty(reg.UserSession) || string.IsNullOrEmpty(reg.PasswordUser))
            {
                ModelState.AddModelError("", "Ingresar los datos solicitados");
                return View(reg);
            }
            res= lo.LoginSingIn(reg);
            if (res.UserSession != null)
            {

                HttpContext.Session.SetString("_User", reg.UserSession);
                if (res.TypeUser == 1)
                {
                    return RedirectToAction("Grafico", "Panel");
                }
                if (res.TypeUser == 2)

                    return RedirectToAction("Portal", "Ecommerce");
            }
            else
            {
                ModelState.AddModelError("", "Datos ingresados no validos");
            }

            return View(reg);
        }

    }
}
