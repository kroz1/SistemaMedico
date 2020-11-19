using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaMedico.cModels
{
    public class cCategorias
    {
        public int Id { get; set; }
        public string Categoria1 { get; set; }
        public Nullable<int> NumProductos { get; set; }
        public string Estado { get; set; }
        public System.DateTime Agregado { get; set; }
    }
}