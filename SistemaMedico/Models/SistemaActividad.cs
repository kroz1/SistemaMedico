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
    
    public partial class SistemaActividad
    {
        public int Id { get; set; }
        public string Actividad { get; set; }
        public System.DateTime FechaActividad { get; set; }
        public Nullable<int> id_usuario { get; set; }
    
        public virtual Usuarios Usuarios { get; set; }
    }
}