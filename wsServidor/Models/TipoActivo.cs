using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wsServidor.Models
{
    public class TipoActivo
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public TipoActivo()
        {

        }

        public TipoActivo(int id, string tipo, string nombre, string descripcion)
        {
            Id = id;
            Tipo = tipo;
            Nombre = nombre;
            Descripcion = descripcion;
        }
    }
}