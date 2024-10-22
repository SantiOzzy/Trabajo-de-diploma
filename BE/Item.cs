using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Item
    {
        public int Cantidad;
        public int CodFactura;
        public string ISBN;

        public Item(int codFactura, string iSBN, int cantidad)
        {
            Cantidad = cantidad;
            CodFactura = codFactura;
            ISBN = iSBN;
        }
    }
}
