using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public class DALCliente
    {
        Datos Data = new Datos();

        SqlConnection con = new SqlConnection(Conexion.cadena);

        public bool RevisarDesactivado(string DNI, string Columnas)
        {
            DataTable dt = Data.LlenarTabla(Columnas, "Cliente");

            foreach (DataRow row in dt.Rows)
            {
                if (row[0].ToString() == DNI)
                {
                    return Convert.ToBoolean(row[1]);
                }
            }
            return false;
        }

        public void DesactivacionCliente(string DNI, int Activo)
        {
            con.Open();
            string query = $"UPDATE Cliente SET Activo = {Activo} WHERE DNI = {DNI}";
            SqlCommand com = new SqlCommand(query, con);
            com.ExecuteNonQuery();
            con.Close();
        }
    }
}
