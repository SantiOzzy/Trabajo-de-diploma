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
using BE;
using BLL;

namespace Trabajo_de_campo
{
    public partial class FRMBitacoraCambios : Form, IObserver
    {
        Negocios negocios = new Negocios();
        BLLLibro_C NegociosLibro_C = new BLLLibro_C();
        BLLEvento NegociosEvento = new BLLEvento();
        FRMUI parent;

        public FRMBitacoraCambios()
        {
            InitializeComponent();
            LanguageManager.ObtenerInstancia().Agregar(this);
        }

        public void ActualizarIdioma()
        {
            LanguageManager.ObtenerInstancia().CambiarIdiomaControles(this);

            Actualizar();
        }

        private void VaciarCampos()
        {
            CBIsbn.Text = "";
            CBNombre.Text = "";
            dateTimePicker1.Value = dateTimePicker1.MinDate;
            dateTimePicker2.Value = dateTimePicker2.MinDate;
        }

        private void BTNLimpiar_Click(object sender, EventArgs e)
        {
            VaciarCampos();
        }

        private void FRMBitacoraCambios_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        public void Actualizar()
        {
            dataGridView1.DataSource = NegociosLibro_C.ObtenerCambios(CBIsbn.Text, dateTimePicker1.Value, dateTimePicker2.Value, CBNombre.Text, dateTimePicker1.MinDate, dateTimePicker2.MinDate);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dataGridView1.Rows[e.RowIndex].Selected = true;
            }
            catch (Exception) { }
        }

        public void LlenarCombobox()
        {
            CBIsbn.DataSource = negocios.ObtenerTabla("ISBN", "Libro");
            CBIsbn.DisplayMember = "ISBN";
            CBIsbn.ValueMember = "ISBN";
        }

        private void CBIsbn_TextChanged(object sender, EventArgs e)
        {
            Actualizar();
        }

        private void CBNombre_TextChanged(object sender, EventArgs e)
        {
            Actualizar();
        }

        private void dateTimePicker1_CloseUp(object sender, EventArgs e)
        {
            ValidarFechas();
        }

        private void dateTimePicker2_CloseUp(object sender, EventArgs e)
        {
            ValidarFechas();
        }

        private void ValidarFechas()
        {
            try
            {
                NegociosLibro_C.ValidarFechas(dateTimePicker1.Value, dateTimePicker2.Value);

                Actualizar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                dataGridView1.DataSource = null;
            }
        }

        private void BTNActivar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMBitacoraCambios.Etiquetas.SeleccionarCambio"));
            }
            else
            {
                try
                {
                    NegociosLibro_C.ActivarLibro_C(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString(), dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[1].Value.ToString(), dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[2].Value.ToString(), dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[3].Value.ToString(), dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[4].Value.ToString(), Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[5].Value), Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[6].Value), Convert.ToBoolean(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[7].Value), Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[8].Value), Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[9].Value), Convert.ToBoolean(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[10].Value));

                    Actualizar();

                    parent.FormBitacoraEventos.Actualizar();

                    MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMBitacoraCambios.Etiquetas.EstadoActivado"));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void FRMBitacoraCambios_VisibleChanged(object sender, EventArgs e)
        {
            LlenarCombobox();
            VaciarCampos();
            Actualizar();
        }

        private void FRMBitacoraCambios_Load(object sender, EventArgs e)
        {
            parent = this.MdiParent as FRMUI;
        }
    }
}
