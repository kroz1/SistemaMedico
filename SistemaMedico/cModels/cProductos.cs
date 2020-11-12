using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaMedico.cModels
{
    public class cProductos
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public Nullable<int> Stock { get; set; }
        public Nullable<decimal> PrecioCompra { get; set; }
        public Nullable<decimal> PrecioVenta { get; set; }
        public System.DateTime Agregado { get; set; }
    }
}