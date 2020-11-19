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
    public class CategoriaController : Controller
    {
        public Models.citas_medicasEntities1 db = new Models.citas_medicasEntities1();
        // GET: Categoria
        public ActionResult Index()
        {
            return View();
        }

        public string listar()
        {
            var query = (from p in db.Categoria select p).OrderBy(p => p.Id).ToList<Categoria>();
            List<cCategorias> listaCategorias = new List<cCategorias>();
            foreach (Categoria categoria in query)
            {
                cCategorias ObjCategoria = new cCategorias();
                ObjCategoria.Id = categoria.Id;
                ObjCategoria.Categoria1 = categoria.Categoria1;
                ObjCategoria.NumProductos = categoria.NumProductos;
                ObjCategoria.Estado = categoria.Estado;
                ObjCategoria.Agregado = categoria.Agregado;

                listaCategorias.Add(ObjCategoria);
            }
            return JsonConvert.SerializeObject(new
            {
                status = true,
                mensaje = "Datos cargados",
                data = listaCategorias
            });
        }

        public JsonResult guardar(cCategorias ccategorias)
        {
            Categoria objCategoria = new Categoria();
            if (ccategorias.Id != 0)
            {
                //editar
                objCategoria = db.Categoria.Where(a => a.Id == ccategorias.Id).FirstOrDefault();
                if (objCategoria == null)
                {
                    return Json(new { status = false, mensaje = "No existe el registro" });
                }
                else
                {
                    objCategoria.Categoria1 = ccategorias.Categoria1;
                    //objCategoria.NumProductos = ccategorias.NumProductos;
                    objCategoria.Estado = ccategorias.Estado;
                    objCategoria.Agregado = DateTime.Now;

                    db.Categoria.Attach(objCategoria);
                    db.Entry(objCategoria).State = System.Data.Entity.EntityState.Modified;
                }
            }
            else
            {
                //nuevo
                objCategoria.Categoria1 = ccategorias.Categoria1;
                objCategoria.NumProductos = 0;
                objCategoria.Estado = ccategorias.Estado;
                objCategoria.Agregado = DateTime.Now;
                db.Categoria.Add(objCategoria);
            }
            db.SaveChanges();
            return Json(new { status = true, mensaje = "Datos guardados", datos = objCategoria });
        }

        public JsonResult eliminar(int Id)
        {
            Categoria objCategoria = new Categoria();
            if (Id == 0)
            {
                return Json(new { status = false, mensaje = "El id esta en 0" });
            }

            objCategoria = db.Categoria.Where(a => a.Id == Id).FirstOrDefault();
            if (objCategoria == null)
            {
                return Json(new { status = false, mensaje = "No existe el registro" });
            }
            else
            {
                db.Categoria.Attach(objCategoria);
                db.Categoria.Remove(objCategoria);
                db.SaveChanges();

                return Json(new { status = true, mensaje = "Registro eliminado" });
            }
        }

        public JsonResult editar(int Id)
        {
            Categoria objCategoria = new Categoria();
            if (Id == 0)
            {
                return Json(new { status = false, mensaje = "El id esta en 0" });
            }

            objCategoria = db.Categoria.Where(a => a.Id == Id).FirstOrDefault();
            if (objCategoria == null)
            {
                return Json(new { status = false, mensaje = "No existe el registro" });
            }

            return Json(new { status = true, mensaje = "Datos cargados", datos = objCategoria });
        }
    }  
}