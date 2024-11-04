using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using BE;
using DAL;
using Services;

namespace BLL
{
    public class BLLLibro
    {
        Datos Data = new Datos();
        Negocios negocios = new Negocios();
        DALLibro DataLibro = new DALLibro();
        BLLDV NegociosDV = new BLLDV();

        public void RegistrarLibro(Libro book)
        {
            Data.EjecutarComando("InsertarLibro", $"'{book.ISBN}', '{book.Autor}', '{book.Nombre}', {book.Precio}, {book.Stock}, {book.StockMaximo}, {book.StockMinimo}");

            NegociosDV.RecalcularDVTabla("Libro");
            NegociosDV.RecalcularDVTabla("Libro_C");
        }

        public void ModificarLibro(Libro book)
        {
            Data.EjecutarComando("ModificarLibro", $"'{book.ISBN}', '{book.Autor}', '{book.Nombre}', {book.Precio}, {book.Stock}, {book.StockMaximo}, {book.StockMinimo}");

            NegociosDV.RecalcularDVTabla("Libro");
            NegociosDV.RecalcularDVTabla("Libro_C");
        }

        public void DesactivarLibro(string ISBN)
        {
            DataLibro.DesactivacionLibro(ISBN, 0);

            NegociosDV.RecalcularDVTabla("Libro");
            NegociosDV.RecalcularDVTabla("Libro_C");
        }

        public void ActivarLibro(string ISBN)
        {
            DataLibro.DesactivacionLibro(ISBN, 1);

            NegociosDV.RecalcularDVTabla("Libro");
            NegociosDV.RecalcularDVTabla("Libro_C");
        }

        public bool RevisarDesactivado(string ISBN)
        {
            return DataLibro.RevisarDesactivado(ISBN, "ISBN, Activo");
        }

        public void ValidarLibroParaVenta(string Cantidad, int Stock)
        {
            bool ValidarNumero = Cantidad.All(char.IsDigit);

            if (Cantidad.Length >= 11)
            {
                Cantidad = Cantidad.Substring(0, 11);
            }

            if (ValidarNumero == false || Cantidad == "" || Convert.ToInt64(Cantidad) > 2147483647)
            {
                throw new Exception(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMSeleccionarLibros.Etiquetas.NumeroInvalido"));
            }
            else if (Convert.ToInt32(Stock) < Convert.ToInt32(Cantidad))
            {
                throw new Exception(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMSeleccionarLibros.Etiquetas.SinStock"));
            }
        }

        public DataTable ActualizarTablaSeleccion()
        {
            DataTable dt = negocios.ObtenerTabla("*", "Libro", "Activo = 1");

            dt.Columns[0].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.ISBN");
            dt.Columns[1].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Autor");
            dt.Columns[2].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Nombre");
            dt.Columns[3].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Precio");
            dt.Columns[4].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Stock");
            dt.Columns[5].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Activo");
            dt.Columns[6].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.MaxStock");
            dt.Columns[7].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.MinStock");

            return dt;
        }
    }
}
