﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class citas_medicasEntities1 : DbContext
    {
        public citas_medicasEntities1()
            : base("name=citas_medicasEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Citas> Citas { get; set; }
        public virtual DbSet<Consultorios> Consultorios { get; set; }
        public virtual DbSet<Especialidades> Especialidades { get; set; }
        public virtual DbSet<Medicos> Medicos { get; set; }
        public virtual DbSet<Pacientes> Pacientes { get; set; }
        public virtual DbSet<PerfilEmpresa> PerfilEmpresa { get; set; }
        public virtual DbSet<SistemaActividad> SistemaActividad { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }
        public virtual DbSet<Events> Events { get; set; }
        public virtual DbSet<Productos> Productos { get; set; }
        public virtual DbSet<Categoria> Categoria { get; set; }
        public virtual DbSet<ProductosInventario> ProductosInventario { get; set; }
    }
}
