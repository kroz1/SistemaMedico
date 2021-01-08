using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaMedico.cModels
{
    public class cCliente
    {
        public int Id { get; set; }
        public string RegFiscal { get; set; }
        public string Cliente1 { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public Nullable<System.DateTime> Agregado { get; set; }
    }
}