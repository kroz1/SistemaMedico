using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaMedico.cModels;
using SistemaMedico.Models;
using SistemaMedico.Clases;

namespace SistemaMedico.Controllers
{
    public class LoginController : Controller
    {
        public Models.citas_medicasEntities1 db = new Models.citas_medicasEntities1();
        public ActionResult LoginNuevo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginNuevo(FormCollection form)
        {
            string correoElectronico = "";
            if (form.AllKeys.Contains("correoElectronico"))
            {
                correoElectronico = form["correoElectronico"].ToString();
            }
            string password = "";
            if (form.AllKeys.Contains("pass"))
            {
                password = form["pass"].ToString();
            }

            cUsuarios cUsuarios = new cUsuarios();
            Usuarios usuarios = cUsuarios.ingresar(correoElectronico, password);

            if(usuarios != null) 
            {
                Session["nombreCompleto"] = usuarios.Nombre;
                Session["correoElectronico"] = usuarios.Correo;
                Session["id"] = usuarios.Id;

                if(usuarios.Id != -1)
                {
                    string nombreCompleto = usuarios.Nombre + " " + usuarios.Apellido;

                    SistemaActividad oSistemaActividad = new SistemaActividad();
                    oSistemaActividad.Actividad = "Ingreso al sistema el usuario: " + nombreCompleto;
                    oSistemaActividad.FechaActividad = DateTime.Now;
                    oSistemaActividad.id_usuario = usuarios.Id;
                    //oSistemaActividad.Usuarios = cGeneral.oUsuario;

                    db.SistemaActividad.Add(oSistemaActividad);
                    db.SaveChanges();
                }
                //Si esta registrado el usuario
                return RedirectToAction("Index", "Home");
            }
            else
            {
                //sino encuentra el usuario ingresado dado de alta
                ViewBag.Message = "El correo y/o contraseña son incorrectos, favor de verificar tus credenciales";
                return View("LoginNuevo");
            }           
        }
    }
}