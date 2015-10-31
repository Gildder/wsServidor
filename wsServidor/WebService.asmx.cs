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
using System.Security.Cryptography;
using System.Text;
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

        /// <summary>
        /// Este metodo valida usuario ayudante y retorna la lista de inventario
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="contrasenia"></param>
        /// <returns></returns>
        [WebMethod]
        public bool ValidarUsuario(String usuario, String contrasenia)
        {
            List<Inventario> lista = new List<Inventario>();


            cn = ConexionMySql.getInstance();
            DataTable respuesta = (DataTable)cn.ConsultarTabla("*", "view_usuario", "usuario = '" + usuario + "' AND contrasenia ='" + GetMD5(contrasenia) + "'");

  

            //existe usuario
            if(respuesta.Rows.Count > 0)
            {
                return true;
               
            }


            return false;


        }

        
        [WebMethod]
        public String ObtenerInventarioJson()
        {
            List<Inventario> lista = new List<Inventario>();

            //serializa el resultado
            JavaScriptSerializer s = new JavaScriptSerializer();
            var json = "";


            cn = ConexionMySql.getInstance();

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

            cn = ConexionMySql.getInstance();

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

        public static string GetMD5(string str)
        {
            MD5 md5 = MD5CryptoServiceProvider.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = md5.ComputeHash(encoding.GetBytes(str));
            for(int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }

    }//fin clase
}//fin namespace
