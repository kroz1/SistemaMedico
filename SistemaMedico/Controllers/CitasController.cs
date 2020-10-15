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
    public class CitasController : Controller
    {
        public Models.citas_medicasEntities1 db = new Models.citas_medicasEntities1();
        // GET: Citas
        public ActionResult Index()
        {
            List<cPacientes> listaPacientes = null;
            /*mediante entity framework, realizamos la consulta a la tabla que queramos mostrar 
            los datos de la tabla*/
            listaPacientes = (from a in db.Pacientes
                              select new cPacientes
                              {
                                  Id = a.Id,
                                  Nombre = a.Nombre,
                                  Apellido = a.Apellido
                              }).ToList();

            /*
             SelectListItem nos sirve para mostrar que datos mostraran en nuestro dropdownlist de 
             nuestra vista
             */
            List<SelectListItem> itemsPaciente = listaPacientes.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.Nombre.ToString() + " " + a.Apellido.ToString(),
                    Value = a.Id.ToString(),
                    Selected = false
                };
            });

            ViewBag.itemsPaciente = itemsPaciente; //Viewbag se utiliza para enviar datos a la vista

            //=================== MEDICO =========================
            List<cMedicos> listaMedico = null;
            /*mediante entity framework, realizamos la consulta a la tabla que queramos mostrar 
            los datos de la tabla*/
            listaMedico = (from a in db.Medicos
                           select new cMedicos
                           {
                               Id = a.Id,
                               Nombre = a.Nombre,
                               Apellido = a.Apellido
                           }).ToList();

            /*
             SelectListItem nos sirve para mostrar que datos mostraran en nuestro dropdownlist de 
             nuestra vista
             */
            List<SelectListItem> itemsMedico = listaMedico.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.Nombre.ToString() + " " + a.Apellido.ToString(),
                    Value = a.Id.ToString(),
                    Selected = false
                };
            });

            ViewBag.itemsMedico = itemsMedico; //Viewbag se utiliza para enviar datos a la vista

            //=================== CONSULTORIO =========================
            List<cConsultorios> listaConsultorio = null;
            /*mediante entity framework, realizamos la consulta a la tabla que queramos mostrar 
            los datos de la tabla*/
            listaConsultorio = (from a in db.Consultorios
                                select new cConsultorios
                                {
                                    Id = a.Id,
                                    Nombre = a.Nombre
                                }).ToList();

            /*
             SelectListItem nos sirve para mostrar que datos mostraran en nuestro dropdownlist de 
             nuestra vista
             */
            List<SelectListItem> itemsConsultorio = listaConsultorio.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.Nombre.ToString(),
                    Value = a.Id.ToString(),
                    Selected = false
                };
            });

            ViewBag.itemsConsultorio = itemsConsultorio; //Viewbag se utiliza para enviar datos a la vista
            return View();
        }

        public string listar()
        {
            var query = (from a in db.Citas select a).OrderBy(a => a.Id).ToList<Citas>();
            List<cCitas> listaCitas = new List<cCitas>();
            foreach(Citas citas in query)
            {
                cCitas objCitas = new cCitas();
                objCitas.Id = citas.Id;
                objCitas.Fecha = citas.Fecha;
                objCitas.Hora = citas.Hora;
                objCitas.Id_paciente = citas.Id_paciente;
                objCitas.Id_medico = citas.Id_medico;
                objCitas.Id_consultorio = citas.Id_consultorio;
                objCitas.Estado = citas.Estado;
                objCitas.Observacion = citas.Observacion;

                listaCitas.Add(objCitas);
            }

            return JsonConvert.SerializeObject(new
            {
                status = true,
                mensaje = "Datos cargados",
                data = listaCitas
            });
        }

        public JsonResult guardar(cCitas citas)
        {
            Citas objCitas = new Citas();           

            if (citas.Id != 0)
            {
                //editar
                objCitas = db.Citas.Where(a => a.Id == citas.Id).FirstOrDefault();
                if(objCitas == null)
                {
                    return Json(new { status = false, mensaje = "No existe el registro" });
                }
                else
                {
                    objCitas.Fecha = citas.Fecha;
                    objCitas.Hora = citas.Hora;
                    objCitas.Id_paciente = citas.Id_paciente;
                    objCitas.Id_medico = citas.Id_medico;
                    objCitas.Id_consultorio = citas.Id_consultorio;
                    objCitas.Estado = citas.Estado;
                    objCitas.Observacion = citas.Observacion;

                    db.Citas.Attach(objCitas);
                    db.Entry(objCitas).State = System.Data.Entity.EntityState.Modified;
                }
            }
            else
            {
                //nuevo
                objCitas.Fecha = citas.Fecha;
                objCitas.Hora = citas.Hora;
                objCitas.Id_paciente = citas.Id_paciente;
                objCitas.Id_medico = citas.Id_medico;
                objCitas.Id_consultorio = citas.Id_consultorio;
                objCitas.Estado = citas.Estado;
                objCitas.Observacion = citas.Observacion;

                db.Citas.Add(objCitas);
            }
            db.SaveChanges();
            return Json(new { status = true, mensaje = "Datos guardados", datos = objCitas });
        }

        public JsonResult eliminar(int Id)
        {
            Citas objCitas = new Citas();
            if(Id == 0)
            {
                return Json(new { status = false, mensaje = "El id esta en 0" });
            }
            objCitas = db.Citas.Where(a => a.Id == Id).FirstOrDefault();
            if(objCitas == null)
            {
                return Json(new { status = false, mensaje = "No existe el registro" });
            }
            db.Citas.Attach(objCitas);
            db.Citas.Remove(objCitas);
            db.SaveChanges();

            return Json(new { status = true, mensaje = "Registro eliminado" });
        }

        public JsonResult editar(int Id)
        {
            try
            {
                Citas objCitas = new Citas();
                if (Id == 0)
                {
                    return Json(new { status = false, mensaje = "El id esta en 0" });
                }
                objCitas = db.Citas.Where(a => a.Id == Id).FirstOrDefault();
                if (objCitas == null)
                {
                    return Json(new { status = false, mensaje = "No existe el registro" });
                }
                return Json(new { status = true, mensaje = "Registro cargado", datos = objCitas });
            }
            catch (Exception error)
            {
                string mensaje = error.Message.ToString();
                if (error.InnerException != null)
                {
                    mensaje += Environment.NewLine + error.InnerException.ToString();
                }
                return Json(new { status = false, mensaje = mensaje });
                //return JsonConvert.SerializeObject(new
                //{
                //    status = false,
                //    mensaje = mensaje
                //});
            }
        }
    }
}