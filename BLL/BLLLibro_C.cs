using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services;
using DAL;

namespace BLL
{
    public class BLLLibro_C
    {
        Datos Data = new Datos();
        DALLibro_C DataLibro_C = new DALLibro_C();
        BLLDV NegociosDV = new BLLDV();

        public void ActivarEstado(Libro_C lc)
        {
            Data.EjecutarComando("ActivarLibro_C", $"{lc.ISBN}, {lc.Fecha}, {lc.Hora}, {lc.Autor}, {lc.Nombre}, {lc.Precio}, {lc.Stock}");

            NegociosDV.RecalcularDVTabla("Libro_C");
        }
    }
}
