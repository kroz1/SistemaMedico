using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaMedico.Models;
using SistemaMedico.cModels;
using Newtonsoft.Json;

namespace SistemaMedico.Controllers
{
    public class ProveedoresController : Controller
    {
        public Models.citas_medicasEntities1 db = new Models.citas_medicasEntities1();
        // GET: Proveedores
        public ActionResult Index()
        {
            return View();
        }

        public string listar()
        {
            var query = (from p in db.Proveedor select p).OrderBy(p => p.Id).ToList<Proveedor>();
            List<cProveedor> listaProveedor = new List<cProveedor>();
            foreach (Proveedor proveedor in query)
            {
                cProveedor ObjProveedor = new cProveedor();
                ObjProveedor.Id = proveedor.Id;
                ObjProveedor.Proveedor1 = proveedor.Proveedor1;
                ObjProveedor.RegFiscal = proveedor.RegFiscal;
                ObjProveedor.Direccion = proveedor.Direccion;
                ObjProveedor.Email = proveedor.Email;
                ObjProveedor.Telefono = proveedor.Telefono;
                ObjProveedor.Agregado = proveedor.Agregado;

                listaProveedor.Add(ObjProveedor);
            }
            return JsonConvert.SerializeObject(new
            {
                status = true,
                mensaje = "Datos cargados",
                data = listaProveedor
            });
        }

        public JsonResult guardar(cProveedor cproveedor)
        {
            Proveedor objProveedor = new Proveedor();
            if (cproveedor.Id != 0)
            {
                //editar
                objProveedor = db.Proveedor.Where(a => a.Id == cproveedor.Id).FirstOrDefault();
                if (objProveedor == null)
                {
                    return Json(new { status = false, mensaje = "No existe el registro" });
                }
                else
                {
                    objProveedor.RegFiscal = cproveedor.RegFiscal;
                    objProveedor.Proveedor1 = cproveedor.Proveedor1;
                    objProveedor.Direccion = cproveedor.Direccion;
                    objProveedor.Email = cproveedor.Email;
                    objProveedor.Telefono = cproveedor.Telefono;
                    objProveedor.Agregado = DateTime.Now;

                    db.Proveedor.Attach(objProveedor);
                    db.Entry(objProveedor).State = System.Data.Entity.EntityState.Modified;
                }
            }
            else
            {
                //nuevo
                objProveedor.RegFiscal = cproveedor.RegFiscal;
                objProveedor.Proveedor1 = cproveedor.Proveedor1;
                objProveedor.Direccion = cproveedor.Direccion;
                objProveedor.Email = cproveedor.Email;
                objProveedor.Telefono = cproveedor.Telefono;
                objProveedor.Agregado = DateTime.Now;
                db.Proveedor.Add(objProveedor);
            }
            db.SaveChanges();
            return Json(new { status = true, mensaje = "Datos guardados", datos = objProveedor });
        }

        public JsonResult eliminar(int Id)
        {
            Proveedor objProveedor = new Proveedor();
            if (Id == 0)
            {
                return Json(new { status = false, mensaje = "El id esta en 0" });
            }

            objProveedor = db.Proveedor.Where(a => a.Id == Id).FirstOrDefault();
            if (objProveedor == null)
            {
                return Json(new { status = false, mensaje = "No existe el registro" });
            }
            else
            {
                db.Proveedor.Attach(objProveedor);
                db.Proveedor.Remove(objProveedor);
                db.SaveChanges();

                return Json(new { status = true, mensaje = "Registro eliminado" });
            }
        }

        public JsonResult editar(int Id)
        {
            Proveedor objProveedor = new Proveedor();
            if (Id == 0)
            {
                return Json(new { status = false, mensaje = "El id esta en 0" });
            }

            objProveedor = db.Proveedor.Where(a => a.Id == Id).FirstOrDefault();
            if (objProveedor == null)
            {
                return Json(new { status = false, mensaje = "No existe el registro" });
            }

            return Json(new { status = true, mensaje = "Datos cargados", datos = objProveedor });
        }
    }
}