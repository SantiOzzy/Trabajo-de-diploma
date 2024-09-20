using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DAL
{
    public class DALRespaldo
    {
        SqlConnection con = new SqlConnection(Conexion.cadena);

        public void Respaldar(string path)
        {
            try
            {
                con.Open();
                string query = $"BACKUP DATABASE IngenieriaSoftware TO DISK='{System.IO.Path.Combine(path, $"LibreriaUAI.BCK_{DateTime.Now:ddMMyy_HHmm}.bak")}'";
                SqlCommand com = new SqlCommand(query, con);
                com.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                throw ex;
            }
        }

        public void Restaurar(string path)
        {
            try
            {
                con.Open();

                string query = "";

                query = $"USE master;";
                SqlCommand com = new SqlCommand(query, con);
                com.ExecuteNonQuery();

                query = "ALTER DATABASE IngenieriaSoftware SET SINGLE_USER WITH ROLLBACK IMMEDIATE;";
                com = new SqlCommand(query, con);
                com.ExecuteNonQuery();

                query = $"RESTORE DATABASE IngenieriaSoftware FROM DISK = '{path}' WITH REPLACE;";
                com = new SqlCommand(query, con);
                com.ExecuteNonQuery();

                query = "ALTER DATABASE IngenieriaSoftware SET MULTI_USER;";
                com = new SqlCommand(query, con);
                com.ExecuteNonQuery();

                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                throw ex;
            }
        }
    }
}
