using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class Libro_C
    {
        public string ISBN;
        public string Fecha;
        public string Hora;
        public string Autor;
        public string Nombre;
        public int Precio;
        public int Stock;
        public int MaxStock;
        public int MinStock;
        public bool Activo;

        public Libro_C(string iSBN, string fecha, string hora, string autor, string nombre, int precio, int stock, int maxStock, int minStock, bool activo)
        {
            ISBN = iSBN;
            Fecha = fecha;
            Hora = hora;
            Autor = autor;
            Nombre = nombre;
            Precio = precio;
            Stock = stock;
            MaxStock = maxStock;
            MinStock = minStock;
            Activo = activo;
        }
    }
}
