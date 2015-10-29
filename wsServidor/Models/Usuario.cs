using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wsServidor.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Contrasenia { get; set; }

        public Usuario()
        {
        }

        public Usuario(string nombre, string contrasenia)
        {
            this.Nombre = nombre;
            this.Contrasenia = contrasenia;
        }
    }
}