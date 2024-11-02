using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using BE;
using Services;

namespace DAL
{
    public class DALDV
    {
        SqlConnection con = new SqlConnection(Conexion.cadena);

        Datos datos = new Datos();

        public void RecalcularDVHTabla(string Tabla, string DVH)
        {
            con.Open();
            SqlCommand com = new SqlCommand($"UPDATE DV SET DVH = '{DVH}' WHERE Tabla = '{Tabla}'", con);
            com.ExecuteNonQuery();
            con.Close();
        }

        public void RecalcularDVVTabla(string Tabla, string DVV)
        {
            con.Open();
            SqlCommand com = new SqlCommand($"UPDATE DV SET DVV = '{DVV}' WHERE Tabla = '{Tabla}'", con);
            com.ExecuteNonQuery();
            con.Close();
        }

        public DataTable ObtenerDV(string Tabla)
        {
            DataTable dt = datos.LlenarTabla("*", "DV", $"Tabla = '{Tabla}'");
            return dt;
        }
    }
}
