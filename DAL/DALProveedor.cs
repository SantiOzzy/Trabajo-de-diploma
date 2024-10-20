using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using BE;

namespace DAL
{
    public class DALProveedor
    {
        Datos Data = new Datos();
        public Proveedor ObtenerProveedor(string CUIT)
        {
            Proveedor prov = null;
            DataTable dt = Data.LlenarTabla("*", "Proveedor");

            foreach (DataRow row in dt.Rows)
            {
                if (row[0].ToString() == CUIT)
                {
                    prov = new Proveedor(row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString(), row[5].ToString(), row[6].ToString());
                }
            }
            return prov;
        }
    }
}
