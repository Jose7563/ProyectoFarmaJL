using Microsoft.AspNetCore.Mvc;
using ProyectoFarmacia.DAO;
using ProyectoFarmacia.DTO;
using ProyectoFarmacia.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace ProyectoFarmacia.Controllers
{
    public class PanelController : Controller
    {

        PanelDAO _panel= new PanelDAO();
        public IActionResult Grafico()
        {
			ViewBag.Sale = _panel.SalesForYear(DateTime.Now.Year);
            ViewBag.SaleM = _panel.SaleforMonth(DateTime.Now.Month);
            List<ProductModel> products = _panel.ListStock(15);
			return View(products); 
        }
		public IActionResult Tes()
        {  
            List<Generic> list = _panel.ListSalesMonthInYear(DateTime.Now.Year); 
            return StatusCode(StatusCodes.Status200OK, list); 
        }
        public IActionResult Donut()
        {
            List<Generic> ListDonut = _panel.Donuts(DateTime.Now.Year);
			return StatusCode(StatusCodes.Status200OK, ListDonut);
		}

    }
}
