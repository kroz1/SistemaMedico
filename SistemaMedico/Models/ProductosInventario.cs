//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SistemaMedico.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductosInventario
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
    
        public virtual Categoria Categoria { get; set; }
    }
}