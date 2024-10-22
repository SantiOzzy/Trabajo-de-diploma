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
        public int PrecioTotal;
        public string CodFactura;
        public List<ItemOrden> Items = new List<ItemOrden>();

        public OrdenCompra()
        {
        }

        public OrdenCompra(string codFactura)
        {
            CodFactura = codFactura;
        }

        public OrdenCompra(string cUIT, DateTime fechaCreacion, int precioTotal, string codFactura)
        {
            CUIT = cUIT;
            FechaCreacion = fechaCreacion;
            PrecioTotal = precioTotal;
            CodFactura = codFactura;
        }
    }
}
