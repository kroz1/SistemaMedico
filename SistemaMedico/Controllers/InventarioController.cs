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
            //=========================== Monitor SV =======================================
            var MonitorSV = db.Productos.Where(p => p.Id == 3).Select(p => p.Stock).FirstOrDefault();
            if (MonitorSV != null)
            {
                ViewBag.MonitorSV = MonitorSV;
            }
            else
            {
                ViewBag.MonitorSV = 0;
            }
            //=========================== Electroquirúrgicas =======================================
            var Electroquirúrgicas = db.Productos.Where(p => p.Id == 4).Select(p => p.Stock).FirstOrDefault();
            if (Electroquirúrgicas != null)
            {
                ViewBag.Electroquirúrgicas = Electroquirúrgicas;
            }
            else
            {
                ViewBag.Electroquirúrgicas = 0;
            }
            //=========================== Electrocardiograma =======================================
            var Electrocardiograma = db.Productos.Where(p => p.Id == 5).Select(p => p.Stock).FirstOrDefault();
            if (Electrocardiograma != null)
            {
                ViewBag.Electrocardiograma = Electrocardiograma;
            }
            else
            {
                ViewBag.Electrocardiograma = 0;
            }
            //=========================== Respirador artificial =======================================
            var RespArtificial = db.Productos.Where(p => p.Id == 6).Select(p => p.Stock).FirstOrDefault();
            if (RespArtificial != null)
            {
                ViewBag.RespArtificial = RespArtificial;
            }
            else
            {
                ViewBag.RespArtificial = 0;
            }
            //=========================== Desfibrilador =======================================
            var Desfibrilador = db.Productos.Where(p => p.Id == 7).Select(p => p.Stock).FirstOrDefault();
            if (Desfibrilador != null)
            {
                ViewBag.Desfibrilador = Desfibrilador;
            }
            else
            {
                ViewBag.Desfibrilador = 0;
            }
            ViewBag.Desfibrilador = Desfibrilador;
            //=========================== Esterilizadores =======================================
            var Esterilizadores = db.Productos.Where(p => p.Id == 8).Select(p => p.Stock).FirstOrDefault();
            if(Esterilizadores != null)
            {
                ViewBag.Esterilizadores = Esterilizadores;
            }
            else
            {
                ViewBag.Esterilizadores = 0;
            }
            //=========================== Ultrasonido =======================================
            var Ultrasonido = db.Productos.Where(p => p.Id == 9).Select(p => p.Stock).FirstOrDefault();
            if (Ultrasonido != null)
            {
                ViewBag.Ultrasonido = Ultrasonido;
            }
            else
            {
                ViewBag.Ultrasonido = 0;
            }
            //=========================== Camas y camillas quirúrgicas =======================================
            var CamasCamillas = db.Productos.Where(p => p.Id == 10).Select(p => p.Stock).FirstOrDefault();
            if (CamasCamillas != null)
            {
                ViewBag.CamasCamillas = CamasCamillas;
            }
            else
            {
                ViewBag.CamasCamillas = 0;
            }
            return View();
        }

        public static bool ConsultarInventario()
        {
            Models.citas_medicasEntities1 db = new Models.citas_medicasEntities1();
            string conectionERP = db.Database.Connection.ConnectionString;
            cConexion conexion = new cConexion(conectionERP);

            string SQL = @"select Stock from Productos where Id = 3";
            DataTable odataTable = conexion.EjecutarConsulta(SQL);

            if (odataTable != null)
            {
                if (odataTable.Rows.Count == 0)
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
            return true;
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

        public JsonResult guardar(cProductos cproductos)
        {
            Productos objProductos = new Productos();
            if(cproductos.Id != 0)
            {
                //editar
            }
            else
            {
                //nuevo
                objProductos.Nombre = cproductos.Nombre;
                objProductos.Stock = cproductos.Stock;
                objProductos.Nombre = cproductos.Nombre;
                objProductos.Nombre = cproductos.Nombre;
                objProductos.Nombre = cproductos.Nombre;
            }
            return Json(new { status = true, mensaje = "Datos guardados", datos = objProductos });
        }
    }
}