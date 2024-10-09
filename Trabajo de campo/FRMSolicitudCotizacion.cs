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

            parent.FormPreRegistrarProveedor.Hide();
        }

        private void FRMSolicitudCotizacion_Load(object sender, EventArgs e)
        {
            parent = this.MdiParent as FRMUI;
        }

        private void FRMSolicitudCotizacion_VisibleChanged(object sender, EventArgs e)
        {
            DataTable dt = negocios.ObtenerTabla("ISBN, Autor, Nombre, Stock, MaxStock, MinStock", "Libro", "Stock < MinStock");
            dataGridView1.DataSource = dt;

            //dt = negocios.ObtenerTabla("*", "Proveedor");
            //dataGridView2.DataSource = dt;
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
                        MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMSeleccionarLibros.Etiquetas.LibroYaSeleccionado"));
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
            try
            {
                dataGridView2.Rows[e.RowIndex].Selected = true;
            }
            catch (Exception ex) { }
        }

        private void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            listBox1.Items.Remove(listBox1.SelectedItem);
        }
    }
}
