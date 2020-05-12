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
        public Models.citas_medicasEntities db = new Models.citas_medicasEntities();
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
    }
}