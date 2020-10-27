using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaMedico.cModels;
using SistemaMedico.Models;
using Newtonsoft.Json;

using System.Data;
using Microsoft.Reporting.WebForms;

namespace SistemaMedico.Controllers
{
    public class ReportePacientesController : Controller
    {
        public Models.citas_medicasEntities1 db = new Models.citas_medicasEntities1();
        // GET: ReportePacientes
        public ActionResult Index()
        {
            return View();
        }

        public string listar()
        {
            var query = (from a in db.Citas select a).OrderBy(a => a.Id).ToList<Citas>();
            List<cCitas> listaCitasPacientes = new List<cCitas>();
            foreach (Citas citas in query)
            {
                cCitas objCitas = new cCitas();
                objCitas.Id = citas.Id;
                objCitas.Fecha = citas.Fecha;
                objCitas.Hora = citas.Hora;
                objCitas.Id_paciente = citas.Id_paciente;
                objCitas.Id_medico = citas.Id_medico;
                objCitas.Id_consultorio = citas.Id_consultorio;
                objCitas.Estado = citas.Estado;

                listaCitasPacientes.Add(objCitas);
            }

            return JsonConvert.SerializeObject(new
            {
                status = true,
                mensaje = "Datos cargados",
                data = listaCitasPacientes
            });
        }

        public void ImprimirReporte()
        {
            string pass = "";
        }
    }
}