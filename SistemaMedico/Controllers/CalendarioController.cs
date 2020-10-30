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

        [HttpPost]
        public JsonResult SaveEvent(Events e)
        {
            var status = false;
            using (citas_medicasEntities1 db = new citas_medicasEntities1())
            {
                if(e.Id > 0)
                {
                    //Actualizar el evento
                    var v = db.Events.Where(a => a.Id == e.Id).FirstOrDefault();
                    if(v != null)
                    {
                        v.Tema = e.Tema;
                        v.FechaInicio = e.FechaInicio;
                        v.FechaFin = e.FechaFin;
                        v.Descripcion = e.Descripcion;
                        v.EsDiaEntero = e.EsDiaEntero;
                        v.Color = e.Color;
                    }
                }
                else
                {
                    db.Events.Add(e);
                }
                db.SaveChanges();
                status = true;
            }
            return new JsonResult { Data = new { status = status } };
        }

        [HttpPost]
        public JsonResult DeleteEvent(int IdEvent)
        {
            var status = false;
            using (citas_medicasEntities1 dc = new citas_medicasEntities1())
            {
                var v = dc.Events.Where(a => a.Id == IdEvent).FirstOrDefault();
                if (v != null)
                {
                    dc.Events.Remove(v);
                    dc.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }
    }
}