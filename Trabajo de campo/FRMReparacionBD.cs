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
using Services;

namespace Trabajo_de_campo
{
    public partial class FRMReparacionBD : Form, IObserver
    {
        BLLDV NegociosDV = new BLLDV();
        public string TablaError;

        public FRMReparacionBD()
        {
            InitializeComponent();
            LanguageManager.ObtenerInstancia().Agregar(this);
        }

        public void ActualizarIdioma()
        {
            LanguageManager.ObtenerInstancia().CambiarIdiomaControles(this);

            label3.Text = TablaError;
        }

        private void BTNSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BTNRecalcular_Click(object sender, EventArgs e)
        {
            NegociosDV.RecalcularBD();

            this.Hide();

            SessionManager.ObtenerInstancia().CerrarSesion();

            MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMReparacionBD.Etiquetas.DVRecalculado"));
        }

        private void BTNRestaurar_Click(object sender, EventArgs e)
        {
            FRMUI parent = this.MdiParent as FRMUI;
            parent.FormRespaldos.Show();
        }

        private void FRMReparacionBD_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }

            SessionManager.ObtenerInstancia().CerrarSesion();
        }

        private void FRMReparacionBD_VisibleChanged(object sender, EventArgs e)
        {
            label3.Text = TablaError;
        }
    }
}
