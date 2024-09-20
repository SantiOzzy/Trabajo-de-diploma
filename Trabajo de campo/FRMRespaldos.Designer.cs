
namespace Trabajo_de_campo
{
    partial class FRMRespaldos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FRMRespaldos));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.BTNRespaldar = new System.Windows.Forms.Button();
            this.BTNRestaurar = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.PBBackup = new System.Windows.Forms.PictureBox();
            this.PBRestore = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.PBBackup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PBRestore)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Respaldar";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 139);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Restaurar";
            // 
            // BTNRespaldar
            // 
            this.BTNRespaldar.Location = new System.Drawing.Point(26, 64);
            this.BTNRespaldar.Name = "BTNRespaldar";
            this.BTNRespaldar.Size = new System.Drawing.Size(159, 33);
            this.BTNRespaldar.TabIndex = 2;
            this.BTNRespaldar.Text = "Respaldar";
            this.BTNRespaldar.UseVisualStyleBackColor = true;
            this.BTNRespaldar.Click += new System.EventHandler(this.BTNRespaldar_Click);
            // 
            // BTNRestaurar
            // 
            this.BTNRestaurar.Location = new System.Drawing.Point(26, 181);
            this.BTNRestaurar.Name = "BTNRestaurar";
            this.BTNRestaurar.Size = new System.Drawing.Size(159, 33);
            this.BTNRestaurar.TabIndex = 3;
            this.BTNRestaurar.Text = "Restaurar";
            this.BTNRestaurar.UseVisualStyleBackColor = true;
            this.BTNRestaurar.Click += new System.EventHandler(this.BTNRestaurar_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(26, 38);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(280, 20);
            this.textBox1.TabIndex = 4;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(26, 155);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(280, 20);
            this.textBox2.TabIndex = 5;
            // 
            // PBBackup
            // 
            this.PBBackup.Image = global::Trabajo_de_campo.Properties.Resources._8316630;
            this.PBBackup.Location = new System.Drawing.Point(312, 38);
            this.PBBackup.Name = "PBBackup";
            this.PBBackup.Size = new System.Drawing.Size(58, 59);
            this.PBBackup.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PBBackup.TabIndex = 6;
            this.PBBackup.TabStop = false;
            this.PBBackup.Click += new System.EventHandler(this.PBBackup_Click);
            // 
            // PBRestore
            // 
            this.PBRestore.Image = global::Trabajo_de_campo.Properties.Resources._8316630;
            this.PBRestore.Location = new System.Drawing.Point(312, 155);
            this.PBRestore.Name = "PBRestore";
            this.PBRestore.Size = new System.Drawing.Size(58, 59);
            this.PBRestore.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PBRestore.TabIndex = 7;
            this.PBRestore.TabStop = false;
            this.PBRestore.Click += new System.EventHandler(this.PBRestore_Click);
            // 
            // FRMRespaldos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightBlue;
            this.ClientSize = new System.Drawing.Size(380, 226);
            this.Controls.Add(this.PBRestore);
            this.Controls.Add(this.PBBackup);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.BTNRestaurar);
            this.Controls.Add(this.BTNRespaldar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FRMRespaldos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Respaldos";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FRMRespaldos_FormClosing);
            this.Load += new System.EventHandler(this.FRMRespaldos_Load);
            this.VisibleChanged += new System.EventHandler(this.FRMRespaldos_VisibleChanged);
            ((System.ComponentModel.ISupportInitialize)(this.PBBackup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PBRestore)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button BTNRespaldar;
        private System.Windows.Forms.Button BTNRestaurar;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.PictureBox PBBackup;
        private System.Windows.Forms.PictureBox PBRestore;
    }
}