using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class ItemOrden
    {
        public string ISBN;
        public int CodOrdenCompra;
        public double Cotizacion;
        public int StockCompra;
        public int StockRecepcion;
        public DateTime FechaEntrega;
        public string CodFactura;

        public ItemOrden(string iSBN, int codOrdenCompra, double cotizacion, int stockCompra)
        {
            ISBN = iSBN;
            CodOrdenCompra = codOrdenCompra;
            Cotizacion = cotizacion;
            StockCompra = stockCompra;
        }

        public ItemOrden(string iSBN, int codOrdenCompra, double cotizacion, int stockCompra, int stockRecepcion, DateTime fechaEntrega, string codFactura)
        {
            ISBN = iSBN;
            CodOrdenCompra = codOrdenCompra;
            Cotizacion = cotizacion;
            StockCompra = stockCompra;
            StockRecepcion = stockRecepcion;
            FechaEntrega = fechaEntrega;
            CodFactura = codFactura;
        }
    }
}
