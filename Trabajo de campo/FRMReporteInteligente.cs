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
            dataGridView1.DataSource = null;

            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    dt = negocios.ObtenerTablaConsulta("SELECT Autor, COUNT(i.ISBN) AS TotalLibrosVendidos, SUM(f.PrecioTotal) AS TotalDineroGenerado FROM Libro l INNER JOIN Item i ON l.ISBN = i.ISBN INNER JOIN Factura f ON i.CodFactura = f.CodFactura GROUP BY Autor ORDER BY TotalDineroGenerado DESC");
                    break;
                case 1:
                    dt = negocios.ObtenerTablaConsulta("SELECT ISBN, Nombre, Stock, MinStock FROM Libro WHERE Stock < MinStock");
                    break;
                case 2:
                    dt = negocios.ObtenerTablaConsulta("SELECT c.Nombre, c.Email, MAX(f.Fecha) AS UltimaCompra FROM Cliente c LEFT JOIN Factura f ON c.DNI = f.DNI GROUP BY c.Nombre, c.Email HAVING MAX(f.Fecha) < DATEADD(MONTH, -6, GETDATE())");
                    break;
                case 3:
                    dt = negocios.ObtenerTablaConsulta("SELECT l.Nombre AS Libro, SUM(i.Cantidad) AS TotalLibrosVendidos FROM Libro l LEFT JOIN Item i ON l.ISBN = i.ISBN GROUP BY l.Nombre ORDER BY TotalLibrosVendidos ASC");
                    break;
                case 4:
                    dt = negocios.ObtenerTablaConsulta("SELECT c.Nombre, c.Apellido, SUM(f.PrecioTotal) AS TotalDineroGastado FROM Cliente c INNER JOIN Factura f ON c.DNI = f.DNI GROUP BY c.Nombre, c.Apellido ORDER BY TotalDineroGastado DESC");
                    break;
                case 5:
                    dt = negocios.ObtenerTablaConsulta("SELECT io.CodOrdenCompra, AVG(l.Precio) AS PromedioPrecio FROM ItemOrden io INNER JOIN Libro l ON io.ISBN = l.ISBN GROUP BY io.CodOrdenCompra");
                    break;
                case 6:
                    dt = negocios.ObtenerTablaConsulta("SELECT p.RazonSocial, COUNT(ps.CodSolicitud) AS TotalSolicitudes FROM Proveedor p INNER JOIN ProveedorSolicitud ps ON p.CUIT = ps.CUIT GROUP BY p.RazonSocial ORDER BY TotalSolicitudes DESC");
                    break;
                case 7:
                    dt = negocios.ObtenerTablaConsulta("SELECT p.RazonSocial, COUNT(oc.CodOrdenCompra) AS TotalOrdenes FROM Proveedor p INNER JOIN OrdenCompra oc ON p.CUIT = oc.CUIT GROUP BY p.RazonSocial ORDER BY TotalOrdenes DESC;");
                    break;
                case 8:
                    dt = negocios.ObtenerTablaConsulta("SELECT oc.CodOrdenCompra, p.CUIT, p.RazonSocial FROM OrdenCompra oc INNER JOIN ItemOrden io ON io.CodOrdenCompra = oc.CodOrdenCompra INNER JOIN Proveedor p ON p.CUIT = oc.CUIT WHERE io.FechaEntrega IS NULL GROUP BY oc.CodOrdenCompra, p.CUIT, p.RazonSocial");
                    break;
                default:
                    dt = null;
                    dataGridView1.DataSource = null;
                    break;

                //PARA COPIAR Y AGREGAR NUEVOS REPORTES
                //case :
                //    dt = negocios.ObtenerTablaConsulta("");
                //    break;
            }

            dt = TraducirTabla(dt);
            dataGridView1.DataSource = dt;
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
