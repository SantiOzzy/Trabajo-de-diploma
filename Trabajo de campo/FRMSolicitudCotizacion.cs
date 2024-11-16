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
using BE;

namespace Trabajo_de_campo
{
    public partial class FRMSolicitudCotizacion : Form, IObserver
    {
        FRMUI parent;
        Negocios negocios = new Negocios();
        BLLSolicitudCotizacion NegociosSolicitud = new BLLSolicitudCotizacion();
        BLLEvento NegociosEvento = new BLLEvento();

        public FRMSolicitudCotizacion()
        {
            InitializeComponent();
            LanguageManager.ObtenerInstancia().Agregar(this);
        }

        public void ActualizarIdioma()
        {
            LanguageManager.ObtenerInstancia().CambiarIdiomaControles(this);
        }

        private void BTNPreRegistrar_Click(object sender, EventArgs e)
        {
            parent.FormPreRegistrarProveedor.Show();
        }

        private void BTNGenerarSolicitudCotizacion_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
            VaciarCampos();

            parent.FormPreRegistrarProveedor.Hide();
        }

        private void FRMSolicitudCotizacion_Load(object sender, EventArgs e)
        {
            parent = this.MdiParent as FRMUI;
        }

        private void FRMSolicitudCotizacion_VisibleChanged(object sender, EventArgs e)
        {
            VaciarCampos();

            RefrescarGrillas();
        }

        public void RefrescarGrillas()
        {
            DataTable dt = negocios.ObtenerTabla("ISBN, Autor, Nombre, Stock, MaxStock, MinStock", "Libro", "Stock < MinStock AND Activo = 1");
            dt = TraducirTablaProductos(dt);
            dataGridView1.DataSource = dt;

            dt = negocios.ObtenerTabla("CUIT, RazonSocial, Nombre, Email, NumTelefono", "Proveedor", "Activo = 1");
            dt = TraducirTablaProveedores(dt);
            dataGridView2.DataSource = dt;
        }

        void VaciarCampos()
        {
            textBox1.Text = "";
            textBox2.Text = "";

            listBox1.Items.Clear();
            listBox2.Items.Clear();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            bool LibroSeleccionado = false;
            try
            {
                foreach (var item in listBox1.Items)
                {
                    if (item.ToString().Substring(0, item.ToString().IndexOf(' ')) == dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString())
                    {
                        MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMSolicitudCotizacion.Etiquetas.LibroYaSeleccionado"));
                        LibroSeleccionado = true;
                    }
                }

                if (LibroSeleccionado == false)
                {
                    listBox1.Items.Add(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString() + " - " + dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() + " - " + dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString() + " - " + dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString() + " - " + dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString() + " - " + dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString());
                }

                dataGridView1.Rows[e.RowIndex].Selected = true;
            }
            catch (Exception ex) { }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            bool ProveedorSeleccionado = false;
            try
            {
                foreach (var item in listBox2.Items)
                {
                    if (item.ToString().Substring(0, item.ToString().IndexOf(' ')) == dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString())
                    {
                        MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMSolicitudCotizacion.Etiquetas.ProveedorYaSeleccionado"));
                        ProveedorSeleccionado = true;
                    }
                }

                if (ProveedorSeleccionado == false)
                {
                    listBox2.Items.Add(dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString() + " - " + dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString() + " - " + dataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString() + " - " + dataGridView2.Rows[e.RowIndex].Cells[3].Value.ToString() + " - " + dataGridView2.Rows[e.RowIndex].Cells[4].Value.ToString());
                }

                dataGridView2.Rows[e.RowIndex].Selected = true;
            }
            catch (Exception ex) { }
        }

        private void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            listBox1.Items.Remove(listBox1.SelectedItem);
        }

        private void listBox2_SelectedValueChanged(object sender, EventArgs e)
        {
            listBox2.Items.Remove(listBox2.SelectedItem);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = negocios.ObtenerTabla("ISBN, Autor, Nombre, Stock, MaxStock, MinStock", "Libro", $"Activo = 1 AND Stock < MinStock AND ISBN LIKE '{textBox1.Text}%'");
            dt = TraducirTablaProductos(dt);
            dataGridView1.DataSource = dt;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = negocios.ObtenerTabla("CUIT, RazonSocial, Nombre, Email, NumTelefono", "Proveedor", $"Activo = 1 AND CUIT LIKE '{textBox2.Text}%'");
            dt = TraducirTablaProveedores(dt);
            dataGridView2.DataSource = dt;
        }

        private void BTNGenerarSolicitud_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count == 0)
            {
                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMSolicitudCotizacion.Etiquetas.SeleccioneProductos"));
            }
            else if (listBox2.Items.Count == 0)
            {
                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMSolicitudCotizacion.Etiquetas.SeleccioneProveedores"));
            }
            else
            {
                SolicitudCotizacion sol = new SolicitudCotizacion(DateTime.Now);

                NegociosSolicitud.RegistrarSolicitudCotizacion(sol);

                int CodSol = NegociosSolicitud.ObtenerCodSolicitudCotizacion();

                foreach (DataGridViewRow dr in dataGridView1.Rows)
                {
                    sol.Items.Add(new ItemSolicitud(dr.Cells[0].Value.ToString(), CodSol));
                }
                NegociosSolicitud.RegistrarItems(sol);

                foreach (DataGridViewRow dr in dataGridView2.Rows)
                {
                    sol.Proveedores.Add(new Proveedor(dr.Cells[0].Value.ToString(), dr.Cells[1].Value.ToString(), dr.Cells[2].Value.ToString(), dr.Cells[3].Value.ToString(), dr.Cells[4].Value.ToString()));
                }
                NegociosSolicitud.RegistrarProveedores(sol, CodSol);

                NegociosEvento.RegistrarEvento(new Evento(SessionManager.ObtenerInstancia().ObtenerDatosUsuario().Username, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:ss"), "Compras", "Registro de solicitud de cotización", 3));

                GenerarReportePDF();

                parent.FormBitacoraEventos.Actualizar();

                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMSolicitudCotizacion.Etiquetas.SolicitudGenerada"));

                this.Hide();

                VaciarCampos();
            }
        }

        DataTable TraducirTablaProductos(DataTable dt)
        {
            dt.Columns[0].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.ISBN");
            dt.Columns[1].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Autor");
            dt.Columns[2].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Nombre");
            dt.Columns[3].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Stock");
            dt.Columns[4].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.MaxStock");
            dt.Columns[5].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.MinStock");

            return dt;
        }

        DataTable TraducirTablaProveedores(DataTable dt)
        {

            dt.Columns[0].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.CUIT");
            dt.Columns[1].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.RazonSocial");
            dt.Columns[2].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Nombre");
            dt.Columns[3].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Email");
            dt.Columns[4].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.NumTelefono");

            return dt;
        }

        void GenerarReportePDF()
        {
            DataTable ProductosSolicitud = negocios.ObtenerTabla("Libro.ISBN, Autor, Nombre", "Libro INNER JOIN ItemSolicitud ON Libro.ISBN = ItemSolicitud.ISBN", $"CodSolicitud = {NegociosSolicitud.ObtenerCodSolicitudCotizacion()}");
            string codSolicitud = NegociosSolicitud.ObtenerCodSolicitudCotizacion().ToString();

            foreach (DataGridViewRow dr in dataGridView2.Rows)
            {
                DataTable dt = negocios.ObtenerTabla("CUIT, RazonSocial, Nombre, Email, NumTelefono", "Proveedor", $"CUIT = {dr.Cells[0].Value}");
                Proveedor prov = new Proveedor(dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString(), dt.Rows[0][2].ToString(), dt.Rows[0][3].ToString(), dt.Rows[0][4].ToString());

                try
                {
                    ReportesPDF.ReporteSolicitud(ProductosSolicitud, codSolicitud, new SolicitudCotizacion(DateTime.Now), prov);

                    NegociosEvento.RegistrarEvento(new Evento(SessionManager.ObtenerInstancia().ObtenerDatosUsuario().Username, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:ss"), "Ventas", "Generación de reporte de solicitud de cotización", 5));

                    parent.FormBitacoraEventos.Actualizar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
