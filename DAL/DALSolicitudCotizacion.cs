using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace DAL
{
    public class DALSolicitudCotizacion
    {
        SqlConnection con = new SqlConnection(Conexion.cadena);

        public int ObtenerCodSolicitudCotizacion()
        {
            con.Open();
            SqlCommand com = new SqlCommand($"SELECT TOP 1 * FROM SolicitudCotizacion ORDER BY CodSolicitud DESC", con);
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            con.Close();

            return Convert.ToInt32(dt.Rows[0][0]);
        }
    }
}
