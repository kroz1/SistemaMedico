using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaMedico.Controllers
{
    public class CitasController : Controller
    {
        // GET: Citas
        public ActionResult Index()
        {
            return View();
        }
    }
}