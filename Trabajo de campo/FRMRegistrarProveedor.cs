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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Trabajo_de_campo
{
    public partial class FRMRegistrarProveedor : Form, IObserver
    {
        Negocios negocios = new Negocios();
        BLLProveedor NegociosProveedor = new BLLProveedor();
        BLLEvento NegociosEvento = new BLLEvento();
        public FRMRegistrarProveedor()
        {
            InitializeComponent();
            LanguageManager.ObtenerInstancia().Agregar(this);
        }

        public void ActualizarIdioma()
        {
            LanguageManager.ObtenerInstancia().CambiarIdiomaControles(this);
        }

        private void FRMRegistrarProveedor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void BTNRegistrarProveedor_Click(object sender, EventArgs e)
        {
            if (CBCuit.Text == "" || textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMRegistrarProveedor.Etiquetas.LlenarCampos"));
            }
            else if (negocios.RevisarDisponibilidad(textBox2.Text, "CuentaBancaria", "Proveedor"))
            {
                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMRegistrarProveedor.Etiquetas.CuentaBancariaOcupado"));
            }
            else
            {
                Proveedor prov = new Proveedor(CBCuit.Text, textBox1.Text, textBox2.Text);

                NegociosProveedor.RegistrarProveedor(prov);

                NegociosEvento.RegistrarEvento(new Evento(SessionManager.ObtenerInstancia().ObtenerDatosUsuario().Username, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:ss"), "Proveedor", "Registro de proveedor", 4));
                
                FRMUI parent = this.MdiParent as FRMUI;
                parent.FormBitacoraEventos.Actualizar();
                parent.FormOrdenCompra.RefrescarGrillas();

                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMRegistrarProveedor.Etiquetas.ProveedorRegistrado"));

                this.Hide();

                textBox1.Text = "";
                textBox2.Text = "";
            }
        }

        private void FRMRegistrarProveedor_VisibleChanged(object sender, EventArgs e)
        {
            CBCuit.DataSource = negocios.ObtenerTabla("CUIT", "Proveedor", "Direccion IS NULL");
            CBCuit.DisplayMember = "CUIT";
            CBCuit.ValueMember = "CUIT";
        }
    }
}
