using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;


namespace wsServidor.Helper
{
    public class ConexionMySql
    {
        //atributo de singleton
        private static ConexionMySql conexionMySql;

        //atributo de conexion
        private MySqlConnection connection;
        private MySqlCommand command;
        private MySqlDataAdapter dataAdapter;
        private MySqlCommandBuilder commandBuilder;
        private DataSet dataSet;
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
            if(conexionMySql == null)
            {
                conexionMySql = new ConexionMySql();
            }

            return conexionMySql;
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
            
            connection.Open();
            string sql = "DELETE FROM " + tabla + " WHERE " + condicion;
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
            
            connection.Open();
            string sql = "UPDATE " + tabla + " SET " + campos + " WHERE " + condicion;
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
        public object ConsultarGenerica(string sql, string tabla)
        {
            dataAdapter = new MySqlDataAdapter(sql, connection);
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
        public object ConsultarTabla(string tabla)
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
        public object ConsultarTabla(string campos, string tabla)
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
        public object ConsultarTabla(string campos, string tabla, string condicion)
        {
           
            string sql = "SELECT " + campos + " FROM " + tabla + " WHERE " + condicion;
            dataAdapter = new MySqlDataAdapter(sql, connection);
            commandBuilder = new MySqlCommandBuilder(dataAdapter);

            dataSet = new DataSet();
            dataAdapter.Fill(dataSet, tabla);

            return dataSet.Tables[tabla];
          
        }


        #endregion

    }//fin clase

}