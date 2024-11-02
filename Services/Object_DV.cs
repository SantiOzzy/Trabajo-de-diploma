using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class Object_DV
    {
        public string Tabla;
        public string DVH;
        public string DVV;

        public Object_DV(string tabla, string dVH, string dVV)
        {
            Tabla = tabla;
            DVH = dVH;
            DVV = dVV;
        }
    }
}
