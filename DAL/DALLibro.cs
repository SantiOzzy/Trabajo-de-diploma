using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DAL
{
    public class DALLibro
    {
        Datos Data = new Datos();

        SqlConnection con = new SqlConnection(Conexion.cadena);

        public bool RevisarDesactivado(string ISBN, string Columnas)
        {
            DataTable dt = Data.LlenarTabla(Columnas, "Libro");

            foreach (DataRow row in dt.Rows)
            {
                if (row[0].ToString() == ISBN)
                {
                    return Convert.ToBoolean(row[1]);
                }
            }
            return false;
        }

        public void DesactivacionLibro(string ISBN, int Activo)
        {
            con.Open();
            string query = $"UPDATE Libro SET Activo = {Activo} WHERE ISBN = {ISBN}";
            SqlCommand com = new SqlCommand(query, con);
            com.ExecuteNonQuery();
            con.Close();
        }
    }
}
