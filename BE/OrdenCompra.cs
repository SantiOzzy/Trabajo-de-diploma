using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class OrdenCompra
    {
        public string CUIT;
        public DateTime FechaCreacion;
        public double PrecioTotal;
        public string NumTransaccion;
        public List<ItemOrden> Items = new List<ItemOrden>();

        public OrdenCompra()
        {
        }

        public OrdenCompra(string numTransaccion)
        {
            NumTransaccion = numTransaccion;
        }

        public OrdenCompra(string cUIT, DateTime fechaCreacion, double precioTotal, string numTransaccion)
        {
            CUIT = cUIT;
            FechaCreacion = fechaCreacion;
            PrecioTotal = precioTotal;
            NumTransaccion = numTransaccion;
        }
    }
}
