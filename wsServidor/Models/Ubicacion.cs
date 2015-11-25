using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wsServidor.Models
{
    public class Ubicacion
    {
        public int Id { get; set; }
        public string Sector { get; set; }
        public string Area { get; set; }
        public string Lugar { get; set; }

        public Ubicacion()
        {

        }

        public Ubicacion(int id, string sector, string area, string lugar)
        {
            Id = id;
            Sector = sector;
            Area = area;
            Lugar = lugar;
        }
    }
}