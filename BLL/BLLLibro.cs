using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;

namespace BLL
{
    public class BLLLibro
    {
        Datos Data = new Datos();
        DALLibro DataLibro = new DALLibro();

        public void RegistrarLibro(Libro book)
        {
            Data.EjecutarComando("InsertarLibro", $"'{book.ISBN}', '{book.Autor}', '{book.Nombre}', {book.Precio}, {book.Stock}, {book.StockMaximo}, {book.StockMinimo}");
        }

        public void ModificarLibro(Libro book)
        {
            Data.EjecutarComando("ModificarLibro", $"'{book.ISBN}', '{book.Autor}', '{book.Nombre}', {book.Precio}, {book.Stock}, {book.StockMaximo}, {book.StockMinimo}");
        }

        public void DesactivarLibro(string ISBN)
        {
            DataLibro.DesactivacionLibro(ISBN, 0);
        }

        public void ActivarLibro(string ISBN)
        {
            DataLibro.DesactivacionLibro(ISBN, 1);
        }

        public bool RevisarDesactivado(string ISBN)
        {
            return DataLibro.RevisarDesactivado(ISBN, "ISBN, Activo");
        }
    }
}
