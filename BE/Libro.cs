using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Libro
    {
        public string ISBN;
        public string Autor;
        public string Nombre;
        public int Precio;
        public int Stock;
        public int StockMaximo;
        public int StockMinimo;

        public Libro(string iSBN, string autor, string nombre, int precio, int stock, int stockMaximo, int stockMinimo)
        {
            ISBN = iSBN;
            Autor = autor;
            Nombre = nombre;
            Precio = precio;
            Stock = stock;
            StockMaximo = stockMaximo;
            StockMinimo = stockMinimo;
        }
    }
}
