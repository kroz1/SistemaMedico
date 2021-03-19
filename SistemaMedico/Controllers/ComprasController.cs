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
    public class ComprasController : Controller
    {
        public Models.citas_medicasEntities1 db = new Models.citas_medicasEntities1();
        // GET: Compras
        public ActionResult Index()
        {
            List<cProveedor> listaProveedores = null;

            /*mediante entity framework, realizamos la consulta a la tabla que queramos mostrar 
            los datos de la tabla*/
            listaProveedores = (from a in db.Proveedor
                                   select new cProveedor
                                   {
                                       Id = a.Id,
                                       Proveedor1 = a.Proveedor1
                                   }).ToList();

            /*
             SelectListItem nos sirve para mostrar que datos mostraran en nuestro dropdownlist de 
             nuestra vista
             */
            List<SelectListItem> items = listaProveedores.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.Proveedor1.ToString(),
                    Value = a.Id.ToString(),
                    Selected = false
                };
            });

            ViewBag.items = items; //Viewbag se utiliza para enviar datos a la vista

            return View();
        }
    }
}