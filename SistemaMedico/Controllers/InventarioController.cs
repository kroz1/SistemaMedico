using SistemaMedico.Models;
using SistemaMedico.cModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Data;

namespace SistemaMedico.Controllers
{
    public class InventarioController : Controller
    {
        public Models.citas_medicasEntities1 db = new Models.citas_medicasEntities1();
        // GET: Inventario
        public ActionResult Index()
        {
            /*if (Session["perfil"] == null)
            {
                return View("Login");
            }*/
            //consulta para traer el dato stock de la db
            //var MonitorSV = from p in db.Productos where p.Id == 3 select p.Stock;
            //var MonitorSV = db.Productos.Where(p => p.Id == 3).Select(p => new { Stock = p.Stock });

            string conectionERP = db.Database.Connection.ConnectionString;
            cConexion conexion = new cConexion(conectionERP);

            string SQL = @"select Stock from Productos where Id = 3";
            DataTable odataTable = conexion.EjecutarConsulta(SQL);

            if(odataTable != null)
            {
                if (odataTable.Rows.Count == 0)
                {
                    //return false;
                }
            }
            else
            {

            }

            var MonitorSV = db.Productos.Select(a => a.Stock);
            //ViewBag.MonitorSV = MonitorSV;
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