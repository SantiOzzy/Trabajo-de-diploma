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

namespace Trabajo_de_campo
{
    public partial class FRMCambiarIdioma : Form, IObserver
    {
        public FRMCambiarIdioma()
        {
            InitializeComponent();
            LanguageManager.ObtenerInstancia().Agregar(this);
        }

        public void ActualizarIdioma()
        {
            LanguageManager.ObtenerInstancia().CambiarIdiomaControles(this);
        }

        private void BTNCambiarIdioma_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "")
            {
                SessionManager.ObtenerInstancia().IdiomaActual = comboBox1.Text;
                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMCambiarIdioma.Etiquetas.IdiomaCambiado") + comboBox1.Text);
                this.Hide();
            }
            else
            {
                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMCambiarIdioma.Etiquetas.SeleccionarIdioma"));
            }
        }

        private void FRMCambiarIdioma_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void FRMCambiarIdioma_VisibleChanged(object sender, EventArgs e)
        {
            comboBox1.SelectedItem = null;
        }
    }
}
