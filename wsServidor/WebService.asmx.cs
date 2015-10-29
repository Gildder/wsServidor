#region Librerias
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using wsServidor.Helper;
using wsServidor.Models;
#endregion

namespace wsServidor
{

    /// <summary>
    /// Descripción breve de WebService
    /// </summary>
    [WebService(Namespace = "http://ibrasact.com.bo/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService : System.Web.Services.WebService
    {
        #region Campos 
        ConexionMySql cn;

        #endregion


        [WebMethod]
        public int suma(int numero, int nu2)
        {
            return 4;
        }


        [WebMethod]
        public bool ValidarUsuario(String usuario, String contrasenia)
        {
            ConexionMySql cn = new ConexionMySql();
            object respuesta = cn.ConsultarTabla("*","usuario", "usuario = '"+usuario + "' AND contrasenia =' "+contrasenia+ "' AND activo = 1 AND habilitado =1 ");

            if(respuesta != null)
                return true;
            
            return false;


        }

        
        [WebMethod]
        public String ObtenerInventarioJson()
        {
            List<Inventario> lista = new List<Inventario>();

            //serializa el resultado
            JavaScriptSerializer s = new JavaScriptSerializer();
            var json = "";


            cn = new ConexionMySql();

            try
            {
                //consulta Inventario DB
                DataTable respuesta = (DataTable)cn.ConsultarTabla("id, nombre, descripcion, prioridad", "inventario", "registrar = 1 ");


                if(respuesta != null)
                {
                    for(int i = 0; i < respuesta.Rows.Count; i++)
                    {
                        Inventario inventario = new Inventario(Convert.ToInt32(respuesta.Rows[i][0]), respuesta.Rows[i][1].ToString(), respuesta.Rows[i][2].ToString(), respuesta.Rows[i][3].ToString());

                        lista.Add(inventario);

                    }
                }

                json = s.Serialize(lista.ToArray());

            }
            catch(Exception e)
            {
                Console.Write("Error: Consulte al administrador");

                return "Consulte administrador";
            }

            
            
            return json;

        }

        [WebMethod]
        public Inventario[] ObtenerInventarioXml()
        {
            List<Inventario> lista = new List<Inventario>();

            cn = new ConexionMySql();

            try
            {
                DataTable respuesta = (DataTable)cn.ConsultarTabla("id, nombre, descripcion, prioridad", "inventario", "registrar = 1 ");

                if(respuesta != null)
                {
                    for(int i = 0; i < respuesta.Rows.Count; i++)
                    {
                        Inventario inventario = new Inventario(Convert.ToInt32(respuesta.Rows[i][0]), respuesta.Rows[i][1].ToString(), respuesta.Rows[i][2].ToString(), respuesta.Rows[i][3].ToString());

                        lista.Add(inventario);
                    }
                }
            }
            catch(Exception e)
            {
                Console.Write("Error: Consulte al administrador");

                return null;

            }

           return lista.ToArray();
        }

    }//fin clase
}//fin namespace
