using Microsoft.AspNetCore.Mvc;
using ProyectoFarmacia.Models;
using System.Data.SqlClient;
using System.Data;

using ProyectoFarmacia.DAO; 
namespace ProyectoFarmacia.Controllers
{
    public class CategoryController : Controller
    {
        CategoryDAO _ca = new CategoryDAO();
        
        public async Task<IActionResult> Index()
        {
            return View(await Task.Run(() => _ca.listCategories()));
        }

        public async Task<IActionResult> Create()
        {
            return View(new CategoryModel());
        }
        [HttpPost]
        public IActionResult Create(CategoryModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            string messageGeneric = "";
            messageGeneric = _ca.insertCategory(model); 
            ViewBag.messageGeneric = messageGeneric;
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var ocontacto = _ca.findCategoryId(id);
            return View(ocontacto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryModel model)
        {
            if (!ModelState.IsValid)
                return View();
            bool rpta;
            rpta= _ca.editCategory(model);
            if (rpta)
                return RedirectToAction("Index");
            else return View(model);
        }

       
    }
}
