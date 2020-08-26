using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using SistemaMedico.cModels;
using SistemaMedico.Models;

namespace SistemaMedico.Controllers
{
    public class HomeController : Controller
    {
        public Models.citas_medicasEntities db = new Models.citas_medicasEntities();
        public ActionResult Index()
        {
            return View();
        }

        public string listarCitas()
        {
            try
            {
                //query para traer los ultimos 10 registros
                var query = (from c in db.Citas orderby c.Fecha descending select c).Take(10);
                List<cCitas> listaUltimasCitas = new List<cCitas>();

                foreach(Citas citas in query)
                {
                    cCitas o = new cCitas();
                    o.Id = citas.Id;
                    o.Id_paciente = citas.Id_paciente;
                    o.Fecha = citas.Fecha;
                    o.Hora = citas.Hora;

                    listaUltimasCitas.Add(o);
                }
                return JsonConvert.SerializeObject(new
                {
                    status = true,
                    mensaje = "Datos cargados",
                    data = listaUltimasCitas
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

        public string listarPacientes()
        {
            try
            {
                var query = (from p in db.Pacientes orderby p.Id descending select p).Take(10);
                List<cPacientes> listaUltimosPacientes = new List<cPacientes>();

                foreach(Pacientes pacientes in query)
                {
                    cPacientes o = new cPacientes();
                    o.Id = pacientes.Id;
                    o.Nombre = pacientes.Nombre;
                    o.Apellido = pacientes.Apellido;
                    o.Telefono = pacientes.Telefono;

                    listaUltimosPacientes.Add(o);
                }
                return JsonConvert.SerializeObject(new
                {
                    status = true,
                    mensaje = "Datos cargados",
                    data = listaUltimosPacientes
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
    }
}