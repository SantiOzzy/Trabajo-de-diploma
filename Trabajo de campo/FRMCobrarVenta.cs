using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using BE;
using Services;

namespace Trabajo_de_campo
{
    public partial class FRMCobrarVenta : Form, IObserver
    {
        Negocios negocios = new Negocios();
        BLLFactura NegociosFactura = new BLLFactura();
        BLLEvento NegociosEvento = new BLLEvento();
        BLLCliente NegociosCliente = new BLLCliente();
        CryptoManager Encriptar = new CryptoManager();
        FRMUI parent;

        public FRMCobrarVenta()
        {
            InitializeComponent();
            LanguageManager.ObtenerInstancia().Agregar(this);
        }

        public void ActualizarIdioma()
        {
            LanguageManager.ObtenerInstancia().CambiarIdiomaControles(this);
        }

        private void FRMCobrarVenta_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void FRMCobrarVenta_VisibleChanged(object sender, EventArgs e)
        {
            VaciarCampos();
        }

        private void BTNCobrarVenta_Click(object sender, EventArgs e)
        {
            if (negocios.RevisarDisponibilidad(numericUpDown1.Value.ToString(), "DNI", "Cliente") == false)
            {
                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMCobrarVenta.Etiquetas.DNINoExiste"));

                parent.FormRegistrarCliente.Show();
            }
            else if (NegociosCliente.RevisarDesactivado(numericUpDown1.Value.ToString()) == false)
            {
                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMCobrarVenta.Etiquetas.ClienteDesactivado"));
            }
            else if (comboBox1.Text == "")
            {
                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMCobrarVenta.Etiquetas.ElegirMedioPago"));
            }
            else if (comboBox1.Text != "Efectivo" && (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == ""))
            {
                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMCobrarVenta.Etiquetas.LlenarCampos"));
            }
            else
            {
                parent.fact.DNI = Convert.ToInt32(numericUpDown1.Value);
                parent.cobro.MetodoPago = comboBox1.Text;

                parent.FormGenerarFactura.label1.Text = LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGenerarFactura.label1") + numericUpDown1.Value.ToString();
                parent.FormGenerarFactura.label2.Text = LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGenerarFactura.label2") + comboBox1.Text;

                if (comboBox1.Text != "Efectivo")
                {
                    parent.cobro.Banco = textBox1.Text;
                    parent.cobro.MarcaTarjeta = textBox2.Text;
                    parent.cobro.TipoTarjeta = textBox3.Text;

                    parent.FormGenerarFactura.label3.Text = LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGenerarFactura.label3") + textBox1.Text;
                    parent.FormGenerarFactura.label4.Text = LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGenerarFactura.label4") + textBox2.Text;
                    parent.FormGenerarFactura.label5.Text = LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGenerarFactura.label5") + textBox3.Text;
                }
                else
                {
                    parent.cobro.Banco = null;
                    parent.cobro.MarcaTarjeta = null;
                    parent.cobro.TipoTarjeta = null;

                    parent.FormGenerarFactura.label3.Text = LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGenerarFactura.label3") + "-";
                    parent.FormGenerarFactura.label4.Text = LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGenerarFactura.label4") + "-";
                    parent.FormGenerarFactura.label5.Text = LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGenerarFactura.label5") + "-";
                }

                VaciarCampos();

                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMCobrarVenta.Etiquetas.CobroRealizado"));

                this.Hide();
            }
        }

        private void VaciarCampos()
        {
            numericUpDown1.Value = numericUpDown1.Minimum;
            comboBox1.SelectedItem = null;
            comboBox1.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";

            foreach (TextBox tb in this.Controls.OfType<TextBox>())
            {
                tb.Enabled = false;
            }
        }

        private void FRMCobrarVenta_Load(object sender, EventArgs e)
        {
            parent = this.MdiParent as FRMUI;
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Efectivo")
            {
                foreach (TextBox tb in this.Controls.OfType<TextBox>())
                {
                    tb.Enabled = false;
                }
            }
            else
            {
                foreach (TextBox tb in this.Controls.OfType<TextBox>())
                {
                    tb.Enabled = true;
                }
            }
        }
    }
}
