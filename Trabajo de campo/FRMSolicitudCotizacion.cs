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
    }
}
