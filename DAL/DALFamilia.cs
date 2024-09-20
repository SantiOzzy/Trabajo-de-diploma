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
    public class DALFamilia
    {
        Datos Data = new Datos();

        SqlConnection con = new SqlConnection(Conexion.cadena);

        public bool VerificarTipo(string Nombre)
        {
            DataTable dt = Data.LlenarTabla("Nombre, Tipo", "Familia");

            foreach (DataRow row in dt.Rows)
            {
                if (row[0].ToString() == Nombre)
                {
                    return Convert.ToBoolean(row[1]);
                }
            }
            return false;
        }
    }
}
