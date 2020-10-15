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
    public class MedicosController : Controller
    {
        public Models.citas_medicasEntities1 db = new Models.citas_medicasEntities1();
        // GET: Medicos
        public ActionResult Index()
        {
            List<cEspecialidades> listaEspecialidades = null;
            /*mediante entity framework, realizamos la consulta a la tabla que queramos mostrar 
            los datos de la tabla*/
            listaEspecialidades = (from a in db.Especialidades
                                   select new cEspecialidades
                                   {
                                       Id = a.Id,
                                       Nombre = a.Nombre
                                   }).ToList();

            /*
             SelectListItem nos sirve para mostrar que datos mostraran en nuestro dropdownlist de 
             nuestra vista
             */
            List<SelectListItem> items = listaEspecialidades.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.Nombre.ToString(),
                    Value = a.Id.ToString(),
                    Selected = false
                };
            });

            ViewBag.items = items; //Viewbag se utiliza para enviar datos a la vista

            return View();
        }

        public string listar()
        {
            try
            {
                var query = (from m in db.Medicos select m).OrderBy(m => m.Id).ToList<Medicos>();
                List<cMedicos> listaMedicos = new List<cMedicos>();

                foreach(Medicos medicos in query)
                {
                    cMedicos o = new cMedicos();
                    o.Id = medicos.Id;
                    o.Nombre = medicos.Nombre;
                    o.Apellido = medicos.Apellido;
                    o.Telefono = medicos.Telefono;
                    o.Direccion = medicos.Direccion;
                    o.correo = medicos.correo;
                    o.Id_especialidad = medicos.Id_especialidad;

                    listaMedicos.Add(o);
                }

                return JsonConvert.SerializeObject(new
                {
                    status = true,
                    mensaje = "Datos cargados",
                    data = listaMedicos
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

        public JsonResult guardar(cMedicos medicos)
        {
            Medicos objMedicos = new Medicos();
            if(medicos.Id != 0)
            {
                //Editar
                objMedicos = db.Medicos.Where(a => a.Id == medicos.Id).FirstOrDefault();
                if(objMedicos == null)
                {
                    return Json(new { status = false, mensaje = "No existe el registro" });
                }

                objMedicos.Nombre = medicos.Nombre;
                objMedicos.Apellido = medicos.Apellido;
                objMedicos.Telefono = medicos.Telefono;
                objMedicos.Direccion = medicos.Direccion;
                objMedicos.correo = medicos.correo;
                objMedicos.Id_especialidad = medicos.Id_especialidad;

                db.Medicos.Attach(objMedicos);
                db.Entry(objMedicos).State = System.Data.Entity.EntityState.Modified;
            }
            else
            {
                //guardar
                objMedicos.Nombre = medicos.Nombre;
                objMedicos.Apellido = medicos.Apellido;
                objMedicos.Telefono = medicos.Telefono;
                objMedicos.Direccion = medicos.Direccion;
                objMedicos.correo = medicos.correo;
                objMedicos.Id_especialidad = medicos.Id_especialidad;

                db.Medicos.Add(objMedicos);
            }
            db.SaveChanges();
            return Json(new { status = true, mensaje = "Datos guardados", datos = objMedicos});
        }

        public JsonResult eliminar(int Id)
        {
            Medicos medicos = new Medicos();
            if(Id == 0)
            {
                return Json(new { status = false, mensaje = "El id esta en 0" });
            }

            medicos = db.Medicos.Where(a => a.Id == Id).FirstOrDefault();
            if(medicos == null)
            {
                return Json(new { status = false, mensaje = "No existe el registro" });
            }
            else
            {
                db.Medicos.Attach(medicos);
                db.Medicos.Remove(medicos);
                db.SaveChanges();

                return Json(new { status = true, mensaje = "Registro eliminado" });
            }
        }

        public JsonResult editar(int Id)
        {
            Medicos medicos = new Medicos();
            if(Id == 0)
            {
                return Json(new { status = false, mensaje = "El id esta en 0" });
            }

            medicos = db.Medicos.Where(a => a.Id == Id).FirstOrDefault();
            if(medicos == null)
            {
                return Json(new { status = false, mensaje = "No existe el registro" });
            }

            return Json(new { status = true, mensaje = "Datos cargados", datos = medicos });
        }
    }
}