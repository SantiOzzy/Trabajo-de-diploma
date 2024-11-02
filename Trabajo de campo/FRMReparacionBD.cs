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
    public partial class FRMReparacionBD : Form
    {
        BLLDV NegociosDV = new BLLDV();
        public FRMReparacionBD()
        {
            InitializeComponent();
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

            MessageBox.Show("Dígito verificador recalculado");
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
    }
}
