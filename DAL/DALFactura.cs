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
    public class DALFactura
    {
        SqlConnection con = new SqlConnection(Conexion.cadena);

        public int ObtenerCodFactura()
        {
            con.Open();
            SqlCommand com = new SqlCommand($"SELECT TOP 1 * FROM Factura ORDER BY CodFactura DESC", con);
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            con.Close();

            return Convert.ToInt32(dt.Rows[0][0]);
        }
    }
}
