using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Windows.Forms;
using BE;
using BLL;
using Services;

namespace Trabajo_de_campo
{
    public partial class FRMRegistrarCliente : Form, IObserver
    {
        Negocios negocios = new Negocios();
        BLLCliente NegociosCliente = new BLLCliente();
        BLLEvento NegociosEvento = new BLLEvento();
        public FRMRegistrarCliente()
        {
            InitializeComponent();
            LanguageManager.ObtenerInstancia().Agregar(this);
        }

        public void ActualizarIdioma()
        {
            LanguageManager.ObtenerInstancia().CambiarIdiomaControles(this);
        }

        private void FRMRegistrarCliente_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void BTNRegistrarCliente_Click(object sender, EventArgs e)
        {
            try
            {
                NegociosCliente.RegistrarClienteFormulario(numericUpDown1.Value.ToString(), textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text);
                FRMUI parent = this.MdiParent as FRMUI;
                parent.FormBitacoraEventos.Actualizar();

                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMRegistrarCliente.Etiquetas.ClienteRegistrado"));

                this.Hide();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void VaciarCampos()
        {
            numericUpDown1.Value = numericUpDown1.Minimum;
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
        }

        private void FRMRegistrarCliente_VisibleChanged(object sender, EventArgs e)
        {
            VaciarCampos();
        }
    }
}
