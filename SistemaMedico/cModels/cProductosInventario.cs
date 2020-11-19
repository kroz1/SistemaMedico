using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaMedico.cModels
{
    public class cProductosInventario
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Presentacion { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Costo { get; set; }
        public Nullable<decimal> Utilidad { get; set; }
        public decimal PrecioVenta { get; set; }
        public Nullable<int> StockInicial { get; set; }
        public string Estado { get; set; }
        public byte[] Imagen { get; set; }
        public Nullable<int> Id_Categoria { get; set; }
    }
}