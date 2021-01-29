using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaMedico.Models;
using SistemaMedico.cModels;
using Newtonsoft.Json;

namespace SistemaMedico.Controllers
{
    public class ClientesController : Controller
    {
        public Models.citas_medicasEntities1 db = new Models.citas_medicasEntities1();
        // GET: Clientes
        public ActionResult Index()
        {
            return View();
        }

        public string listar()
        {
            var query = (from p in db.Cliente select p).OrderBy(p => p.Id).ToList<Cliente>();
            List<cCliente> listaCliente = new List<cCliente>();
            foreach (Cliente cliente in query)
            {
                cCliente ObjCliente = new cCliente();
                ObjCliente.Id = cliente.Id;
                ObjCliente.Cliente1 = cliente.Cliente1;
                ObjCliente.RegFiscal = cliente.RegFiscal;
                ObjCliente.Direccion = cliente.Direccion;
                ObjCliente.Email = cliente.Email;
                ObjCliente.Telefono = cliente.Telefono;
                ObjCliente.Agregado = cliente.Agregado;

                listaCliente.Add(ObjCliente);
            }
            return JsonConvert.SerializeObject(new
            {
                status = true,
                mensaje = "Datos cargados",
                data = listaCliente
            });
        }

        public JsonResult guardar(cCliente ccliente)
        {
            Cliente objCliente = new Cliente();
            if (ccliente.Id != 0)
            {
                //editar
                objCliente = db.Cliente.Where(a => a.Id == ccliente.Id).FirstOrDefault();
                if (objCliente == null)
                {
                    return Json(new { status = false, mensaje = "No existe el registro" });
                }
                else
                {
                    objCliente.RegFiscal = ccliente.RegFiscal;
                    objCliente.Cliente1 = ccliente.Cliente1;
                    objCliente.Direccion = ccliente.Direccion;
                    objCliente.Email = ccliente.Email;
                    objCliente.Telefono = ccliente.Telefono;
                    objCliente.Agregado = DateTime.Now;

                    db.Cliente.Attach(objCliente);
                    db.Entry(objCliente).State = System.Data.Entity.EntityState.Modified;
                }
            }
            else
            {
                //nuevo
                objCliente.RegFiscal = ccliente.RegFiscal;
                objCliente.Cliente1 = ccliente.Cliente1;
                objCliente.Direccion = ccliente.Direccion;
                objCliente.Email = ccliente.Email;
                objCliente.Telefono = ccliente.Telefono;
                objCliente.Agregado = DateTime.Now;
                db.Cliente.Add(objCliente);
            }
            db.SaveChanges();
            return Json(new { status = true, mensaje = "Datos guardados", datos = objCliente });
        }

        public JsonResult eliminar(int Id)
        {
            Cliente objCliente = new Cliente();
            if (Id == 0)
            {
                return Json(new { status = false, mensaje = "El id esta en 0" });
            }

            objCliente = db.Cliente.Where(a => a.Id == Id).FirstOrDefault();
            if (objCliente == null)
            {
                return Json(new { status = false, mensaje = "No existe el registro" });
            }
            else
            {
                db.Cliente.Attach(objCliente);
                db.Cliente.Remove(objCliente);
                db.SaveChanges();

                return Json(new { status = true, mensaje = "Registro eliminado" });
            }
        }

        public JsonResult editar(int Id)
        {
            Cliente objCliente = new Cliente();
            if (Id == 0)
            {
                return Json(new { status = false, mensaje = "El id esta en 0" });
            }

            objCliente = db.Cliente.Where(a => a.Id == Id).FirstOrDefault();
            if (objCliente == null)
            {
                return Json(new { status = false, mensaje = "No existe el registro" });
            }

            return Json(new { status = true, mensaje = "Datos cargados", datos = objCliente });
        }
    }
}
