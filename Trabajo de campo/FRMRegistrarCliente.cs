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
            MailAddress Correo;
            bool MailValido = false;
            bool NumeroTelefonoValido = Regex.IsMatch(textBox5.Text, @"^\s*(?:\+?(\d{1,3}))?[-. (]*(\d{3})[-. )]*(\d{3})[-. ]*(\d{4})(?: *x(\d+))?\s*$");

            if (textBox4.Text != "")
            {
                try
                {
                    Correo = new MailAddress(textBox4.Text);
                    MailValido = true;
                }
                catch (FormatException) { }
            }

            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "")
            {
                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMRegistrarCliente.Etiquetas.LlenarCampos"));
            }
            else if (negocios.RevisarDisponibilidad(numericUpDown1.Value.ToString(), "DNI", "Cliente"))
            {
                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMRegistrarCliente.Etiquetas.DNIOcupado"));
            }
            else if (MailValido == false)
            {
                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMRegistrarCliente.Etiquetas.CorreoInvalido"));
            }
            else if (negocios.RevisarDisponibilidad(textBox4.Text, "Email", "Cliente"))
            {
                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMRegistrarCliente.Etiquetas.CorreoOcupado"));
            }
            else if (NumeroTelefonoValido == false)
            {
                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMRegistrarCliente.Etiquetas.NumTelInvalido"));
            }
            else
            {
                Cliente cliente = new Cliente(Convert.ToInt32(numericUpDown1.Value), textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text);

                NegociosCliente.RegistrarCliente(cliente);

                NegociosEvento.RegistrarEvento(new Evento(SessionManager.ObtenerInstancia().ObtenerDatosUsuario().Username, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:ss"), "Cliente", "Registro de cliente", 4));
                FRMUI parent = this.MdiParent as FRMUI;
                parent.FormBitacoraEventos.Actualizar();

                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMRegistrarCliente.Etiquetas.ClienteRegistrado"));

                this.Hide();

                numericUpDown1.Value = numericUpDown1.Minimum;
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
            }
        }
    }
}
