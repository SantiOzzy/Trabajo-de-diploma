using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class DALOrdenCompra
    {
        SqlConnection con = new SqlConnection(Conexion.cadena);

        public int ObtenerCodOrdenCompra()
        {
            con.Open();
            SqlCommand com = new SqlCommand($"SELECT TOP 1 * FROM OrdenCompra ORDER BY CodOrdenCompra DESC", con);
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            con.Close();

            return Convert.ToInt32(dt.Rows[0][0]);
        }
    }
}
