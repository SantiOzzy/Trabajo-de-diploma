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
    public partial class FRMGestionDeProveedores : Form, IObserver
    {
        Negocios negocios = new Negocios();
        BLLProveedor NegociosProveedor = new BLLProveedor();
        BLLEvento NegociosEvento = new BLLEvento();

        public FRMGestionDeProveedores()
        {
            InitializeComponent();
            LanguageManager.ObtenerInstancia().Agregar(this);
        }

        public void ActualizarIdioma()
        {
            LanguageManager.ObtenerInstancia().CambiarIdiomaControles(this);

            if (ObtenerCondicionesQuery().Length > 5)
            {
                DataTable dt = negocios.ObtenerTabla("CUIT, RazonSocial, Nombre, Email, NumTelefono, Direccion, CuentaBancaria, Activo", "Proveedor", ObtenerCondicionesQuery());
                dt = TraducirTabla(dt);
                dataGridView1.DataSource = dt;
            }
            else
            {
                DataTable dt = negocios.ObtenerTabla("CUIT, RazonSocial, Nombre, Email, NumTelefono, Direccion, CuentaBancaria, Activo", "Proveedor");
                dt = TraducirTabla(dt);
                dataGridView1.DataSource = dt;
            }

            ContarProveedores();

            if (BTNAñadir.Enabled == false)
            {
                textBox8.Text = LanguageManager.ObtenerInstancia().ObtenerTexto("Etiquetas.ModoAñadir");
            }
            else if (BTNModificar.Enabled == false)
            {
                textBox8.Text = LanguageManager.ObtenerInstancia().ObtenerTexto("Etiquetas.ModoModificar");
            }
            else if (BTNEliminar.Enabled == false)
            {
                textBox8.Text = LanguageManager.ObtenerInstancia().ObtenerTexto("Etiquetas.ModoEliminar");
            }
            else
            {
                textBox8.Text = LanguageManager.ObtenerInstancia().ObtenerTexto("Etiquetas.ModoConsulta");
            }
        }

        private void FRMGestionDeProveedores_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }

            LimpiarCampos();

            DataTable dt = negocios.ObtenerTabla("CUIT, RazonSocial, Nombre, Email, NumTelefono, Direccion, CuentaBancaria, Activo", "Proveedor");
            dt = TraducirTabla(dt);
            dataGridView1.DataSource = dt;
        }

        private void FRMGestionDeProveedores_VisibleChanged(object sender, EventArgs e)
        {
            DataTable dt = negocios.ObtenerTabla("CUIT, RazonSocial, Nombre, Email, NumTelefono, Direccion, CuentaBancaria, Activo", "Proveedor");
            dt = TraducirTabla(dt);
            dataGridView1.DataSource = dt;

            ContarProveedores();
            LimpiarCampos();
        }

        private void BTNAñadir_Click(object sender, EventArgs e)
        {
            ActivarBotones(BTNAñadir, LanguageManager.ObtenerInstancia().ObtenerTexto("Etiquetas.ModoAñadir"));
        }

        private void BTNModificar_Click(object sender, EventArgs e)
        {
            ActivarBotones(BTNModificar, LanguageManager.ObtenerInstancia().ObtenerTexto("Etiquetas.ModoModificar"));
        }

        private void BTNEliminar_Click(object sender, EventArgs e)
        {
            ActivarBotones(BTNEliminar, LanguageManager.ObtenerInstancia().ObtenerTexto("Etiquetas.ModoEliminar"));
        }

        private void BTNAplicar_Click(object sender, EventArgs e)
        {
            if (textBox8.Text == LanguageManager.ObtenerInstancia().ObtenerTexto("Etiquetas.ModoConsulta"))
            {
                NegociosEvento.RegistrarEvento(new Evento(SessionManager.ObtenerInstancia().ObtenerDatosUsuario().Username, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:ss"), "Proveedores", "Consulta de proveedores", 6));
            }

            if (textBox8.Text == LanguageManager.ObtenerInstancia().ObtenerTexto("Etiquetas.ModoAñadir"))
            {
                if (ValidarCampos() == true)
                {
                    Proveedor prov = new Proveedor(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text, textBox7.Text);

                    NegociosProveedor.RegistrarProveedorCompleto(prov);

                    NegociosEvento.RegistrarEvento(new Evento(SessionManager.ObtenerInstancia().ObtenerDatosUsuario().Username, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:ss"), "Proveedores", "Registro de proveedor", 4));

                    MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDeProveedores.Etiquetas.ProveedorRegistrado"));

                    ActivarBotones(BTNCancelar, LanguageManager.ObtenerInstancia().ObtenerTexto("Etiquetas.ModoConsulta"));

                    LimpiarCampos();
                }
            }

            if (textBox8.Text == LanguageManager.ObtenerInstancia().ObtenerTexto("Etiquetas.ModoModificar"))
            {
                if (ValidarCampos() == true)
                {
                    Proveedor prov = new Proveedor(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text, textBox7.Text);

                    NegociosProveedor.ModificarProveedor(prov);

                    NegociosEvento.RegistrarEvento(new Evento(SessionManager.ObtenerInstancia().ObtenerDatosUsuario().Username, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:ss"), "Proveedores", "Modificación de proveedor", 4));

                    MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDeProveedores.Etiquetas.ProveedorModificado"));

                    ActivarBotones(BTNCancelar, LanguageManager.ObtenerInstancia().ObtenerTexto("Etiquetas.ModoConsulta"));

                    LimpiarCampos();
                }
            }

            if (textBox8.Text == LanguageManager.ObtenerInstancia().ObtenerTexto("Etiquetas.ModoEliminar"))
            {
                if (negocios.RevisarDisponibilidad(textBox1.Text, "CUIT", "Proveedor") == false)
                {
                    MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionProveedores.Etiquetas.CuitNoExiste"));
                }
                else if (NegociosProveedor.RevisarDesactivado(textBox1.Text) == false)
                {
                    NegociosProveedor.ActivarProveedor(textBox1.Text);

                    NegociosEvento.RegistrarEvento(new Evento(SessionManager.ObtenerInstancia().ObtenerDatosUsuario().Username, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:ss"), "Proveedores", "Restauración de proveedor", 4));

                    MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDeProveedores.Etiquetas.ProveedorRestaurado"));

                    ActivarBotones(BTNCancelar, LanguageManager.ObtenerInstancia().ObtenerTexto("Etiquetas.ModoConsulta"));

                    LimpiarCampos();
                }
                else
                {
                    NegociosProveedor.DesactivarProveedor(textBox1.Text);

                    NegociosEvento.RegistrarEvento(new Evento(SessionManager.ObtenerInstancia().ObtenerDatosUsuario().Username, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:ss"), "Proveedores", "Borrado lógico de proveedor", 4));

                    MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDeProveedores.Etiquetas.ProveedorEliminado"));

                    ActivarBotones(BTNCancelar, LanguageManager.ObtenerInstancia().ObtenerTexto("Etiquetas.ModoConsulta"));

                    LimpiarCampos();
                }
            }

            FRMUI parent = this.MdiParent as FRMUI;
            parent.FormBitacoraEventos.Actualizar();
            parent.FormBitacoraCambios.Actualizar();

            if (ObtenerCondicionesQuery().Length > 5)
            {
                DataTable dt = negocios.ObtenerTabla("CUIT, RazonSocial, Nombre, Email, NumTelefono, Direccion, CuentaBancaria, Activo", "Proveedor", ObtenerCondicionesQuery()); ;
                dt = TraducirTabla(dt);
                dataGridView1.DataSource = dt;
            }
            else
            {
                DataTable dt = negocios.ObtenerTabla("CUIT, RazonSocial, Nombre, Email, NumTelefono, Direccion, CuentaBancaria, Activo", "Proveedor"); ;
                dt = TraducirTabla(dt);
                dataGridView1.DataSource = dt;
            }

            ContarProveedores();
        }

        public void RefrescarGrilla()
        {
            if (ObtenerCondicionesQuery().Length > 5)
            {
                DataTable dt = negocios.ObtenerTabla("CUIT, RazonSocial, Nombre, Email, NumTelefono, Direccion, CuentaBancaria, Activo", "Proveedor", ObtenerCondicionesQuery()); ;
                dt = TraducirTabla(dt);
                dataGridView1.DataSource = dt;
            }
            else
            {
                DataTable dt = negocios.ObtenerTabla("CUIT, RazonSocial, Nombre, Email, NumTelefono, Direccion, CuentaBancaria, Activo", "Proveedor"); ;
                dt = TraducirTabla(dt);
                dataGridView1.DataSource = dt;
            }

            ContarProveedores();
        }

        private void BTNCancelar_Click(object sender, EventArgs e)
        {
            ActivarBotones(BTNCancelar, LanguageManager.ObtenerInstancia().ObtenerTexto("Etiquetas.ModoConsulta"));
        }

        private string ObtenerCondicionesQuery()
        {
            //Esta función devuelve las condiciones para el WHERE de la consulta de proveedores

            string c = "";

            if (checkBox1.Checked == true)
            {
                c += "CUIT = '" + textBox1.Text + "' AND ";
            }

            if (checkBox2.Checked == true)
            {
                c += "RazonSocial = '" + textBox2.Text + "' AND ";
            }

            if (checkBox3.Checked == true)
            {
                c += "Nombre = '" + textBox3.Text + "' AND ";
            }

            if (checkBox4.Checked == true)
            {
                c += "Email = " + textBox4.Text + " AND ";
            }

            if (checkBox5.Checked == true)
            {
                c += "NumTelefono = " + textBox5.Text + " AND ";
            }

            if (checkBox6.Checked == true)
            {
                c += "Direccion = " + textBox6.Text + " AND ";
            }

            if (checkBox7.Checked == true)
            {
                c += "CuentaBancaria = " + textBox7.Text + " AND ";
            }

            if (checkBox8.Checked == true)
            {
                c += "Activo = '" + checkBox12.Checked.ToString() + "' AND ";
            }

            if (c.Length > 5)
            {
                c = c.Substring(0, c.Length - 5);
            }

            return c;
        }

        private void ActivarBotones(Button b, string modo)
        {
            //Este método hace que al seleccionar un modo el botón correspondiente quede inactivo y el resto de botones queden activos.

            textBox8.Text = modo;
            BTNAñadir.Enabled = true;
            BTNModificar.Enabled = true;
            BTNEliminar.Enabled = true;
            BTNCancelar.Enabled = true;

            b.Enabled = false;

            LimpiarCampos();

            if (modo == LanguageManager.ObtenerInstancia().ObtenerTexto("Etiquetas.ModoConsulta"))
            {
                foreach (CheckBox cb in this.Controls.OfType<CheckBox>())
                {
                    cb.Enabled = true;
                }
            }
            else
            {
                foreach (CheckBox cb in this.Controls.OfType<CheckBox>())
                {
                    cb.Enabled = false;
                }
            }

            if (modo == LanguageManager.ObtenerInstancia().ObtenerTexto("Etiquetas.ModoEliminar"))
            {
                textBox2.Enabled = false;
                textBox3.Enabled = false;
                textBox4.Enabled = false;
                textBox5.Enabled = false;
                textBox6.Enabled = false;
                textBox7.Enabled = false;
            }
            else
            {
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                textBox4.Enabled = true;
                textBox5.Enabled = true;
                textBox6.Enabled = true;
                textBox7.Enabled = true;
            }
        }

        private void LimpiarCampos()
        {
            //Reinicia todos los campos del formulario

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";

            foreach (CheckBox cb in this.Controls.OfType<CheckBox>())
            {
                cb.Checked = false;
            }
        }

        private bool ValidarCampos()
        {
            //Valida todos los campos para verificar que los datos ingresados son válidos (Se usa al añadir y modificar proveedores)

            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "")
            {
                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDeProveedores.Etiquetas.LlenarCampos"));
            }
            else if (textBox1.Text.All(char.IsDigit) == false)
            {
                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDeProveedores.Etiquetas.SoloNumeros"));
            }
            else if (textBox1.Text.Length < 11)
            {
                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDeProveedores.Etiquetas.11Digitos"));
            }
            else if (negocios.RevisarDisponibilidad(textBox1.Text, "CUIT", "Proveedor") && textBox8.Text == LanguageManager.ObtenerInstancia().ObtenerTexto("Etiquetas.ModoAñadir"))
            {
                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDeProveedores.Etiquetas.YaRegistrado"));
            }
            else if (negocios.RevisarDisponibilidad(textBox1.Text, "CUIT", "Proveedor") == false && textBox8.Text != LanguageManager.ObtenerInstancia().ObtenerTexto("Etiquetas.ModoAñadir"))
            {
                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDeProveedores.Etiquetas.CuitNoExiste"));
            }
            else
            {
                return true;
            }

            return false;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                textBox6.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                textBox7.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();

                dataGridView1.Rows[e.RowIndex].Selected = true;
            }
            catch (Exception)
            {

            }
        }

        private void ContarProveedores()
        {
            int cont = 0;
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    cont++;

                    if (Convert.ToInt32(row.Cells[7].Value) == 0)
                    {
                        row.DefaultCellStyle.BackColor = Color.Red;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDeProveedores.Etiquetas.ErrorConteo"));
            }

            label4.Text = cont.ToString();
        }

        DataTable TraducirTabla(DataTable dt)
        {
            dt.Columns[0].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.CUIT");
            dt.Columns[1].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.RazonSocial");
            dt.Columns[2].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Nombre");
            dt.Columns[3].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Email");
            dt.Columns[4].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.NumTelefono");
            dt.Columns[5].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Direccion");
            dt.Columns[6].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.CuentaBancaria");
            dt.Columns[7].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Activo");

            return dt;
        }
    }
}
