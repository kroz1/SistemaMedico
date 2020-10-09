using SistemaMedico.Clases;
using SistemaMedico.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaMedico.cModels
{
    public class cUsuarios
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Usuario { get; set; }
        public string Correo { get; set; }
        public string Grupo { get; set; }
        public System.DateTime Agregado { get; set; }
        public string Estado { get; set; }
        public string Contrasenia { get; set; }


        public Usuarios ingresar(string txtUsuario, string txtPassword)
        {
            try
            {
                //si el usuario ingreso este correo y el password
                if((txtUsuario.Trim() == "a@a.com") && (txtPassword.Trim() == "a"))
                {
                    Usuarios objUsuario = new Usuarios();
                    objUsuario.Nombre = "Administrador";
                    objUsuario.Id = -1;
                    cGeneral.oUsuario = objUsuario;
                    return objUsuario;
                }
                else
                {
                    citas_medicasEntities db = new citas_medicasEntities();
                    //query consultar si existe el correo y el password en la base de datos
                    Usuarios registro = (from r in db.Usuarios.Where(
                                            a => a.Correo.Equals(txtUsuario) & a.Contrasenia.Equals(txtPassword))
                                         select r).FirstOrDefault();

                    if(registro != null)
                    {
                        cGeneral.oUsuario = registro;
                        return registro;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception error)
            {

                throw (error);
            }
        }
    }
}