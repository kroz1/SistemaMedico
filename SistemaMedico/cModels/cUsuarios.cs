﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaMedico.cModels
{
    public class cUsuarios
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Usuario { get; set; }
        public string Correo { get; set; }
        public string Grupo { get; set; }
        public System.DateTime Agregado { get; set; }
        public string Estado { get; set; }
        public string Contrasenia { get; set; }
    }
}