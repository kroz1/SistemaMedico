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
    
    public partial class Compra
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
    
        public virtual Productos Productos { get; set; }
        public virtual Proveedor Proveedor1 { get; set; }
        public virtual Usuarios Usuarios { get; set; }
    }
}
