using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wsServidor.Models
{
    public class Activo
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Serie { get; set; }
        public string Estado { get; set; }
        public string Color { get; set; }
        public string Alto { get; set; }
        public string Ancho { get; set; }
        public string Profundidad { get; set; }
        public string Contenido { get; set; }
        public string Peso { get; set; }
        public string Nro { get; set; }
        public string FechaMantenimieto { get; set; }
        public string Unidad { get; set; }
        public string Cantidad { get; set; }
        public string Material { get; set; }
        public string CodigoTic { get; set; }
        public string CodigoPatrimonio { get; set; }
        public string CodigoActivoFijo { get; set; }
        public string CodigoGerencia { get; set; }
        public string OtroCodigo { get; set; }
        public object Imagen { get; set; }
        public string Observacion { get; set; }
        public string TipoActivo_id { get; set; }
        public string Empleado_id { get; set; }
        public string Ubicacion_id { get; set; }
        public string Inventario_id { get; set; }

        public Activo()
        {

        }


        public Activo(int Id, string Descripcion, string Marca, string Modelo, string Serie, string Estado, string Color, string 
            Alto, string Ancho, string Profundidad, string Contenido, string Peso, string Nro, string FechaMantenimieto, string 
            Unidad, string Cantidad, string Material, string CodigoTic, string CodigoPatrimonio, string CodigoActivoFijo, string CodigoGerencia, string OtroCodigo,
            object Imagen, string Observacion, string TipoActivo_id, string Empleado_id, string Ubicacion_id, string Inventario_id)
        {
            this.Id = Id;
            this.Descripcion =  Descripcion; 
            this.Marca =  Marca; 
            this.Modelo = Modelo ; 
            this.Serie =  Serie; 
            this.Estado =  Estado; 
            this.Color =  Color; 
            this.Alto =  Alto; 
            this.Ancho = Ancho ; 
            this.Profundidad =  Profundidad; 
            this.Contenido =  Contenido; 
            this.Peso =  Peso; 
            this.Nro = Nro ; 
            this.FechaMantenimieto =  FechaMantenimieto; 
            this.Unidad =  Unidad; 
            this.Cantidad = Cantidad ; 
            this.Material =  Material; 
            this.CodigoTic =  CodigoTic; 
            this.CodigoPatrimonio =  CodigoPatrimonio; 
            this.CodigoActivoFijo = CodigoActivoFijo ; 
            this.CodigoGerencia = CodigoGerencia ; 
            this.OtroCodigo = OtroCodigo ; 
            this.Imagen = Imagen ; 
            this.Observacion = Observacion ; 
            this.TipoActivo_id = TipoActivo_id ; 
            this.Empleado_id = Empleado_id ; 
            this.Ubicacion_id = Ubicacion_id ;
            this.Inventario_id = Inventario_id; 
        }

        public Activo(int Id, string Descripcion, string Marca, string Modelo, string Serie, string Estado, string Color, string
           Alto, string Ancho, string Profundidad, string Contenido, string Peso, string Nro, string FechaMantenimieto, string
           Unidad, string Cantidad, string Material, string CodigoTic, string CodigoPatrimonio, string CodigoActivoFijo, string CodigoGerencia, 
            string OtroCodigo,
            string Observacion, string TipoActivo_id, string Empleado_id, string Ubicacion_id, string Inventario_id)
        {
            this.Id = Id;
            this.Descripcion = Descripcion;
            this.Marca = Marca;
            this.Modelo = Modelo;
            this.Serie = Serie;
            this.Estado = Estado;
            this.Color = Color;
            this.Alto = Alto;
            this.Ancho = Ancho;
            this.Profundidad = Profundidad;
            this.Contenido = Contenido;
            this.Peso = Peso;
            this.Nro = Nro;
            this.FechaMantenimieto = FechaMantenimieto;
            this.Unidad = Unidad;
            this.Cantidad = Cantidad;
            this.Material = Material;
            this.CodigoTic = CodigoTic;
            this.CodigoPatrimonio = CodigoPatrimonio;
            this.CodigoActivoFijo = CodigoActivoFijo;
            this.CodigoGerencia = CodigoGerencia;
            this.OtroCodigo = OtroCodigo;
            this.Observacion = Observacion;
            this.TipoActivo_id = TipoActivo_id;
            this.Empleado_id = Empleado_id;
            this.Ubicacion_id = Ubicacion_id;
            this.Inventario_id = Inventario_id;
        }
    }
}