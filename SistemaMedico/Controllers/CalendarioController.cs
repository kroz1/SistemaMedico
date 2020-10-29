using SistemaMedico.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaMedico.Controllers
{
    public class CalendarioController : Controller
    {
        // GET: Calendario
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetEvents()
        {
            using (citas_medicasEntities1 db = new citas_medicasEntities1())
            {
                var events = db.Events.ToList();
                return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }
    }
}