using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaMedico.cModels
{
    public class cCompra
    {
        public int Id { get; set; }
        public string NumCompra { get; set; }
        public System.DateTime Fecha { get; set; }
        public decimal Neto { get; set; }
        public decimal IVA { get; set; }
        public decimal Total { get; set; }
        public int Id_proveedor { get; set; }
        public string Proveedor { get; set; }
        public int Id_productos { get; set; }
        public int Id_usuario { get; set; }
    }
}