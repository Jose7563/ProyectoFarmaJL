using Microsoft.AspNetCore.Mvc;
using ProyectoFarmacia.Models;
using System.Data.SqlClient;

using ProyectoFarmacia.Models;
using Newtonsoft.Json;
using System.IO;
using System.Data;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using ProyectoFarmacia.DTO;
using ProyectoFarmacia.DAO;

namespace ProyectoFarmacia.Controllers
{
    public class EcommerceController : Controller
    {
        decimal totalItemDetalle = 0;
        decimal subtotal = 0;
        decimal igv = 0;
        decimal total = 0;
        public readonly IConfiguration _config;
        EcommerceDAO _ec = new EcommerceDAO();
        UserDAO usd = new UserDAO();
        OrderDAO od = new OrderDAO();
        public EcommerceController(IConfiguration config)
        {
            _config = config;
        }
        public async Task<IActionResult> Portal(string name)
        {
            IEnumerable<ProductModel> listFilter = _ec.listProducts(name);
            // se valida si exite sesion previa  , donde se aagrego productos
            if (HttpContext.Session.GetString("basket") == null)
                HttpContext.Session.SetString("basket", JsonConvert.SerializeObject(new List<ItemModel>()));
            // si la sesion expiro o es la primera vez se cre ala sesion basket 
            return View(await Task.Run(() => listFilter));
        }

        public async Task<IActionResult> Add(int id = 0)
        {
            return View(await Task.Run(() => _ec.findProductId(id)));
        }
        [HttpPost]
        public IActionResult Add(int id, int units)
        {
            ProductModel p = _ec.findProductId(id);
            if (units > p.UnitsStock)
            {
                ViewBag.message = string.Format("El producto solo dispone de {0} unidades ", p.UnitsStock);
                return View(p);
            }

            List<ItemModel> cart = JsonConvert.DeserializeObject<List<ItemModel>>(HttpContext.Session.GetString("basket"));
            bool existe = cart.Exists(x => x.IdProduct == id);

            if (existe)
            {
                foreach (var item in cart)
                {
                    if (item.IdProduct == id)
                    {
                        item.Units = units;
                    }
                }
                HttpContext.Session.SetString("basket", JsonConvert.SerializeObject(cart));
                return RedirectToAction("Basket");
            }
            else
            {
                ItemModel item = new ItemModel();
                item.IdProduct = p.IdProduct;
                item.NameProduct = p.NameProduct;
                item.IdCategory = p.IdCategory;
                item.PriceUnit = p.PriceUnit;
                item.route = p.ImageProduct;
                item.Units = units;
                cart.Add(item);
                HttpContext.Session.SetString("basket", JsonConvert.SerializeObject(cart));
                return RedirectToAction("Basket");
            }
        }
        public IActionResult Basket()
        {//si no hay productos rederijo a vista portal
            if (HttpContext.Session.GetString("basket") == null)
                return RedirectToAction("Portal");
            //obtener productos en la lista canasta (cart)
            List<ItemModel> cart = JsonConvert.DeserializeObject<List<ItemModel>>(HttpContext.Session.GetString("basket"));
            if (cart != null)
            {

                foreach (var item in cart)
                {
                    total = total + item.Amount;
                }
            }
            ViewBag.total = total;

            return View(cart);
        }

        public IActionResult Delete(int id)
        {
            List<ItemModel> cart = JsonConvert.DeserializeObject<List<ItemModel>>(HttpContext.Session.GetString("basket"));
            ItemModel it = cart.Where(item => item.IdProduct == id).FirstOrDefault();
            cart.Remove(it);
            HttpContext.Session.SetString("basket", JsonConvert.SerializeObject(cart));
            return RedirectToAction("Basket");
        }
        decimal summaryTotal = 0;
        public IActionResult Buy()
        {
            if (HttpContext.Session.GetString("_User") == null && HttpContext.Session.GetString("_User")!= "")
            {
                return RedirectToAction("Index","Login"); 
            }

            List<ItemModel> cart = JsonConvert.DeserializeObject<List<ItemModel>>(HttpContext.Session.GetString("basket"));

            TicketModel ticket = new TicketModel();
            ticket.DateCreation = DateTime.Now;
            ticket.IdUser = usd.UserSession(HttpContext.Session.GetString("_User")).Id; 

            foreach (var t in cart)
            {
                summaryTotal = summaryTotal + t.Amount;
            }
            ticket.Total = summaryTotal;
            /***insertar la orden en la base de datos*/
            _ec.InsertTicket(ticket); 
            /*** insercion orden  detalle**/
            foreach (ItemModel it in cart)
            {
                _ec.InsertDetail( it, od.MaxIdTicket());
            }
            // descuento de productos
            foreach (var p in cart)
            {
                _ec.UpdateProductoAfterOrder(p);
            }

            ticket = new TicketModel();
            HttpContext.Session.SetString("basket", JsonConvert.SerializeObject(new List<ItemModel>()));
            return RedirectToAction("Portal");    
        }
        public IActionResult Order()
        {
            int idUser = usd.UserSession(HttpContext.Session.GetString("_User")).Id;
            List<TicketModel> list= od.orders(idUser);
            return View(list);
        }
        public IActionResult DetailOrder(int id)
        {
            UserModel us = usd.UserSession(HttpContext.Session.GetString("_User"));
            List<DetailTicketDTO> list = od.details(id);
            foreach (var item in list)
            {
                totalItemDetalle = totalItemDetalle + item.TotalItem;
            }
            igv = totalItemDetalle * (0.18m);
            subtotal = totalItemDetalle - igv;

            ViewBag.igv = Math.Round(igv, 2).ToString("#.#0");
            ViewBag.subtotal = Math.Round(subtotal, 2).ToString("#.#0"); ;
            ViewBag.total = Math.Round(totalItemDetalle, 2).ToString("#.#0"); ;

            ViewBag.names = us.Names;
            ViewBag.lastNames = us.LastNameUser;
            ViewBag.phone = us.Phone;
            return View(list);
        }
    }
}
