using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using BE;

namespace DAL
{
    public class DALUsuario
    {
        Datos Data = new Datos();

        SqlConnection con = new SqlConnection(Conexion.cadena);

        public Usuario RevisarLogIn(string Username, string Contraseña)
        {
            Usuario user = null;
            DataTable dt = Data.LlenarTabla("*", "Usuario");

            foreach (DataRow row in dt.Rows)
            {
                if (row[6].ToString() == Username && row[7].ToString() == Contraseña)
                {
                    user = new Usuario(Convert.ToInt32(row[0]), row[1].ToString(), row[2].ToString(), Convert.ToDateTime(row[3]), row[4].ToString(), row[5].ToString(), row[6].ToString(), row[7].ToString(), row[8].ToString());
                }
            }
            return user;
        }

        public Usuario ObtenerUsuario(string Username)
        {
            Usuario user = null;
            DataTable dt = Data.LlenarTabla("*", "Usuario");

            foreach (DataRow row in dt.Rows)
            {
                if (row[6].ToString() == Username)
                {
                    user = new Usuario(Convert.ToInt32(row[0]), row[1].ToString(), row[2].ToString(), Convert.ToDateTime(row[3]), row[4].ToString(), row[5].ToString(), row[6].ToString(), row[7].ToString(), row[8].ToString());
                }
            }
            return user;
        }

        public bool RevisarSanciones(string Username, string Columnas)
        {
            DataTable dt = Data.LlenarTabla(Columnas, "Usuario");

            foreach (DataRow row in dt.Rows)
            {
                if (row[0].ToString() == Username)
                {
                    return Convert.ToBoolean(row[1]);
                }
            }
            return false;
        }

        public bool RevisarSanciones(string Username, int Num, string Columnas)
        {
            DataTable dt = Data.LlenarTabla(Columnas, "Usuario");

            foreach (DataRow row in dt.Rows)
            {
                if (row[0].ToString() == Username && Convert.ToInt32(row[1]) >= Num)
                {
                    return true;
                }
            }
            return false;
        }

        public bool RevisarSanciones(int Num, string Columnas)
        {
            DataTable dt = Data.LlenarTabla(Columnas, "Usuario");

            foreach (DataRow row in dt.Rows)
            {
                if (Convert.ToInt32(row[0]) == Num)
                {
                    return Convert.ToBoolean(row[1]);
                }
            }
            return false;
        }
    }
}
