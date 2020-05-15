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
    public class PacientesController : Controller
    {
        public Models.citas_medicasEntities db = new Models.citas_medicasEntities();
        // GET: Pacientes
        public ActionResult Index()
        {
            return View();
        }

        public string listar()
        {
            //consulta a la tabla pacientes
            var query = (from a in db.Pacientes select a).OrderBy(a => a.Id).ToList<Pacientes>();
            //creamos una lista donde vamos a guardar los pacientes de la tabla
            List<cPacientes> listaPacientes = new List<cPacientes>();
            //creamos un ciclo donde recorreremos cuantos pacientes estan dados de alta
            foreach (Pacientes pacientes in query)
            {
                //hacemos una instancia a la clase cPacientes
                cPacientes objPacientes = new cPacientes();
                //agregamos los datos a los atributos de nuestra clase Pacientes a cPacientes
                objPacientes.Id = pacientes.Id;
                objPacientes.Nombre = pacientes.Nombre;
                objPacientes.Apellido = pacientes.Apellido;
                objPacientes.Edad = pacientes.Edad;
                objPacientes.Telefono = pacientes.Telefono;
                objPacientes.Direccion = pacientes.Direccion;
                objPacientes.Correo = pacientes.Correo;
                objPacientes.Genero = pacientes.Genero;
                objPacientes.FechaNacimiento = pacientes.FechaNacimiento;

                //agregamos los datos a nuestra lista
                listaPacientes.Add(objPacientes);
            }

            return JsonConvert.SerializeObject(new
            {
                status = true,
                mensaje = "Datos cargados",
                data = listaPacientes
            });
        }

        public JsonResult guardar(cPacientes pacientes)
        {
            //creamos el objeto de la clase Pacientes
            Pacientes objPaciente = new Pacientes();
            if(pacientes.FechaNacimiento == null)
            {
                return Json(new { status = false, mensaje = "La fecha esta vacia" });
            }
            if(pacientes.Id != 0)
            {
                //editar
                //consulta a la tabla para actualizar al paciente seleccionado
                objPaciente = db.Pacientes.Where(a => a.Id == pacientes.Id).FirstOrDefault();
                if(objPaciente == null)
                {
                    return Json(new { status = false, mensaje = "No existe el registro"});
                }
                else
                {
                    objPaciente.Nombre = pacientes.Nombre;
                    objPaciente.Apellido = pacientes.Apellido;
                    objPaciente.Edad = pacientes.Edad;
                    objPaciente.Telefono = pacientes.Telefono;
                    objPaciente.Direccion = pacientes.Direccion;
                    objPaciente.Correo = pacientes.Correo;
                    objPaciente.Genero = pacientes.Genero;
                    objPaciente.FechaNacimiento = pacientes.FechaNacimiento;

                    db.Pacientes.Attach(objPaciente);
                    db.Entry(objPaciente).State = System.Data.Entity.EntityState.Modified;
                }
            }
            else
            {
                //nuevo
                objPaciente.Nombre = pacientes.Nombre;
                objPaciente.Apellido = pacientes.Apellido;
                objPaciente.Edad = pacientes.Edad;
                objPaciente.Telefono = pacientes.Telefono;
                objPaciente.Direccion = pacientes.Direccion;
                objPaciente.Correo = pacientes.Correo;
                objPaciente.Genero = pacientes.Genero;
                objPaciente.FechaNacimiento = pacientes.FechaNacimiento;

                //agregamos los datos ingresados a la tabla
                db.Pacientes.Add(objPaciente);
            }
            db.SaveChanges();
            return Json(new { status = true, mensaje = "Datos guardados", datos = objPaciente});
        }

        public JsonResult eliminar(int Id)
        {
            Pacientes objPacientes = new Pacientes();
            if(Id == 0)
            {
                return Json(new { status = false, mensaje = "El id esta en 0" });
            }
            else
            {
                //consulta a la tabla para obtener el paciente por el id
                objPacientes = db.Pacientes.Where(a => a.Id == Id).FirstOrDefault();
                if(objPacientes == null)
                {
                    return Json(new { status = false, mensaje = "No existe el registro" });
                }
                else
                {
                    //procedemos a eliminar el registro
                    db.Pacientes.Attach(objPacientes);
                    db.Pacientes.Remove(objPacientes);
                    db.SaveChanges();

                    return Json(new { status = true, mensaje = "Registro eliminado" });
                }
            }
        }

        public JsonResult editar(int Id)
        {
            Pacientes objPacientes = new Pacientes();
            if (Id == 0)
            {
                return Json(new { status = false, mensaje = "El id esta en 0" });
            }
            else
            {
                //consulta a la tabla para obtener el paciente por el id
                objPacientes = db.Pacientes.Where(a => a.Id == Id).FirstOrDefault();
                if (objPacientes == null)
                {
                    return Json(new { status = false, mensaje = "No existe el registro" });
                }
                return Json(new { status = true, mensaje = "Registro cargado", datos = objPacientes });
            }
        }
    }
}