using SistemaMedico.Models;
using SistemaMedico.cModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace SistemaMedico.Controllers
{
    public class InventarioController : Controller
    {
        public Models.citas_medicasEntities1 db = new Models.citas_medicasEntities1();
        // GET: Inventario
        public ActionResult Index()
        {
            //consulta para traer el dato stock de la db
            //var MonitorSV = from p in db.Productos where p.Id == 3 select p.Stock;
            var MonitorSV = db.Productos.Where(p => p.Id == 3).Select(p => new { Stock = p.Stock });
            //var row = db.Productos.SingleOrDefault(a => a.Id == 3);
            //var MonitorSV = row != null ? row.Stock : String.Empty;
            ViewBag.MonitorSV = MonitorSV;
            return View();
        }

        public string listarProductos()
        {
            var query = (from p in db.Productos select p).OrderBy(p => p.Id).ToList<Productos>();
            List<cProductos> listaProductos = new List<cProductos>();
            foreach(Productos productos in query)
            {
                cProductos ObjProductos = new cProductos();
                ObjProductos.Id = productos.Id;
                ObjProductos.Nombre = productos.Nombre;
                ObjProductos.Stock = productos.Stock;
                ObjProductos.PrecioCompra = productos.PrecioCompra;
                ObjProductos.PrecioVenta = productos.PrecioVenta;
                ObjProductos.Agregado = productos.Agregado;

                listaProductos.Add(ObjProductos);
            }
            return JsonConvert.SerializeObject(new
            {
                status = true,
                mensaje = "Datos cargados",
                data = listaProductos
            });
        }
    }
}