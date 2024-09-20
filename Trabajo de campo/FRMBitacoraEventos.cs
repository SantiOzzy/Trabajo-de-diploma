using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using iTextSharp.text;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using BE;
using Services;

namespace Trabajo_de_campo
{
    public partial class FRMBitacoraEventos : Form, IObserver
    {
        BLLUsuario NegociosUsuario = new BLLUsuario();
        BLLEvento NegociosEvento = new BLLEvento();
        Negocios negocios = new Negocios();
        public FRMBitacoraEventos()
        {
            InitializeComponent();
            LanguageManager.ObtenerInstancia().Agregar(this);
        }

        public void ActualizarIdioma()
        {
            LanguageManager.ObtenerInstancia().CambiarIdiomaControles(this);

            Actualizar();
        }

        private void FRMBitacoraEventos_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }

            LlenarCombobox();

            textBox1.Text = "";
            textBox2.Text = "";
            CBLogin.Text = "";
            CBModulo.Text = "";
            CBEvento.Text = "";
            CBCriticidad.Text = "";
            dateTimePicker1.Value = dateTimePicker1.MinDate;
            dateTimePicker2.Value = dateTimePicker2.MinDate;
        }

        private void FRMBitacoraEventos_Load(object sender, EventArgs e)
        {
            LlenarCombobox();

            textBox1.Text = "";
            textBox2.Text = "";
            CBLogin.Text = "";
            CBModulo.Text = "";
            CBEvento.Text = "";
            CBCriticidad.Text = "";
            dateTimePicker1.Value = dateTimePicker1.MinDate;
            dateTimePicker2.Value = dateTimePicker2.MinDate;

            Actualizar();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                textBox1.Text = NegociosUsuario.ObtenerUsuario(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString()).Nombre;
                textBox2.Text = NegociosUsuario.ObtenerUsuario(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString()).Apellido;

                dataGridView1.Rows[e.RowIndex].Selected = true;
            }
            catch (Exception) { }
        }

        public void Actualizar()
        {
            DataTable dt;
            if (dateTimePicker1.Value == dateTimePicker1.MinDate && dateTimePicker2.Value == dateTimePicker2.MinDate && CBCriticidad.Text == "" && CBEvento.Text == "" && CBLogin.Text == "" && CBModulo.Text == "")
            {
                dt = negocios.ObtenerTabla("*", "Evento", $"CONVERT(date,Fecha) >= '{DateTime.Now.AddDays(-3).ToString("yyyy-MM-ddTHH:mm:ss.fff")}' ORDER BY CodEvento DESC");
            }
            else
            {
                dt = negocios.ObtenerTabla("*", "Evento", $"Login LIKE '{CBLogin.Text}%' AND CONVERT(date,Fecha) >= '{dateTimePicker1.Value.ToString("yyyy-MM-ddTHH:mm:ss.fff")}' AND CONVERT(date,Fecha) <= '{dateTimePicker2.Value.ToString("yyyy-MM-ddTHH:mm:ss.fff")}' AND Modulo LIKE '{CBModulo.Text}%' AND Evento LIKE '{CBEvento.Text}%' AND Criticidad LIKE '{CBCriticidad.Text}%' ORDER BY CodEvento DESC");
            }
            dt.Columns[0].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.CodEvento");
            dt.Columns[1].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Login");
            dt.Columns[2].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Fecha");
            dt.Columns[3].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Hora");
            dt.Columns[4].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Modulo");
            dt.Columns[5].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Evento");
            dt.Columns[6].ColumnName = LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Criticidad");
            dataGridView1.DataSource = dt;
        }

        public void LlenarCombobox()
        {
            CBLogin.DataSource = negocios.ObtenerTabla("Username", "Usuario");
            CBLogin.DisplayMember = "Username";
            CBLogin.ValueMember = "Username";
        }

        private void BTNLimpiar_Click(object sender, EventArgs e)
        {
            dateTimePicker1.Value = dateTimePicker1.MinDate;
            dateTimePicker2.Value = dateTimePicker2.MinDate;
            textBox1.Text = "";
            textBox2.Text = "";
            CBLogin.Text = "";
            CBModulo.Text = "";
            CBEvento.Text = "";
            CBCriticidad.Text = "";

            Actualizar();
        }

        private void CBLogin_TextChanged(object sender, EventArgs e)
        {
            Actualizar();
        }

        private void CBModulo_TextChanged(object sender, EventArgs e)
        {
            CBEvento.Text = "";

            switch (CBModulo.Text)
            {
                case "Sesiones":
                    CBEvento.Items.Clear();
                    CBEvento.Items.Add("");
                    CBEvento.Items.Add("Inicio de sesión");
                    CBEvento.Items.Add("Cierre de sesión");
                    CBEvento.Items.Add("Cambio de contraseña");
                    break;
                case "Usuarios":
                    CBEvento.Items.Clear();
                    CBEvento.Items.Add("");
                    CBEvento.Items.Add("Registro de usuario");
                    CBEvento.Items.Add("Modificación de usuario");
                    CBEvento.Items.Add("Borrado lógico de usuario");
                    CBEvento.Items.Add("Restauración de usuario");
                    CBEvento.Items.Add("Bloqueo manual de usuario");
                    CBEvento.Items.Add("Desbloqueo de usuario");
                    CBEvento.Items.Add("Consulta de usuarios");
                    CBEvento.Items.Add("Generación de reporte de evento");
                    break;
                case "Perfiles":
                    CBEvento.Items.Clear();
                    CBEvento.Items.Add("");
                    CBEvento.Items.Add("Creación de perfil");
                    CBEvento.Items.Add("Modificación de perfil");
                    CBEvento.Items.Add("Borrado de perfil");
                    CBEvento.Items.Add("Creación de familia");
                    CBEvento.Items.Add("Modificación de familia");
                    CBEvento.Items.Add("Borrado de familia");
                    break;
                case "Libros":
                    CBEvento.Items.Clear();
                    CBEvento.Items.Add("");
                    CBEvento.Items.Add("Registro de libro");
                    CBEvento.Items.Add("Modificación de libro");
                    CBEvento.Items.Add("Borrado lógico de libro");
                    CBEvento.Items.Add("Restauración de libro");
                    CBEvento.Items.Add("Actualización de estado de libro");
                    CBEvento.Items.Add("Consulta de libros");
                    break;
                case "Clientes":
                    CBEvento.Items.Clear();
                    CBEvento.Items.Add("");
                    CBEvento.Items.Add("Registro de cliente");
                    CBEvento.Items.Add("Modificación de cliente");
                    CBEvento.Items.Add("Borrado lógico de cliente");
                    CBEvento.Items.Add("Restauración de cliente");
                    CBEvento.Items.Add("Consulta de clientes");
                    CBEvento.Items.Add("Serialización XML de clientes");
                    CBEvento.Items.Add("Des-serialización XML de clientes");
                    break;
                case "Ventas":
                    CBEvento.Items.Clear();
                    CBEvento.Items.Add("");
                    CBEvento.Items.Add("Generación de factura");
                    break;
                case "Base de datos":
                    CBEvento.Items.Clear();
                    CBEvento.Items.Add("");
                    CBEvento.Items.Add("Respaldo");
                    CBEvento.Items.Add("Restauración");
                    break;
                default:
                    CBEvento.Items.Clear();
                    break;
            }

            Actualizar();
        }

        private void CBEvento_TextChanged(object sender, EventArgs e)
        {
            Actualizar();
        }

        private void CBCriticidad_TextChanged(object sender, EventArgs e)
        {
            Actualizar();
        }

        private void dateTimePicker1_CloseUp(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value > DateTime.Now)
            {
                MessageBox.Show("La fecha inicial no puede ser mayor a la fecha actual");
                dataGridView1.DataSource = null;
            }
            else if (dateTimePicker1.Value > dateTimePicker2.Value)
            {
                MessageBox.Show("La fecha inicial no puede ser mayor a la fecha final");
                dataGridView1.DataSource = null;
            }
            else
            {
                Actualizar();
            }
        }

        private void dateTimePicker2_CloseUp(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value > DateTime.Now)
            {
                MessageBox.Show("La fecha inicial no puede ser mayor a la fecha actual");
                dataGridView1.DataSource = null;
            }
            else if (dateTimePicker1.Value > dateTimePicker2.Value)
            {
                MessageBox.Show("La fecha inicial no puede ser mayor a la fecha final");
                dataGridView1.DataSource = null;
            }
            else
            {
                Actualizar();
            }
        }

        private void BTNImprimir_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos en la grilla para imprimir");
            }
            else
            {
                try
                {
                    SaveFileDialog savefile = new SaveFileDialog();
                    savefile.FileName = string.Format("{0}.pdf", DateTime.Now.ToString("ddMMyyyyHHmmss"));

                    string PaginaHTML_Texto = Properties.Resources.ReporteEvento.ToString();
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@FECHA", DateTime.Now.ToString("dd/MM/yyyy"));

                    //DATOS DE EVENTO

                    string filas = string.Empty;

                    //

                    DataTable dt;
                    if (dateTimePicker1.Value == dateTimePicker1.MinDate && dateTimePicker2.Value == dateTimePicker2.MinDate && CBCriticidad.Text == "" && CBEvento.Text == "" && CBLogin.Text == "" && CBModulo.Text == "")
                    {
                        dt = negocios.ObtenerTabla("*", "Evento", $"CAST(Fecha AS date) >= '{DateTime.Now.AddDays(-3)}' ORDER BY CodEvento DESC");
                    }
                    else
                    {
                        dt = negocios.ObtenerTabla("*", "Evento", $"Login LIKE '{CBLogin.Text}%' AND CAST(Fecha AS date) >= '{dateTimePicker1.Value.ToString("yyyy-MM-dd")}' AND CAST(Fecha AS date) <= '{dateTimePicker2.Value.ToString("yyyy-MM-dd")}' AND Modulo LIKE '{CBModulo.Text}%' AND Evento LIKE '{CBEvento.Text}%' AND Criticidad LIKE '{CBCriticidad.Text}%' ORDER BY CodEvento DESC");
                    }

                    foreach (DataRow r in dt.Rows)
                    {
                        filas += "<tr>";
                        filas += "<td>" + r[0].ToString() + "</td>";
                        filas += "<td>" + r[1].ToString() + "</td>";
                        filas += "<td>" + r[2].ToString() + "</td>";
                        filas += "<td>" + r[3].ToString() + "</td>";
                        filas += "<td>" + r[4].ToString() + "</td>";
                        filas += "<td>" + r[5].ToString() + "</td>";
                        filas += "<td>" + r[6].ToString() + "</td>";
                        filas += "</tr>";
                    }
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@FILAS1", filas);

                    //DATOS DE USUARIO

                    dt = negocios.ObtenerTabla("DNI, Username, (Nombre + ' ' + Apellido), Rol, FechaNac, Email, NumTelefono", "Usuario", $"Username = '{dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[1].Value}'");

                    filas = string.Empty;

                    filas += "<tr>";
                    filas += "<td>" + dt.Rows[0][0].ToString() + "</td>";
                    filas += "<td>" + dt.Rows[0][1].ToString() + "</td>";
                    filas += "<td>" + dt.Rows[0][2].ToString() + "</td>";
                    filas += "<td>" + dt.Rows[0][3].ToString() + "</td>";
                    filas += "<td>" + dt.Rows[0][4].ToString() + "</td>";
                    filas += "<td>" + dt.Rows[0][5].ToString() + "</td>";
                    filas += "<td>" + dt.Rows[0][6].ToString() + "</td>";
                    filas += "</tr>";

                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@FILAS2", filas);

                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@REPORTEEVENTO", LanguageManager.ObtenerInstancia().ObtenerTexto("Reporte.ReporteEvento"));
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@IMPRESIONFECHA", LanguageManager.ObtenerInstancia().ObtenerTexto("Reporte.FechaImpresion"));
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@ISBN", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.ISBN"));
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@LIBRO", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Nombre"));
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@AUTOR", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Autor"));
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@PRECIO", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Precio"));
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@CANTIDAD", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Cantidad"));
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@CODEVENTO", dataGridView1.Columns[0].HeaderText);
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@LOGIN", dataGridView1.Columns[1].HeaderText);
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@FECHEVENTO", dataGridView1.Columns[2].HeaderText);
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@HORA", dataGridView1.Columns[3].HeaderText);
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@MODULO", dataGridView1.Columns[4].HeaderText);
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@EVENTO", dataGridView1.Columns[5].HeaderText);
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@CRITICIDAD", dataGridView1.Columns[6].HeaderText);
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@DNI", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.DNI"));
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@USERNAME", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Username"));
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@NOMBRE", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Nombre"));
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@ROL", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Rol"));
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@NACIMIENTO", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.FechaNac"));
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@EMAIL", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.Email"));
                    PaginaHTML_Texto = PaginaHTML_Texto.Replace("@NUMTEL", LanguageManager.ObtenerInstancia().ObtenerTexto("dgv.NumTelefono"));

                    if (savefile.ShowDialog() == DialogResult.OK)
                    {
                        using (FileStream stream = new FileStream(savefile.FileName, FileMode.Create))
                        {
                            Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 25);

                            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                            pdfDoc.Open();
                            pdfDoc.Add(new Phrase(LanguageManager.ObtenerInstancia().ObtenerTexto("Reporte.ReporteEvento")));

                            iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(Properties.Resources.uai, System.Drawing.Imaging.ImageFormat.Png);
                            img.ScaleToFit(150, 150);
                            img.Alignment = iTextSharp.text.Image.UNDERLYING;

                            img.SetAbsolutePosition(pdfDoc.Right - 150, pdfDoc.Top - 60);
                            pdfDoc.Add(img);


                            using (StringReader sr = new StringReader(PaginaHTML_Texto))
                            {
                                XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                            }

                            pdfDoc.Close();
                            stream.Close();
                        }

                        NegociosEvento.RegistrarEvento(new Evento(SessionManager.ObtenerInstancia().ObtenerDatosUsuario().Username, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:ss"), "Usuarios", "Generación de reporte de evento", 5));
                        Actualizar();

                        MessageBox.Show(LanguageManager.ObtenerInstancia().ObtenerTexto("FRMGenerarReporteFactura.Etiquetas.ReporteGenerado"));


                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al imprimir: " + ex);
                }
            }
        }

        private void FRMBitacoraEventos_VisibleChanged(object sender, EventArgs e)
        {
            LlenarCombobox();

            textBox1.Text = "";
            textBox2.Text = "";
            CBLogin.Text = "";
            CBModulo.Text = "";
            CBEvento.Text = "";
            CBCriticidad.Text = "";
            dateTimePicker1.Value = dateTimePicker1.MinDate;
            dateTimePicker2.Value = dateTimePicker2.MinDate;

            Actualizar();
        }
    }
}
