using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using BE;
using DAL;

namespace BLL
{
    public class Negocios
    {
        Datos Data = new Datos();
        public bool RevisarDisponibilidad(string Dato, string Columna, string Tabla)
        {
            return Data.RevisarDisponibilidad(Dato, Columna, Tabla);
        }

        public bool RevisarDisponibilidadConExcepcion(string Dato, string Excepcion, string Columna, string Tabla)
        {
            return Data.RevisarDisponibilidadConExcepcion(Dato, Excepcion, Columna, Tabla);
        }

        public DataTable ObtenerTabla(string columna, string tabla)
        {
            DataTable dt = Data.LlenarTabla(columna, tabla);
            return dt;
        }

        public DataTable ObtenerTabla(string columna, string tabla, string condicion)
        {
            DataTable dt = Data.LlenarTabla(columna, tabla, condicion);
            return dt;
        }
    }
}
