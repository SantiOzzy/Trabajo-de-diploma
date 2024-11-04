using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Services;
using DAL;

namespace BLL
{
    public class BLLLibro_C
    {
        Datos Data = new Datos();
        Negocios negocios = new Negocios();
        DALLibro_C DataLibro_C = new DALLibro_C();
        BLLEvento NegociosEvento = new BLLEvento();
        BLLDV NegociosDV = new BLLDV();

        public void ActivarEstado(Libro_C lc)
        {
            Data.EjecutarComando("ActualizarEstadoLibro", $"'{lc.ISBN}', '{lc.Autor}', '{lc.Nombre}', {lc.Precio}, {lc.Stock}, {lc.MaxStock}, {lc.MinStock}, {lc.Activo}");

            NegociosDV.RecalcularDVTabla("Libro");
            NegociosDV.RecalcularDVTabla("Libro_C");
        }

        public DataTable ObtenerCambios(string ISBN, DateTime FechaInicio, DateTime FechaFin, string Nombre, DateTime FechaInicioDefault, DateTime FechaFinDefault)
        {
            DataTable dt;
            if (FechaInicio == FechaInicioDefault && FechaFin == FechaFinDefault && ISBN == "" && Nombre == "")
            {
                dt = negocios.ObtenerTabla("*", "Libro_C", $"CONVERT(date,Fecha) >= '{DateTime.Now.AddMonths(-1).ToString("yyyy-MM-ddTHH:mm:ss.fff")}'");
            }
            else
            {
                dt = negocios.ObtenerTabla("*", "Libro_C", $"ISBN LIKE '{ISBN}%' AND CONVERT(date,Fecha) >= '{FechaInicio.ToString("yyyy-MM-ddTHH:mm:ss.fff")}' AND CONVERT(date,Fecha) <= '{FechaFin.ToString("yyyy-MM-ddTHH:mm:ss.fff")}' AND Nombre LIKE '{Nombre}%' ORDER BY CAST(Fecha + ' ' + Hora AS date) DESC");
            }

            dt.Columns[0].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.ISBN");
            dt.Columns[1].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Fecha");
            dt.Columns[2].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Hora");
            dt.Columns[3].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Autor");
            dt.Columns[4].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Nombre");
            dt.Columns[5].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Precio");
            dt.Columns[6].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Stock");
            dt.Columns[7].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.EstadoActual");
            dt.Columns[8].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.MaxStock");
            dt.Columns[9].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.MinStock");
            return dt;
        }

        public void ValidarFechas(DateTime FechaInicial, DateTime FechaFinal)
        {
            if (FechaInicial > DateTime.Now)
            {
                throw new Exception("FRMBitacoraCambios.Etiquetas.FechaMayorActual");
            }
            else if (FechaInicial > FechaFinal)
            {
                throw new Exception("FRMBitacoraCambios.Etiquetas.FechaMayorFinal");
            }
        }

        public void ActivarLibro_C(string ISBN, string Fecha, string Hora, string Autor, string Nombre, int Precio, int Stock, bool EstadoActual, int MaxStock, int MinStock, bool Activo)
        {
            if (EstadoActual)
            {
                throw new Exception(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMBitacoraCambios.Etiquetas.EstadoEnUso"));
            }
            else
            {
                //NegociosLibro_C.ActivarEstado(new Libro_C(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString(), dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[1].Value.ToString(), dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[2].Value.ToString(), dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[3].Value.ToString(), dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[4].Value.ToString(), Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[5].Value), Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[6].Value)));
                //ActivarEstado(new Libro_C("'" + ISBN + "'", "'" + Fecha + "'", "'" + Hora + "'", "'" + Autor + "'", "'" + Nombre + "'", Precio, Stock));
                ActivarEstado(new Libro_C(ISBN, Fecha, Hora, Autor, Nombre, Precio, Stock, MaxStock, MinStock, Activo));

                NegociosEvento.RegistrarEvento(new Evento(SessionManager.ObtenerInstancia().ObtenerDatosUsuario().Username, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:ss"), "Libros", "Actualización de estado de libro", 4));
            }
        }
    }
}
