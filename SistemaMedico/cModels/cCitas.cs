using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaMedico.cModels
{
    public class cCitas
    {
        public int Id { get; set; }
        public System.DateTime Fecha { get; set; }
        public string Hora { get; set; }
        public int Id_paciente { get; set; }
        public int Id_medico { get; set; }
        public int Id_consultorio { get; set; }
        public string Estado { get; set; }
        public string Observacion { get; set; }
    }
}