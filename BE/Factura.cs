using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Factura
    {
        public DateTime Fecha;
        public Cobro Cobro;
        public int DNI;
        public List<Item> Items = new List<Item>();

        public Factura(DateTime fecha, Cobro cobro, int dNI)
        {
            Fecha = fecha;
            Cobro = cobro;
            DNI = dNI;
        }

        public Factura()
        {
            Cobro = new Cobro();
        }
    }
}
