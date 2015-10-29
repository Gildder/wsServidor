using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wsServidor.Models
{
    public class Inventario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Prioridad { get; set; }

        public Inventario()
        {

        }

        public Inventario(int id, string nombre, string descripcion, string prioridad)
        {
            Id = id;
            Nombre = nombre;
            Descripcion = descripcion;
            Prioridad = prioridad;
        }
    }
}