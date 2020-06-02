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
    public class UsuariosController : Controller
    {
        public Models.citas_medicasEntities db = new Models.citas_medicasEntities();
        // GET: Usuarios
        public ActionResult Index()
        {
            return View();
        }

        public string listar()
        {
            try
            {
                var query = (from a in db.Usuarios select a).OrderBy(a => a.Id).ToList<Usuarios>();
                List<cUsuarios> listaUsuarios = new List<cUsuarios>();
                foreach(Usuarios usuarios in query)
                {
                    cUsuarios objUsuarios = new cUsuarios();
                    objUsuarios.Id = usuarios.Id;
                    objUsuarios.Nombre = usuarios.Nombre;
                    objUsuarios.Apellido = usuarios.Apellido;
                    objUsuarios.Usuario = usuarios.Usuario;
                    objUsuarios.Correo = usuarios.Correo;
                    objUsuarios.Grupo = usuarios.Grupo;
                    objUsuarios.Agregado = usuarios.Agregado;
                    objUsuarios.Estado = usuarios.Estado;

                    listaUsuarios.Add(objUsuarios);
                }

                return JsonConvert.SerializeObject(new
                {
                    status = true,
                    mensaje = "Datos cargados",
                    data = listaUsuarios
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

        public JsonResult guardar(cUsuarios objUsuario)
        {
            Usuarios usuarios = new Usuarios();
            usuarios.Nombre = objUsuario.Nombre;
            usuarios.Apellido = objUsuario.Apellido;
            usuarios.Usuario = objUsuario.Usuario;
            usuarios.Correo = objUsuario.Correo;
            usuarios.Grupo = objUsuario.Grupo;
            usuarios.Agregado = DateTime.Now;
            usuarios.Estado = objUsuario.Estado;
            usuarios.Contrasenia = objUsuario.Contrasenia;

            db.Usuarios.Add(usuarios);
            db.SaveChanges();
            return Json(new { status = true, mensaje = "Datos guardados", datos = usuarios });
        }

        public JsonResult guardarCambios(cUsuarios objUsuario)
        {
            Usuarios usuarios = new Usuarios();
            usuarios = db.Usuarios.Where(a => a.Id == objUsuario.Id).FirstOrDefault();
            if(usuarios == null)
            {
                return Json(new { status = false, mensaje = "No existe el registro" });
            }
            usuarios.Nombre = objUsuario.Nombre;
            usuarios.Apellido = objUsuario.Apellido;
            usuarios.Usuario = objUsuario.Usuario;
            usuarios.Correo = objUsuario.Correo;
            usuarios.Grupo = objUsuario.Grupo;
            //usuarios.Agregado = DateTime.Now;
            usuarios.Estado = objUsuario.Estado;

            db.Usuarios.Attach(usuarios);
            db.Entry(usuarios).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return Json(new { status = true, mensaje = "Datos guardados", datos = usuarios });
        }

        public JsonResult guardarNuevaContrasena(cUsuarios objUsuario)
        {
            Usuarios o = new Usuarios();
            if(objUsuario.Id == 0)
            {
                return Json(new { status = false, mensaje = "El id viene en 0" });
            }

            if(objUsuario.Contrasenia == null)
            {
                return Json(new { status = false, mensaje = "La contraseña viene vacia" });
            }
            o = db.Usuarios.Where(a => a.Id == objUsuario.Id).FirstOrDefault();
            if(o == null)
            {
                return Json(new { status = false, mensaje = "No existe el registro" });
            }

            o.Contrasenia = objUsuario.Contrasenia;
            db.Usuarios.Attach(o);
            db.Entry(o).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return Json(new { status = true, mensaje = "Datos guardados", datos = o });
        }

        public JsonResult eliminar(int Id)
        {
            Usuarios o = new Usuarios();
            if(Id == 0)
            {
                return Json(new { status = false, mensaje = "El id esta en 0" });
            }

            o = db.Usuarios.Where(a => a.Id == Id).FirstOrDefault();
            if(o == null)
            {
                return Json(new { status = false, mensaje = "No existe el registro" });
            }
            else
            {
                db.Usuarios.Attach(o);
                db.Usuarios.Remove(o);
                db.SaveChanges();

                return Json(new { status = true, mensaje = "Usuario eliminado" });
            }
        }

        public JsonResult editar(int Id)
        {
            Usuarios o = new Usuarios();
            if (Id == 0)
            {
                return Json(new { status = false, mensaje = "El id esta en 0" });
            }

            o = db.Usuarios.Where(a => a.Id == Id).FirstOrDefault();
            if (o == null)
            {
                return Json(new { status = false, mensaje = "No existe el registro" });
            }
            return Json(new { status = true, mensaje = "Datos cargados", datos = o });
        }
    }
}