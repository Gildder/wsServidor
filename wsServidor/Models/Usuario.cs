using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wsServidor.Models
{
    public class UsuarioDatos
    {
        public int Id { get; set; }
        public string Persona { get; set; }
        public string Usuario { get; set; }
        public int Empleado_id { get; set; }
        public int Persona_id { get; set; }
        public int Almacen_id { get; set; }


        public UsuarioDatos()
        {
        }



        public UsuarioDatos(int id,string persona,string usuario, int empleado_id, int persona_id, int almacen_id)
        {
            this.Id = id;
            this.Persona = persona;
            this.Usuario = usuario;
            this.Empleado_id = empleado_id;
            this.Persona_id = persona_id;
            this.Almacen_id = almacen_id;
        }
    }
}