
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
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Drawing;
using System.IO.Compression;
using System.Security.Cryptography;



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


    

        /// <summary>
        /// Este metodo valida usuario ayudante y retorna la lista de inventario
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="contrasenia"></param>
        /// <returns></returns>
        [WebMethod]
        public String ValidarUsuario(String usuario, String contrasenia)
        {
            List<UsuarioDatos> usuarioDatos = new List<UsuarioDatos>();


            cn = ConexionMySql.getInstance();
            DataTable respuesta = (DataTable)cn.ConsultarTabla("id, persona,usuario,empleado_id,persona_id,almacen_id", "view_usuario", "usuario = '" + usuario + "' AND contrasenia ='" + GetMD5(contrasenia) + "'");

            //serializa el resultado
            JavaScriptSerializer s = new JavaScriptSerializer();
            var json = "vacio";

            //existe usuario
            if(respuesta.Rows.Count > 0)
            {
                for(int i = 0; i < respuesta.Rows.Count; i++)
                {
                    UsuarioDatos usuarios = new 
                        UsuarioDatos(Convert.ToInt32(respuesta.Rows[i][0]), 
                        respuesta.Rows[i][1].ToString(), 
                        respuesta.Rows[i][2].ToString(), 
                        Convert.ToInt32(respuesta.Rows[i][3]), 
                        Convert.ToInt32(respuesta.Rows[i][4]), 
                        Convert.ToInt32(respuesta.Rows[i][5]));

                    usuarioDatos.Add(usuarios);

                }

                json = s.Serialize(usuarioDatos.ToArray());
               
            }


            return json;


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
                Console.Write("Error: Consulte al administrador -> "+ e.Message);

                return "Consulte administrador";
            }

            
            
            return json;

        }

      

     


        [WebMethod]
        public String ObtenerTipoActivoJson()
        {
            List<TipoActivo> lista = new List<TipoActivo>();

            //serializa el resultado
            JavaScriptSerializer s = new JavaScriptSerializer();
            var json = "";


            cn = ConexionMySql.getInstance();

            try
            {
                //consulta Inventario DB
                DataTable respuesta = (DataTable)cn.ConsultarTabla("id, tipo, nombre, descripcion", "tipo_activo");


                if(respuesta != null)
                {
                    for(int i = 0; i < respuesta.Rows.Count; i++)
                    {
                        TipoActivo tipoActivo = new TipoActivo(
                            Convert.ToInt32(respuesta.Rows[i][0]), 
                            respuesta.Rows[i][1].ToString(), 
                            respuesta.Rows[i][2].ToString(), 
                            respuesta.Rows[i][3].ToString());

                        lista.Add(tipoActivo);

                    }
                }

                json = s.Serialize(lista.ToArray());

            }
            catch(Exception e)
            {
                Console.Write("Error: Consulte al administrador -> " + e.Message);

                return "Consulte administrador";
            }



            return json;

        }

        [WebMethod]
        public String ObtenerUbicacionJson(string almacen)
        {
            List<Ubicacion> lista = new List<Ubicacion>();

            //serializa el resultado
            JavaScriptSerializer s = new JavaScriptSerializer();
            var json = "";


            cn = ConexionMySql.getInstance();

            try
            {
                //consulta Inventario DB
                DataTable respuesta = (DataTable)cn.ConsultarTabla("id, sector, area, lugar", "ubicacion", "almacen_id = "+almacen);


                if(respuesta != null)
                {
                    for(int i = 0; i < respuesta.Rows.Count; i++)
                    {
                        Ubicacion ubicacion = new Ubicacion(
                            Convert.ToInt32(respuesta.Rows[i][0]),
                            respuesta.Rows[i][1].ToString(),
                            respuesta.Rows[i][2].ToString(),
                            respuesta.Rows[i][3].ToString());

                        lista.Add(ubicacion);

                    }
                }

                json = s.Serialize(lista.ToArray());

            }
            catch(Exception e)
            {
                Console.Write("Error: Consulte al administrador -> " + e.Message);

                return "Consulte administrador";
            }



            return json;

        }

        //Retornos XML **********************************************
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
                Console.Write("Error: Consulte al administrador -> " + e.Message);

                return null;

            }

            return lista.ToArray();
        }

        [WebMethod]
        public Activo[] ObtenerActivoXml(int ids)
        {
            List<Activo> lista = new List<Activo>();

            cn = ConexionMySql.getInstance();


            try
            {

                DataTable respuesta = (DataTable)cn.ConsultarTabla(
                    "id,descripcion,marca,modelo,serie,estado,color,alto,ancho,profundidad,contenido,peso,nro,fechaMantenimiento,unidad,cantidad,material,codigoTIC,codigoPatrimonio,codigoActivoFijo,codigoGerencia,otroCodigo,imagen,observacion,tipoactivo_id,empleado_id,ubicacion_id,inventario_id ",
                    "view_activo", "inventario_id = " + ids.ToString());

                if(respuesta != null)
                {
                    for(int i = 0; i < respuesta.Rows.Count; i++)
                    {
                        Activo inventario = new Activo(Convert.ToInt32(respuesta.Rows[i][0]), respuesta.Rows[i][1].ToString(), respuesta.Rows[i][2].ToString(), respuesta.Rows[i][3].ToString(), respuesta.Rows[i][4].ToString(), respuesta.Rows[i][5].ToString(), respuesta.Rows[i][6].ToString()
                            , respuesta.Rows[i][7].ToString(), respuesta.Rows[i][8].ToString(), respuesta.Rows[i][9].ToString(), respuesta.Rows[i][10].ToString(), respuesta.Rows[i][11].ToString(), respuesta.Rows[i][12].ToString(), respuesta.Rows[i][13].ToString(), respuesta.Rows[i][14].ToString(), respuesta.Rows[i][15].ToString(), respuesta.Rows[i][16].ToString(), respuesta.Rows[i][17].ToString()
                            , respuesta.Rows[i][18].ToString(), respuesta.Rows[i][19].ToString(), respuesta.Rows[i][20].ToString(), respuesta.Rows[i][21].ToString(),
                           (respuesta.Rows[i][22]), 
                            respuesta.Rows[i][23].ToString(), respuesta.Rows[i][24].ToString(), respuesta.Rows[i][25].ToString(), respuesta.Rows[i][26].ToString(), respuesta.Rows[i][27].ToString());
                            //respuesta.Rows[i][22].ToString(), respuesta.Rows[i][23].ToString(), respuesta.Rows[i][24].ToString(), respuesta.Rows[i][25].ToString(), respuesta.Rows[i][26].ToString());

                        lista.Add(inventario);
                    }
                }
                
            }
            catch(Exception e)
            {
                Console.Write("Error: Consulte al administrador -> " + e.Message);

                return null;

            }

            return lista.ToArray();
        }



        [WebMethod]
        public Bitmap ObtenerActivoImge2(string ids)
        {
            cn = ConexionMySql.getInstance();

            byte[] bytes = cn.GetActivoImage(ids.ToString());
            
                using(var stream = new MemoryStream(bytes))
                {
                    var imageConverter = new ImageConverter();
                    var image = (Image)imageConverter.ConvertFrom(bytes);
                    return new Bitmap(image);
                }

               // return bytes.ToString() ;
           
            
        }


        public void setActivo(Activo activo)
        {

        }


        #region metodos

        [WebMethod]
        public TipoActivo[] ObtenerTipoActivoXml()
        {
            List<TipoActivo> lista = new List<TipoActivo>();


            cn = ConexionMySql.getInstance();

            try
            {
                //consulta Inventario DB
                DataTable respuesta = (DataTable)cn.ConsultarTabla("id, tipo, nombre, descripcion", "tipo_activo");


                if(respuesta != null)
                {
                    for(int i = 0; i < respuesta.Rows.Count; i++)
                    {
                        TipoActivo tipoActivo = new TipoActivo(
                            Convert.ToInt32(respuesta.Rows[i][0]),
                            respuesta.Rows[i][1].ToString(),
                            respuesta.Rows[i][2].ToString(),
                            respuesta.Rows[i][3].ToString());

                        lista.Add(tipoActivo);

                    }
                }

            }
            catch(Exception e)
            {
                Console.Write("Error: Consulte al administrador -> " + e.Message);

            }



            return lista.ToArray();

        }

        [WebMethod]
        public Ubicacion[] ObtenerUbicacionXml(string id)
        {
            List<Ubicacion> lista = new List<Ubicacion>();

            cn = ConexionMySql.getInstance();

            try
            {
                //consulta Inventario DB
                DataTable respuesta = (DataTable)cn.ConsultarTabla("id, sector, area, lugar", "ubicacion", "almacen_id = " + id);


                if(respuesta != null)
                {
                    for(int i = 0; i < respuesta.Rows.Count; i++)
                    {
                        Ubicacion ubicacion = new Ubicacion(
                            Convert.ToInt32(respuesta.Rows[i][0]),
                            respuesta.Rows[i][1].ToString(),
                            respuesta.Rows[i][2].ToString(),
                            respuesta.Rows[i][3].ToString());

                        lista.Add(ubicacion);

                    }
                }


            }
            catch(Exception e)
            {
                Console.Write("Error: Consulte al administrador -> " + e.Message);

            }



            return lista.ToArray();

        }

        #endregion

        // Convert an object to a byte array
        public static byte[] ObjectToByteArray(Object obj)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using(var ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        // Convert an object to a string
        public static String ObjectToString(Object obj)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using(var ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return Convert.ToBase64String(ms.ToArray());
            }
        }


        // Convert a byte array to an Object
        public static Object ByteArrayToObject(byte[] arrBytes)
        {
            using(var memStream = new MemoryStream())
            {
                var binForm = new BinaryFormatter();
                memStream.Write(arrBytes, 0, arrBytes.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                var obj = binForm.Deserialize(memStream);
                return obj;
            }
        }

        // Convert a byte array to a String
        public static string ByteArrayToString(byte[] arrBytes)
        {
            return Convert.ToBase64String(arrBytes);
        }



        public int[] StringToInt(string valor)
        {
            char[] ArrayIds = valor.ToCharArray(0, valor.Length);
            int[] IntIds = new int[ArrayIds.Length / 2];

            int inx = 0;
            for(int k = 0; k < ArrayIds.Length; k++)
            {
                try
                {
                    IntIds[inx] = Convert.ToInt32(ArrayIds[k].ToString());
                    inx++;
                }catch(Exception e){

                }
               
            }
            return IntIds;
        }


        //Funcion `para convertir una imagen a bytes
        public Byte[] Imagen_A_Bytes(String ruta)
        {
            FileStream foto = new FileStream(ruta, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            Byte[] arreglo = new Byte[foto.Length];
            BinaryReader reader = new BinaryReader(foto);
            arreglo = reader.ReadBytes(Convert.ToInt32(foto.Length));
            return arreglo;
        }

        //Funcion para convertir un Arreglo de bytes a imagen
        public Bitmap Bytes_A_Imagen(Byte[] ImgBytes)
        {
            Bitmap imagen = null;
            Byte[] bytes = (Byte[])(ImgBytes);
            MemoryStream ms = new MemoryStream(bytes);
            imagen = new Bitmap(ms);
            return imagen;
        }


        //Metodos para encriptar
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


        //>codificar
        public static string Base64_Encode(string str)
        {
            byte[] encbuff = System.Text.Encoding.UTF8.GetBytes(str);
            return Convert.ToBase64String(encbuff);
        }

        //> Decodificar
        public static string Base64_Decode(string str)
        {
            try
            {
                byte[] decbuff = Convert.FromBase64String(str);
                return System.Text.Encoding.UTF8.GetString(decbuff);
            }
            catch
            {
                { return ""; }
            }
        }


        public static byte[] Zip(byte[] value)
        {
         
            //Prepare for compress
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            System.IO.Compression.GZipStream sw = new System.IO.Compression.GZipStream(ms, System.IO.Compression.CompressionMode.Compress);

            //Compress
            sw.Write(value, 0, value.Length);
            //Close, DO NOT FLUSH cause bytes will go missing...
            sw.Close();

            //Transform byte[] zip data to string
            value = ms.ToArray();
            System.Text.StringBuilder sB = new System.Text.StringBuilder(value.Length);
            foreach(byte item in value)
            {
                sB.Append((char)item);
            }
            ms.Close();
            sw.Dispose();
            ms.Dispose();
            return ms.ToArray(); 
        }




    }//fin clase
}//fin namespace
