using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;
using System.Drawing;
using System.IO;

namespace wsServidor.Helper
{
    public class ConexionMySql
    {
        /// <summary>
        /// Atributo singleton
        /// </summary>
        private static ConexionMySql instance;


        private MySqlConnection connection;    
        private MySqlCommand command;    
        private MySqlDataAdapter dataAdapter;
        private MySqlDataReader dataReader;
        private MySqlCommandBuilder commandBuilder;
        public DataSet dataSet;

        private string cadenaConexion = "server="+ Util.SERVIDOR_DB  + 
                                ";user = " + Util.USUARIO_DB + 
                                ";database=" + Util.BASEDATOS +
                                ";port= " + Util.PUERTO_DB + 
                                ";password= " + Util.CONTRASENIA_DB+ ";";



        #region Contructor

        public ConexionMySql()
        {
            Conectar();
        }

        /// <summary>
        /// Metodo singleton para obtener instancia de la clase.
        /// </summary>
        /// <returns></returns>
        public static ConexionMySql getInstance()
        {
            if(instance == null)
            {
                instance = new ConexionMySql();
            }

            return instance;
        }

        #endregion

        #region Metodos

        /// <summary>
        /// Permite la Conexion a la base de datos
        /// </summary>
        public void Conectar()
        {
                connection = new MySqlConnection();
                connection.ConnectionString = cadenaConexion;
        }

       


        
        /// <summary>
        /// Permite insertar a la DB
        /// </summary>
        /// <param name="tabla">Nombre de tabla</param>
        /// <param name="values">Valores a insertar</param>
        /// <returns></returns>
        public bool Insertar(string tabla, string values)
        {
           
            string sql = "INSERT INTO " + tabla + "VALUES (" + values + ")";
            
            connection.Open();
            command = new MySqlCommand(sql, connection);
            int resultado = command.ExecuteNonQuery();
            connection.Close();

            if(resultado > 0)
                return true;
            else
                return false;
           

        }

        /// <summary>
        /// Permite elimnar en la DB
        /// </summary>
        /// <param name="tabla">Nombre tabla</param>
        /// <param name="condicion">Condiciones para eliminar</param>
        /// <returns></returns>
        public bool Eliminar(string tabla, string condicion)
        {
            string sql = "DELETE FROM " + tabla + " WHERE " + condicion;

            connection.Open();
            command = new MySqlCommand(sql, connection);
            int resultado = command.ExecuteNonQuery();
            connection.Close();

            if(resultado > 0)
                return true;
            else
                return false;
            
        }


        /// <summary>
        /// Permite actualizar la DB
        /// </summary>
        /// <param name="tabla">Nombre de la tabla</param>
        /// <param name="campos">Campos a actualizar</param>
        /// <param name="condicion">Condicion para actualizar</param>
        /// <returns></returns>
        public bool Actualizar(string tabla, string campos, string condicion)
        {
            string sql = "UPDATE " + tabla + " SET " + campos + " WHERE " + condicion;

            connection.Open();
            command = new MySqlCommand(sql, connection);
            int resultado = command.ExecuteNonQuery();
            connection.Close();
            
            if(resultado > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Consultamos la tabla
        /// </summary>
        /// <param name="sql">Consulta que se desea reaalizar</param>
        /// <param name="tabla">Tabla de la consulta</param>
        /// <returns></returns>
        public DataTable ConsultarGenerica(string sql, string tabla)
        {
            //comando de conexion para rellenar un DataSet
            dataAdapter = new MySqlDataAdapter(sql, connection);
           //Concilia los dato obtennidos por dataAdapter
            commandBuilder = new MySqlCommandBuilder(dataAdapter);

            dataSet = new DataSet();
            dataAdapter.Fill(dataSet, tabla);

            return dataSet.Tables[tabla];
            
        }



        /// <summary>
        /// Permite Consultar todos las datos de una tabla
        /// </summary>
        /// <param name="tabla">Nombre de la tabla</param>
        /// <returns></returns>
        public DataTable ConsultarTabla(string tabla)
        {
            
            string sql = "SELECT * FROM " + tabla;
            dataAdapter = new MySqlDataAdapter(sql, connection);
            commandBuilder = new MySqlCommandBuilder(dataAdapter);

            dataSet = new DataSet();
            dataAdapter.Fill(dataSet, tabla);

            return dataSet.Tables[tabla];
            
        }

        /// <summary>
        /// Permite consulta campos en particular de una tabla
        /// </summary>
        /// <param name="campos">Campos de interes</param>
        /// <param name="tabla">Nombre de la tabla</param>
        /// <returns></returns>
        public DataTable ConsultarTabla(string campos, string tabla)
        {
            
            string sql = "SELECT " + campos + " FROM " + tabla;
            dataAdapter = new MySqlDataAdapter(sql, connection);
            commandBuilder = new MySqlCommandBuilder(dataAdapter);

            dataSet = new DataSet();
            dataAdapter.Fill(dataSet, tabla);

            return dataSet.Tables[tabla];
            
        }


        /// <summary>
        /// Permite consulta campos particulares de una tabla con una condicion
        /// </summary>
        /// <param name="campos">Campos de interes</param>
        /// <param name="tabla">Nombre de la tabla</param>
        /// <param name="condicion">Condicion de la consulta</param>
        /// <returns></returns>
        public DataTable ConsultarTabla(string campos, string tabla, string condicion)
        {
           
            string sql = "SELECT " + campos + " FROM " + tabla + " WHERE " + condicion;
            dataAdapter = new MySqlDataAdapter(sql, connection);
            commandBuilder = new MySqlCommandBuilder(dataAdapter);

            dataSet = new DataSet();
            dataAdapter.Fill(dataSet, tabla);

            return dataSet.Tables[tabla];
          
        }


        public byte[] GetActivoImage(string condicion)
        {

            string sql = "SELECT imagen FROM activo WHERE id = " + condicion;
            connection.Open();
            command = new MySqlCommand(sql, connection);
            byte[] resultado = (byte[]) command.ExecuteScalar();
            connection.Close();

            return resultado;


        }

        #endregion


        
    }//fin clase

}