using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaMedico.cModels
{
    public class cConsultorios
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Estado { get; set; }
        public System.DateTime Agregado { get; set; }

    }
}