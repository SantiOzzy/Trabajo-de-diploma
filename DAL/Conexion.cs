using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading.Tasks;

namespace DAL
{
    public static class Conexion
    {
        public static string cadena = @"Data Source=PCSANTY\PCSANTI;Initial Catalog=IngenieriaSoftware;Integrated Security=True";

        //public static string cadena = @"Data Source=.;Initial Catalog=IngenieriaSoftware;Integrated Security=True";
    }
}
