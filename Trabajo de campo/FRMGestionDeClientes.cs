using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
using System.IO;
using System.Xml;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using Services;
using BE;
using BLL;

namespace Trabajo_de_campo
{
    public partial class FRMGestionDeClientes : Form, IObserver
    {
        Negocios negocios = new Negocios();
        BLLCliente NegociosCliente = new BLLCliente();
        BLLEvento NegociosEvento = new BLLEvento();
        CryptoManager Encriptacion = new CryptoManager();
        Serializacion serializacion = new Serializacion();

        public FRMGestionDeClientes()
        {
            InitializeComponent();
            LanguageManager.ObtenerInstancia().Agregar(this);
        }

        public void ActualizarIdioma()
        {
            LanguageManager.ObtenerInstancia().CambiarIdiomaControles(this);

            if (ObtenerCondicionesQuery().Length > 5)
            {
                DataTable dt = negocios.ObtenerTabla("DNI, Nombre, Apellido, Direccion, Email, NumTelefono, Activo", "Cliente", ObtenerCondicionesQuery());
                dt = TraducirTabla(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    dr[3] = Encriptacion.DesencriptarAES256(dr[3].ToString());
                }

                dataGridView1.DataSource = dt;
            }
            else
            {
                DataTable dt = negocios.ObtenerTabla("DNI, Nombre, Apellido, Direccion, Email, NumTelefono, Activo", "Cliente");
                dt = TraducirTabla(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    dr[3] = Encriptacion.DesencriptarAES256(dr[3].ToString());
                }

                dataGridView1.DataSource = dt;
            }

            ContarClientes();

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

        private void FRMGestionDeClientes_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }

            LimpiarCampos();

            if (ObtenerCondicionesQuery().Length > 5)
            {
                DataTable dt = negocios.ObtenerTabla("DNI, Nombre, Apellido, Direccion, Email, NumTelefono, Activo", "Cliente", ObtenerCondicionesQuery());
                dt = TraducirTabla(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    dr[3] = Encriptacion.DesencriptarAES256(dr[3].ToString());
                }

                dataGridView1.DataSource = dt;
            }
            else
            {
                DataTable dt = negocios.ObtenerTabla("DNI, Nombre, Apellido, Direccion, Email, NumTelefono, Activo", "Cliente");
                dt = TraducirTabla(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    dr[3] = Encriptacion.DesencriptarAES256(dr[3].ToString());
                }

                dataGridView1.DataSource = dt;
            }

            LimpiarCampos();
        }

        private void FRMGestionDeClientes_VisibleChanged(object sender, EventArgs e)
        {
            DataTable dt = negocios.ObtenerTabla("DNI, Nombre, Apellido, Direccion, Email, NumTelefono, Activo", "Cliente");
            dt = TraducirTabla(dt);

            foreach(DataRow dr in dt.Rows)
            {
                dr[3] = Encriptacion.DesencriptarAES256(dr[3].ToString());
            }

            dataGridView1.DataSource = dt;
            ContarClientes();
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
                NegociosEvento.RegistrarEvento(new Evento(SessionManager.ObtenerInstancia().ObtenerDatosUsuario().Username, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:ss"), "Clientes", "Consulta de clientes", 6));
            }

            if (textBox8.Text == LanguageManager.ObtenerInstancia().ObtenerTexto("Etiquetas.ModoAñadir"))
            {
                if (ValidarCampos() == true)
                {
                    Cliente customer = new Cliente(Convert.ToInt32(numericUpDown1.Value), textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text);

                    NegociosCliente.RegistrarCliente(customer);

                    NegociosEvento.RegistrarEvento(new Evento(SessionManager.ObtenerInstancia().ObtenerDatosUsuario().Username, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:ss"), "Clientes", "Registro de cliente", 4));

                    MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDeClientes.Etiquetas.ClienteRegistrado"));

                    ActivarBotones(BTNCancelar, LanguageManager.ObtenerInstancia().ObtenerTexto("Etiquetas.ModoConsulta"));

                    LimpiarCampos();
                }
            }

            if (textBox8.Text == LanguageManager.ObtenerInstancia().ObtenerTexto("Etiquetas.ModoModificar"))
            {
                if (ValidarCampos() == true)
                {
                    Cliente customer = new Cliente(Convert.ToInt32(numericUpDown1.Value), textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text);

                    NegociosCliente.ModificarCliente(customer);

                    NegociosEvento.RegistrarEvento(new Evento(SessionManager.ObtenerInstancia().ObtenerDatosUsuario().Username, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:ss"), "Clientes", "Modificación de cliente", 4));

                    MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDeClientes.Etiquetas.ClienteModificado"));

                    ActivarBotones(BTNCancelar, LanguageManager.ObtenerInstancia().ObtenerTexto("Etiquetas.ModoConsulta"));

                    LimpiarCampos();
                }
            }

            if (textBox8.Text == LanguageManager.ObtenerInstancia().ObtenerTexto("Etiquetas.ModoEliminar"))
            {
                if (negocios.RevisarDisponibilidad(numericUpDown1.Value.ToString(), "DNI", "Cliente") == false)
                {
                    MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDeClientes.Etiquetas.DNINoExiste"));
                }
                else if (NegociosCliente.RevisarDesactivado(numericUpDown1.Value.ToString()) == false)
                {
                    NegociosCliente.ActivarCliente(numericUpDown1.Value.ToString());

                    NegociosEvento.RegistrarEvento(new Evento(SessionManager.ObtenerInstancia().ObtenerDatosUsuario().Username, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:ss"), "Clientes", "Restauración de cliente", 4));

                    MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDeClientes.Etiquetas.ClienteRestaurado"));

                    ActivarBotones(BTNCancelar, LanguageManager.ObtenerInstancia().ObtenerTexto("Etiquetas.ModoConsulta"));

                    LimpiarCampos();
                }
                else
                {
                    NegociosCliente.DesactivarCliente(numericUpDown1.Value.ToString());

                    NegociosEvento.RegistrarEvento(new Evento(SessionManager.ObtenerInstancia().ObtenerDatosUsuario().Username, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:ss"), "Clientes", "Borrado lógico de cliente", 4));

                    MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDeClientes.Etiquetas.ClienteEliminado"));

                    ActivarBotones(BTNCancelar, LanguageManager.ObtenerInstancia().ObtenerTexto("Etiquetas.ModoConsulta"));

                    LimpiarCampos();
                }
            }

            FRMUI parent = this.MdiParent as FRMUI;
            parent.FormBitacoraEventos.Actualizar();

            if (ObtenerCondicionesQuery().Length > 5)
            {
                DataTable dt = negocios.ObtenerTabla("DNI, Nombre, Apellido, Direccion, Email, NumTelefono, Activo", "Cliente", ObtenerCondicionesQuery());
                dt = TraducirTabla(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    dr[3] = Encriptacion.DesencriptarAES256(dr[3].ToString());
                }

                dataGridView1.DataSource = dt;
            }
            else
            {
                DataTable dt = negocios.ObtenerTabla("DNI, Nombre, Apellido, Direccion, Email, NumTelefono, Activo", "Cliente");
                dt = TraducirTabla(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    dr[3] = Encriptacion.DesencriptarAES256(dr[3].ToString());
                }

                dataGridView1.DataSource = dt;
            }

            ContarClientes();
        }

        private void BTNCancelar_Click(object sender, EventArgs e)
        {
            ActivarBotones(BTNCancelar, LanguageManager.ObtenerInstancia().ObtenerTexto("Etiquetas.ModoConsulta"));
        }

        private string ObtenerCondicionesQuery()
        {
            //Esta función devuelve las condiciones para el WHERE de la consulta de clientes

            string c = "";

            if (checkBox1.Checked == true)
            {
                c += "DNI = " + numericUpDown1.Value.ToString() + " AND ";
            }

            if (checkBox2.Checked == true)
            {
                c += "Nombre = '" + textBox1.Text + "' AND ";
            }

            if (checkBox3.Checked == true)
            {
                c += "Apellido = '" + textBox2.Text + "' AND ";
            }

            if (checkBox4.Checked == true)
            {
                c += "Direccion = '" + textBox3.Text + "' AND ";
            }

            if (checkBox5.Checked == true)
            {
                c += "Email = '" + textBox4.Text + "' AND ";
            }

            if (checkBox6.Checked == true)
            {
                c += "NumTelefono = '" + textBox5.Text + "' AND ";
            }

            if (checkBox7.Checked == true)
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
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                textBox3.Enabled = false;
                textBox4.Enabled = false;
                textBox5.Enabled = false;
            }
            else
            {
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                textBox4.Enabled = true;
                textBox5.Enabled = true;
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
            numericUpDown1.Value = numericUpDown1.Minimum;

            foreach (CheckBox cb in this.Controls.OfType<CheckBox>())
            {
                cb.Checked = false;
            }
        }

        private bool ValidarCampos()
        {
            //Valida todos los campos para verificar que los datos ingresados son válidos (Se usa al añadir y modificar clientes)

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
                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDeClientes.Etiquetas.LlenarCampos"));
            }
            else if (negocios.RevisarDisponibilidad(numericUpDown1.Value.ToString(), "DNI", "Cliente") && textBox8.Text == LanguageManager.ObtenerInstancia().ObtenerTexto("Etiquetas.ModoAñadir"))
            {
                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDeClientes.Etiquetas.DNIEnUso"));
            }
            else if (negocios.RevisarDisponibilidad(numericUpDown1.Value.ToString(), "DNI", "Cliente") == false && textBox8.Text != LanguageManager.ObtenerInstancia().ObtenerTexto("Etiquetas.ModoAñadir"))
            {
                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDeClientes.Etiquetas.DNINoExiste"));
            }
            else if (MailValido == false)
            {
                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDeClientes.Etiquetas.CorreoInvalido"));
            }
            else if (negocios.RevisarDisponibilidad(textBox4.Text, "Email", "Cliente") && textBox8.Text == LanguageManager.ObtenerInstancia().ObtenerTexto("Etiquetas.ModoAñadir"))
            {
                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDeClientes.Etiquetas.CorreoEnUso"));
            }
            else if (negocios.RevisarDisponibilidadConExcepcion(textBox4.Text, numericUpDown1.Value.ToString(), "DNI, Email", "Cliente") && textBox8.Text == LanguageManager.ObtenerInstancia().ObtenerTexto("Etiquetas.ModoModificar"))
            {
                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDeClientes.Etiquetas.CorreoEnUso"));
            }
            else if (NumeroTelefonoValido == false)
            {
                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDeClientes.Etiquetas.NumTelInvalido"));
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
                numericUpDown1.Value = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();

                dataGridView1.Rows[e.RowIndex].Selected = true;
            }
            catch (Exception)
            {

            }
        }

        private void ContarClientes()
        {
            int cont = 0;
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    cont++;

                    if (Convert.ToInt32(row.Cells[6].Value) == 0)
                    {
                        row.DefaultCellStyle.BackColor = Color.Red;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDeClientes.Etiquetas.ErrorEnConteo"));
            }

            label4.Text = cont.ToString();
        }

        DataTable TraducirTabla(DataTable dt)
        {
            dt.Columns[0].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.DNI");
            dt.Columns[1].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Nombre");
            dt.Columns[2].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Apellido");
            dt.Columns[3].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Direccion");
            dt.Columns[4].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Email");
            dt.Columns[5].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.NumTelefono");
            dt.Columns[6].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Activo");

            return dt;
        }

        private void BTNSerializar_Click(object sender, EventArgs e)
        {
            try
            {
                saveFileDialog1.Filter = $"XML Files|*.xml";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    serializacion.SerializarXML(saveFileDialog1.FileName, dataGridView1);

                    MostrarXmlEnListbox(saveFileDialog1.FileName);

                    NegociosEvento.RegistrarEvento(new Evento(SessionManager.ObtenerInstancia().ObtenerDatosUsuario().Username, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:ss"), "Clientes", "Serialización XML de clientes", 5));

                    MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDeClientes.Etiquetas.DatosSerializados"));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{"FRMGestionDeClientes.Etiquetas.ErrorSerializacion"}: {ex.Message}\n{"FRMGestionDeClientes.Etiquetas.Detalles"}: {ex.InnerException?.Message}");
            }
        }

        private void BTNDesserializar_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.Filter = $"XML Files|*.xml";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    DataTable dt = serializacion.DeserializarXML(openFileDialog1.FileName);

                    dataGridView1.DataSource = dt;
                    ContarClientes();

                    listBox1.Items.Clear();

                    foreach(DataRow dr in dt.Rows)
                    {
                        listBox1.Items.Add($"{LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.DNI")}: {dr[0]} - {LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Nombre")}: {dr[1]} - {LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Apellido")}: {dr[2]} - {LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Direccion")}: {dr[3]} - {LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Email")}: {dr[4]} - {LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.NumTelefono")}: {dr[5]} - {LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Activo")}: {dr[6]}");
                    }

                    NegociosEvento.RegistrarEvento(new Evento(SessionManager.ObtenerInstancia().ObtenerDatosUsuario().Username, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:ss"), "Clientes", "Des-serialización XML de clientes", 5));

                    MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGestionDeClientes.Etiquetas.DatosDeserializados"));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{"FRMGestionDeClientes.Etiquetas.ErrorDeserializacion"}: {ex.Message}\n{"FRMGestionDeClientes.Etiquetas.Detalles"}: {ex.InnerException?.Message}");
            }
        }

        private void MostrarXmlEnListbox(string path)
        {
            listBox1.Items.Clear();

            string[] lineas = File.ReadAllLines(path);

            foreach (string linea in lineas)
            {
                listBox1.Items.Add(linea);
            }
        }

        private void BTNLimpiar_Click(object sender, EventArgs e)
        {
            DataTable dt = negocios.ObtenerTabla("DNI, Nombre, Apellido, Direccion, Email, NumTelefono, Activo", "Cliente");
            dt = TraducirTabla(dt);

            foreach (DataRow dr in dt.Rows)
            {
                dr[3] = Encriptacion.DesencriptarAES256(dr[3].ToString());
            }

            dataGridView1.DataSource = dt;

            ContarClientes();
        }
    }
}
