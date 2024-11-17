using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Services;
using BLL;

namespace Trabajo_de_campo
{
    public partial class FRMReporteInteligente : Form, IObserver
    {
        Negocios negocios = new Negocios();
        DataTable dt = null;
        public FRMReporteInteligente()
        {
            InitializeComponent();
        }

        public void ActualizarIdioma()
        {
            LanguageManager.ObtenerInstancia().CambiarIdiomaControles(this);
        }

        private void FRMReporteInteligente_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void BTNGenerarReporte_Click(object sender, EventArgs e)
        {
            try
            {
                ReportesPDF.ReporteInteligente(dt, comboBox1.Text);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    dataGridView1.DataSource = null;
                    dt = negocios.ObtenerTablaConsulta("SELECT Autor, COUNT(i.ISBN) AS TotalLibrosVendidos, SUM(f.PrecioTotal) AS TotalDineroGenerado FROM Libro l INNER JOIN Item i ON l.ISBN = i.ISBN INNER JOIN Factura f ON i.CodFactura = f.CodFactura GROUP BY Autor ORDER BY TotalDineroGenerado DESC;");
                    dt = TraducirTabla(dt);
                    dataGridView1.DataSource = dt;
                    break;
                case 1:
                    dataGridView1.DataSource = null;
                    dt = negocios.ObtenerTablaConsulta("SELECT ISBN, Nombre, Stock, MinStock FROM Libro WHERE Stock < MinStock");
                    dt = TraducirTabla(dt);
                    dataGridView1.DataSource = dt;
                    break;
                default:
                    dt = null;
                    dataGridView1.DataSource = null;
                    break;

                //PARA COPIAR Y AGREGAR NUEVOS REPORTES
                //case :
                //    dt = negocios.ObtenerTablaConsulta("");
                //    dataGridView1.DataSource = dt;
                //    break;
            }
        }

        DataTable TraducirTabla(DataTable dt)
        {
            foreach(DataColumn c in dt.Columns)
            {
                c.ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto($"dgv.{c.ColumnName}");
            }

            return dt;
        }
    }
}
