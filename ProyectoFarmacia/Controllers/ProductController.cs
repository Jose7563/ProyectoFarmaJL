using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoFarmacia.Models;
using ProyectoFarmacia.DAO;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace ProyectoFarmacia.Controllers
{

    public class ProductController : Controller
    { 
        CategoryDAO _ca = new CategoryDAO();
        ProductDAO _pro = new ProductDAO();

        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async  Task<IActionResult> Create()
        {
            ViewBag.categories = new SelectList(await Task.Run(() => _ca.listCategories()), "IdCategory", "NameCategory");
            return View();
        }

        [HttpPost]
        public  IActionResult Create(ProductModel model)
        {

            if (model.ImageFile == null || model.ImageFile.Length < 1)
            {
                ViewBag.ErrorImg = "Se debe seleccionar una imagen";
                return RedirectToAction("Create", "Product");
            }
                // Guardar la imagen en el servidor
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "img");

                // Asegurarse de que el directorio exista, si no, créalo
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Crear un nombre de archivo único usando Guid
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImageFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ImageFile.CopyTo(fileStream);
                }
            
                string messageGeneric = "";
            messageGeneric = _pro.insertProduct(model, uniqueFileName);
            ViewBag.messageGeneric = messageGeneric;
            return View(model);
        }


        public IActionResult Resumen()
        {
            List<CategoryModel> lista = (List<CategoryModel>)_ca.listCategories();

            return StatusCode(StatusCodes.Status200OK,lista);
        }
        public IActionResult Index()
        {
            List<ProductModel> list = (List<ProductModel>)_pro.listProducts();

            return View(list);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var ocontacto = _pro.SearchProducts(id);
            return View(ocontacto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductModel model)
        {
           // if (!ModelState.IsValid)
             //   return View();
            bool rpta;
            if (model.ImageFile == null)
            {
                rpta = _pro.editPorductoNoImg(model);
              
            }
            else
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "img");

                // Asegurarse de que el directorio exista, si no, créalo
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Crear un nombre de archivo único usando Guid
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImageFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ImageFile.CopyTo(fileStream);
                }

                model.ImageProduct = uniqueFileName;

                rpta = _pro.editPorductoImg(model);
            }
          
            if (rpta)
                return RedirectToAction("Index");
            else return View(model);
        }


    }
}
