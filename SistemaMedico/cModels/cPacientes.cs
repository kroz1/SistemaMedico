using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaMedico.cModels
{
    public class cPacientes
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Edad { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Correo { get; set; }
        public string Genero { get; set; }
        public System.DateTime FechaNacimiento { get; set; }
    }
}