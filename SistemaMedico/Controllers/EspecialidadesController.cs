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
    public class EspecialidadesController : Controller
    {
        public Models.citas_medicasEntities db = new Models.citas_medicasEntities();
        // GET: Especialidades
        public ActionResult Index()
        {
            return View();
        }

        public string listar()
        {
            try
            {
                var query = (from e in db.Especialidades select e).OrderBy(e => e.Id).ToList<Especialidades>();
                List<cEspecialidades> listaEspecialidades = new List<cEspecialidades>();

                foreach(Especialidades especialidades in query)
                {
                    cEspecialidades objetoEspecialidad = new cEspecialidades();
                    objetoEspecialidad.Id = especialidades.Id;
                    objetoEspecialidad.Nombre = especialidades.Nombre;
                    objetoEspecialidad.Estado = especialidades.Estado;
                    objetoEspecialidad.Agregado = especialidades.Agregado;

                    listaEspecialidades.Add(objetoEspecialidad);
                }

                return JsonConvert.SerializeObject(new
                {
                    status = true,
                    mensaje = "Datos cargados",
                    data = listaEspecialidades
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

        public JsonResult guardar(cEspecialidades cEspecialidades)
        {
            Especialidades especialidades = new Especialidades();
            if (cEspecialidades.Id != 0)
            {
                especialidades = db.Especialidades.Where(a => a.Id == cEspecialidades.Id).FirstOrDefault();
                if (especialidades == null)
                {
                    return Json(new { status = false, mensaje = "No existe el registro" });
                }
                especialidades.Nombre = cEspecialidades.Nombre;
                especialidades.Estado = cEspecialidades.Estado;
                especialidades.Agregado = DateTime.Now;

                db.Especialidades.Attach(especialidades);
                db.Entry(especialidades).State = System.Data.Entity.EntityState.Modified;                
            }
            else
            {
                especialidades.Nombre = cEspecialidades.Nombre;
                especialidades.Estado = cEspecialidades.Estado;
                especialidades.Agregado = DateTime.Now;

                db.Especialidades.Add(especialidades);               
            }
            db.SaveChanges();
            return Json(new { status = true, mensaje = "Datos guardados", datos = especialidades });
        }

        public JsonResult eliminar(int Id)
        {
            Especialidades especialidades = new Especialidades();
            if(Id == 0)
            {
                return Json(new { status = false, mensaje = "El Id viene en 0"});
            }
            especialidades = db.Especialidades.Where(a => a.Id == Id).FirstOrDefault();
            if(especialidades == null)
            {
                return Json(new { status = false, mensaje = "No existe el registro" });
            }
            else
            {
                db.Especialidades.Attach(especialidades);
                db.Especialidades.Remove(especialidades);
                db.SaveChanges();

                return Json(new { status = true, mensaje = "Especialidad eliminada" });
            }
        }

        public JsonResult editar(int Id)
        {
            Especialidades especialidades = new Especialidades();
            if(Id == 0)
            {
                return Json(new { status = false, mensaje = "El id esta en 0" });
            }
            else
            {
                especialidades = db.Especialidades.Where(a => a.Id == Id).FirstOrDefault();
                if (especialidades == null)
                {
                    return Json(new { status = false, mensaje = "No existe el registro" });
                }
                return Json(new { status = true, mensaje = "Datos cargados", datos = especialidades });
            }
        }
    }
}