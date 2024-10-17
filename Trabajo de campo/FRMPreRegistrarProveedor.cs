using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Net.Mail;
using Services;
using BLL;
using BE;

namespace Trabajo_de_campo
{
    public partial class FRMPreRegistrarProveedor : Form, IObserver
    {
        Negocios negocios = new Negocios();
        BLLProveedor NegociosProveedor = new BLLProveedor();
        BLLEvento NegociosEvento = new BLLEvento();

        public FRMPreRegistrarProveedor()
        {
            InitializeComponent();
            LanguageManager.ObtenerInstancia().Agregar(this);
        }

        public void ActualizarIdioma()
        {
            LanguageManager.ObtenerInstancia().CambiarIdiomaControles(this);
        }

        private void FRMPreRegistrarProveedor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void BTNPreRegistrarProveedor_Click(object sender, EventArgs e)
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
            else if (textBox1.Text.Length < 11)
            {
                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMPreRegistrarProveedor.Etiquetas.11Digitos"));
            }
            else if (negocios.RevisarDisponibilidad(textBox1.Text, "CUIT", "Proveedor"))
            {
                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMPreRegistrarProveedor.Etiquetas.CUITOcupado"));
            }
            else if (negocios.RevisarDisponibilidad(textBox2.Text, "RazonSocial", "Proveedor"))
            {
                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMPreRegistrarProveedor.Etiquetas.RazonSocialOcupado"));
            }
            else if (MailValido == false)
            {
                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMRegistrarCliente.Etiquetas.CorreoInvalido"));
            }
            else if (negocios.RevisarDisponibilidad(textBox4.Text, "Email", "Proveedor"))
            {
                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMRegistrarCliente.Etiquetas.CorreoOcupado"));
            }
            else if (NumeroTelefonoValido == false)
            {
                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMRegistrarCliente.Etiquetas.NumTelInvalido"));
            }
            else
            {
                Proveedor prov = new Proveedor(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text);

                NegociosProveedor.PreRegistrarProveedor(prov);

                //NegociosEvento.RegistrarEvento(new Evento(SessionManager.ObtenerInstancia().ObtenerDatosUsuario().Username, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:ss"), "Proveedor", "Pre-registro de proveedor", 4));
                FRMUI parent = this.MdiParent as FRMUI;
                parent.FormBitacoraEventos.Actualizar();
                parent.FormSolicitudCotizacion.RefrescarGrillas();

                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMPreRegistrarProveedor.Etiquetas.ProveedorPreRegistrado"));

                this.Hide();

                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
            }
        }
    }
}
