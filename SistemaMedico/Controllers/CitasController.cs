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
        public Models.citas_medicasEntities db = new Models.citas_medicasEntities();
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
                                  Nombre = a.Nombre
                              }).ToList();

            /*
             SelectListItem nos sirve para mostrar que datos mostraran en nuestro dropdownlist de 
             nuestra vista
             */
            List<SelectListItem> itemsPaciente = listaPacientes.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.Nombre.ToString(),
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
                               Nombre = a.Nombre
                           }).ToList();

            /*
             SelectListItem nos sirve para mostrar que datos mostraran en nuestro dropdownlist de 
             nuestra vista
             */
            List<SelectListItem> itemsMedico = listaMedico.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.Nombre.ToString(),
                    Value = a.Id.ToString(),
                    Selected = false
                };
            });

            ViewBag.itemsMedico = itemsMedico; //Viewbag se utiliza para enviar datos a la vista

            //=================== MEDICO =========================
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
    }
}