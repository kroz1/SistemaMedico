using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaMedico.cModels;
using SistemaMedico.Models;
using Newtonsoft.Json;

namespace SistemaMedico.Controllers
{
    public class ConsultoriosController : Controller
    {
        public Models.citas_medicasEntities db = new Models.citas_medicasEntities();
        // GET: Consultorios
        public ActionResult Index()
        {
            return View();
        }

        public string listar()
        {
            try
            {
                var query = (from a in db.Consultorios select a).OrderBy(a => a.Id).ToList<Consultorios>();
                List<cConsultorios> listaConsultorios = new List<cConsultorios>();
                foreach(Consultorios consultorios in query)
                {
                    cConsultorios objConsultorios = new cConsultorios();
                    objConsultorios.Id = consultorios.Id;
                    objConsultorios.Nombre = consultorios.Nombre;
                    objConsultorios.Estado = consultorios.Estado;
                    objConsultorios.Agregado = consultorios.Agregado;

                    listaConsultorios.Add(objConsultorios);
                }

                return JsonConvert.SerializeObject(new
                {
                    status = true,
                    mensaje = "Datos cargados",
                    data = listaConsultorios
                });
            }
            catch (Exception error)
            {
                string mensaje = error.Message.ToString();
                if (error.InnerException != null)
                {
                    mensaje += Environment.NewLine + error.InnerException.ToString();
                }
                //return Json(new { status = false, mensaje = mensaje });
                return JsonConvert.SerializeObject(new
                {
                    status = false,
                    mensaje = mensaje
                });
            }
        }

        public JsonResult guardar(cConsultorios consultorios)
        {
            Consultorios objConsultorios = new Consultorios();
            if(consultorios.Id != 0)
            {
                objConsultorios = db.Consultorios.Where(a => a.Id == consultorios.Id).FirstOrDefault();
                if(objConsultorios == null)
                {
                    return Json(new { status = false, mensaje = "No existe el registro" });
                }
                objConsultorios.Nombre = consultorios.Nombre;
                objConsultorios.Estado = consultorios.Estado;
                objConsultorios.Agregado = DateTime.Now;

                db.Consultorios.Attach(objConsultorios);
                db.Entry(objConsultorios).State = System.Data.Entity.EntityState.Modified;
            }
            else
            {
                objConsultorios.Nombre = consultorios.Nombre;
                objConsultorios.Estado = consultorios.Estado;
                objConsultorios.Agregado = DateTime.Now;

                db.Consultorios.Add(objConsultorios);
            }
            db.SaveChanges();
            return Json(new { status = true, mensaje = "Datos guardados", datos = objConsultorios });
        }

        public JsonResult eliminar(int Id)
        {
            Consultorios objConsultorios = new Consultorios();
            if (Id == 0)
            {
                return Json(new { status = false, mensaje = "El Id viene en 0" });
            }
            objConsultorios = db.Consultorios.Where(a => a.Id == Id).FirstOrDefault();
            if (objConsultorios == null)
            {
                return Json(new { status = false, mensaje = "No existe el registro" });
            }
            else
            {
                db.Consultorios.Attach(objConsultorios);
                db.Consultorios.Remove(objConsultorios);
                db.SaveChanges();

                return Json(new { status = true, mensaje = "Consultorio eliminado" });
            }
        }

        public JsonResult editar(int Id)
        {
            Consultorios objConsultorios = new Consultorios();
            if (Id == 0)
            {
                return Json(new { status = false, mensaje = "El id esta en 0" });
            }
            else
            {
                objConsultorios = db.Consultorios.Where(a => a.Id == Id).FirstOrDefault();
                if (objConsultorios == null)
                {
                    return Json(new { status = false, mensaje = "No existe el registro" });
                }
                return Json(new { status = true, mensaje = "Datos cargados", datos = objConsultorios });
            }
        }
    }
}