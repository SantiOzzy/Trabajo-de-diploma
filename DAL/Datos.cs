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
    public class Datos
    {
        SqlConnection con = new SqlConnection(Conexion.cadena);

        //SqlConnection con = new SqlConnection(@"Data Source=localhost;Initial Catalog=IngenieriaSoftware;Integrated Security=True");

        public void EjecutarComando(string Comando, string Parámetros)
        {
            con.Open();
            string query = "exec " + Comando + " " + Parámetros;
            SqlCommand com = new SqlCommand(query, con);
            com.ExecuteNonQuery();
            con.Close();
        }

        public bool RevisarDisponibilidad(string Dato, string Columna, string Tabla)
        {
            DataTable dt = LlenarTabla(Columna, Tabla);

            foreach (DataRow row in dt.Rows)
            {
                if (row[0].ToString() == Dato)
                {
                    return true;
                }
            }
            return false;
        }

        public bool RevisarDisponibilidadConExcepcion(string Dato, string Excepcion, string Columna, string Tabla)
        {
            DataTable dt = LlenarTabla(Columna, Tabla);

            foreach (DataRow row in dt.Rows)
            {
                if (row[1].ToString() == Dato && row[0].ToString() != Excepcion)
                {
                    return true;
                }
            }
            return false;
        }

        public DataTable LlenarTabla(string Columna, string Tabla)
        {
            con.Open();
            SqlCommand com = new SqlCommand($"SELECT {Columna} FROM {Tabla}", con);
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            con.Close();
            return dt;
        }

        public DataTable LlenarTabla(string Columna, string Tabla, string Condicion)
        {
            con.Open();
            SqlCommand com = new SqlCommand($"SELECT {Columna} FROM {Tabla} WHERE {Condicion}", con);
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            con.Close();
            return dt;
        }

        public void EliminarRegistro(string ID, string Columna, string Tabla)
        {
            con.Open();
            string query = $"DELETE FROM {Tabla} WHERE {Columna} = '{ID}'";
            SqlCommand com = new SqlCommand(query, con);
            com.ExecuteNonQuery();
            con.Close();
        }
    }
}
