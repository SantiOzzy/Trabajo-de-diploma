
namespace Trabajo_de_campo
{
    partial class FRMGenerarFactura
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FRMGenerarFactura));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.BTNSeleccionarProductos = new System.Windows.Forms.Button();
            this.BTNCobrarVenta = new System.Windows.Forms.Button();
            this.BTNGenerarFactura = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
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
            this.dataGridView1.Size = new System.Drawing.Size(557, 266);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // BTNSeleccionarProductos
            // 
            this.BTNSeleccionarProductos.Location = new System.Drawing.Point(12, 284);
            this.BTNSeleccionarProductos.Name = "BTNSeleccionarProductos";
            this.BTNSeleccionarProductos.Size = new System.Drawing.Size(134, 23);
            this.BTNSeleccionarProductos.TabIndex = 1;
            this.BTNSeleccionarProductos.Text = "Seleccionar productos";
            this.BTNSeleccionarProductos.UseVisualStyleBackColor = true;
            this.BTNSeleccionarProductos.Click += new System.EventHandler(this.BTNSeleccionarProductos_Click);
            // 
            // BTNCobrarVenta
            // 
            this.BTNCobrarVenta.Location = new System.Drawing.Point(12, 313);
            this.BTNCobrarVenta.Name = "BTNCobrarVenta";
            this.BTNCobrarVenta.Size = new System.Drawing.Size(134, 23);
            this.BTNCobrarVenta.TabIndex = 2;
            this.BTNCobrarVenta.Text = "Cobrar venta";
            this.BTNCobrarVenta.UseVisualStyleBackColor = true;
            this.BTNCobrarVenta.Click += new System.EventHandler(this.BTNCobrarVenta_Click);
            // 
            // BTNGenerarFactura
            // 
            this.BTNGenerarFactura.Location = new System.Drawing.Point(12, 342);
            this.BTNGenerarFactura.Name = "BTNGenerarFactura";
            this.BTNGenerarFactura.Size = new System.Drawing.Size(134, 23);
            this.BTNGenerarFactura.TabIndex = 3;
            this.BTNGenerarFactura.Text = "Generar factura";
            this.BTNGenerarFactura.UseVisualStyleBackColor = true;
            this.BTNGenerarFactura.Click += new System.EventHandler(this.BTNGenerarFactura_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(575, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "DNI: -";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(575, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Medio de pago: -";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(575, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Banco: -";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(575, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Marca de tarjeta: -";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(575, 111);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Tipo de tarjeta: -";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(575, 141);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(69, 13);
            this.label8.TabIndex = 11;
            this.label8.Text = "Monto total: -";
            // 
            // FRMGenerarFactura
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightBlue;
            this.ClientSize = new System.Drawing.Size(798, 367);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BTNGenerarFactura);
            this.Controls.Add(this.BTNCobrarVenta);
            this.Controls.Add(this.BTNSeleccionarProductos);
            this.Controls.Add(this.dataGridView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FRMGenerarFactura";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Generar factura";
            this.Activated += new System.EventHandler(this.FRMGenerarFactura_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FRMGenerarFactura_FormClosing);
            this.Load += new System.EventHandler(this.FRMGenerarFactura_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button BTNSeleccionarProductos;
        private System.Windows.Forms.Button BTNCobrarVenta;
        private System.Windows.Forms.Button BTNGenerarFactura;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label label4;
        public System.Windows.Forms.Label label5;
        public System.Windows.Forms.Label label8;
    }
}