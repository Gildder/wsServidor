using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using wsServidor.Helper;

namespace wsServidor.DAO
{
    public class UsuarioDao
    {
        private ConexionMySql con;


        public bool ValidarUsuario(string usuario, string contrasenia)
        {
            con = new ConexionMySql();

            return false;
        }
    }
}