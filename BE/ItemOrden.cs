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
        public int Cotizacion;
        public int StockCompra;
        public int StockRecepcion;
        public DateTime FechaEntrega;

        public ItemOrden(string iSBN, int codOrdenCompra, int cotizacion, int stockCompra)
        {
            ISBN = iSBN;
            CodOrdenCompra = codOrdenCompra;
            Cotizacion = cotizacion;
            StockCompra = stockCompra;
        }

        public ItemOrden(string iSBN, int codOrdenCompra, int cotizacion, int stockCompra, int stockRecepcion, DateTime fechaEntrega)
        {
            ISBN = iSBN;
            CodOrdenCompra = codOrdenCompra;
            Cotizacion = cotizacion;
            StockCompra = stockCompra;
            StockRecepcion = stockRecepcion;
            FechaEntrega = fechaEntrega;
        }
    }
}
