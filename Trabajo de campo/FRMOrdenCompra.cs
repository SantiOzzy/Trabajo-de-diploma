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
    public partial class FRMOrdenCompra : Form, IObserver
    {
        FRMUI parent;
        public FRMOrdenCompra()
        {
            InitializeComponent();
            LanguageManager.ObtenerInstancia().Agregar(this);
        }

        public void ActualizarIdioma()
        {
            LanguageManager.ObtenerInstancia().CambiarIdiomaControles(this);
        }

        private void FRMOrdenCompra_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }

            parent.FormRegistrarProveedor.Hide();
            parent.FormPagarCompra.Hide();
        }

        private void FRMOrdenCompra_Load(object sender, EventArgs e)
        {
            parent = this.MdiParent as FRMUI;
        }

        private void BTNRegistrarProveedor_Click(object sender, EventArgs e)
        {
            parent.FormRegistrarProveedor.Show();
        }

        private void BTNRegistrarPago_Click(object sender, EventArgs e)
        {
            parent.FormPagarCompra.Show();
        }
    }
}
