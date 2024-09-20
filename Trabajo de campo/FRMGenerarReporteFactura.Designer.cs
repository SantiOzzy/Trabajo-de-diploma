
namespace Trabajo_de_campo
{
    partial class FRMGenerarReporteFactura
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FRMGenerarReporteFactura));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.BTNGenerarReporte = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(1111, 275);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // BTNGenerarReporte
            // 
            this.BTNGenerarReporte.BackColor = System.Drawing.SystemColors.ControlLight;
            this.BTNGenerarReporte.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTNGenerarReporte.ForeColor = System.Drawing.Color.Black;
            this.BTNGenerarReporte.Location = new System.Drawing.Point(427, 293);
            this.BTNGenerarReporte.Name = "BTNGenerarReporte";
            this.BTNGenerarReporte.Size = new System.Drawing.Size(312, 145);
            this.BTNGenerarReporte.TabIndex = 1;
            this.BTNGenerarReporte.Text = "Generar reporte";
            this.BTNGenerarReporte.UseVisualStyleBackColor = false;
            this.BTNGenerarReporte.Click += new System.EventHandler(this.BTNGenerarReporte_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 425);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(415, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Seleccione la factura en la grilla y presione el botón para guardar el reporte co" +
    "mo .PDF";
            // 
            // FRMGenerarReporteFactura
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightBlue;
            this.ClientSize = new System.Drawing.Size(1135, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BTNGenerarReporte);
            this.Controls.Add(this.dataGridView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FRMGenerarReporteFactura";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Generar reporte factura";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FRMGenerarReporteFactura_FormClosing);
            this.Load += new System.EventHandler(this.FRMGenerarReporteFactura_Load);
            this.VisibleChanged += new System.EventHandler(this.FRMGenerarReporteFactura_VisibleChanged);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button BTNGenerarReporte;
        private System.Windows.Forms.Label label1;
    }
}