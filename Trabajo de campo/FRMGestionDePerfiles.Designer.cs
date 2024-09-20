
namespace Trabajo_de_campo
{
    partial class FRMGestionDePerfiles
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FRMGestionDePerfiles));
            this.BTNConfigurarFamilias = new System.Windows.Forms.Button();
            this.BTNCrearPerfil = new System.Windows.Forms.Button();
            this.BTNEliminarPerfil = new System.Windows.Forms.Button();
            this.BTNAplicar = new System.Windows.Forms.Button();
            this.CBPerfiles = new System.Windows.Forms.ComboBox();
            this.CBFamilias = new System.Windows.Forms.ComboBox();
            this.CBPermisos = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.BTNAgregarFamilia = new System.Windows.Forms.Button();
            this.BTNAgregarPermiso = new System.Windows.Forms.Button();
            this.TXTPerfil = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // BTNConfigurarFamilias
            // 
            this.BTNConfigurarFamilias.Location = new System.Drawing.Point(188, 306);
            this.BTNConfigurarFamilias.Name = "BTNConfigurarFamilias";
            this.BTNConfigurarFamilias.Size = new System.Drawing.Size(129, 23);
            this.BTNConfigurarFamilias.TabIndex = 3;
            this.BTNConfigurarFamilias.Text = "Configurar familias";
            this.BTNConfigurarFamilias.UseVisualStyleBackColor = true;
            this.BTNConfigurarFamilias.Click += new System.EventHandler(this.BTNConfigurarFamilias_Click);
            // 
            // BTNCrearPerfil
            // 
            this.BTNCrearPerfil.Location = new System.Drawing.Point(12, 277);
            this.BTNCrearPerfil.Name = "BTNCrearPerfil";
            this.BTNCrearPerfil.Size = new System.Drawing.Size(129, 23);
            this.BTNCrearPerfil.TabIndex = 5;
            this.BTNCrearPerfil.Text = "Crear perfil";
            this.BTNCrearPerfil.UseVisualStyleBackColor = true;
            this.BTNCrearPerfil.Click += new System.EventHandler(this.BTNCrearPerfil_Click);
            // 
            // BTNEliminarPerfil
            // 
            this.BTNEliminarPerfil.Location = new System.Drawing.Point(12, 306);
            this.BTNEliminarPerfil.Name = "BTNEliminarPerfil";
            this.BTNEliminarPerfil.Size = new System.Drawing.Size(129, 23);
            this.BTNEliminarPerfil.TabIndex = 6;
            this.BTNEliminarPerfil.Text = "Eliminar perfil";
            this.BTNEliminarPerfil.UseVisualStyleBackColor = true;
            this.BTNEliminarPerfil.Click += new System.EventHandler(this.BTNEliminarPerfil_Click);
            // 
            // BTNAplicar
            // 
            this.BTNAplicar.Location = new System.Drawing.Point(147, 277);
            this.BTNAplicar.Name = "BTNAplicar";
            this.BTNAplicar.Size = new System.Drawing.Size(214, 23);
            this.BTNAplicar.TabIndex = 7;
            this.BTNAplicar.Text = "Aplicar";
            this.BTNAplicar.UseVisualStyleBackColor = true;
            this.BTNAplicar.Click += new System.EventHandler(this.BTNAplicar_Click);
            // 
            // CBPerfiles
            // 
            this.CBPerfiles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBPerfiles.FormattingEnabled = true;
            this.CBPerfiles.Location = new System.Drawing.Point(12, 32);
            this.CBPerfiles.Name = "CBPerfiles";
            this.CBPerfiles.Size = new System.Drawing.Size(129, 21);
            this.CBPerfiles.TabIndex = 9;
            this.CBPerfiles.SelectedIndexChanged += new System.EventHandler(this.CBPerfiles_SelectedIndexChanged);
            // 
            // CBFamilias
            // 
            this.CBFamilias.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBFamilias.FormattingEnabled = true;
            this.CBFamilias.Location = new System.Drawing.Point(12, 81);
            this.CBFamilias.Name = "CBFamilias";
            this.CBFamilias.Size = new System.Drawing.Size(129, 21);
            this.CBFamilias.TabIndex = 10;
            // 
            // CBPermisos
            // 
            this.CBPermisos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBPermisos.FormattingEnabled = true;
            this.CBPermisos.Location = new System.Drawing.Point(12, 158);
            this.CBPermisos.Name = "CBPermisos";
            this.CBPermisos.Size = new System.Drawing.Size(129, 21);
            this.CBPermisos.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Perfiles";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Familias";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 142);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Permisos";
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(147, 16);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(214, 255);
            this.treeView1.TabIndex = 17;
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            // 
            // BTNAgregarFamilia
            // 
            this.BTNAgregarFamilia.Location = new System.Drawing.Point(12, 108);
            this.BTNAgregarFamilia.Name = "BTNAgregarFamilia";
            this.BTNAgregarFamilia.Size = new System.Drawing.Size(129, 23);
            this.BTNAgregarFamilia.TabIndex = 18;
            this.BTNAgregarFamilia.Text = "Agregar familia";
            this.BTNAgregarFamilia.UseVisualStyleBackColor = true;
            this.BTNAgregarFamilia.Click += new System.EventHandler(this.BTNAgregarFamilia_Click);
            // 
            // BTNAgregarPermiso
            // 
            this.BTNAgregarPermiso.Location = new System.Drawing.Point(12, 185);
            this.BTNAgregarPermiso.Name = "BTNAgregarPermiso";
            this.BTNAgregarPermiso.Size = new System.Drawing.Size(129, 23);
            this.BTNAgregarPermiso.TabIndex = 19;
            this.BTNAgregarPermiso.Text = "Agregar permiso";
            this.BTNAgregarPermiso.UseVisualStyleBackColor = true;
            this.BTNAgregarPermiso.Click += new System.EventHandler(this.BTNAgregarPermiso_Click);
            // 
            // TXTPerfil
            // 
            this.TXTPerfil.Location = new System.Drawing.Point(12, 251);
            this.TXTPerfil.Name = "TXTPerfil";
            this.TXTPerfil.Size = new System.Drawing.Size(129, 20);
            this.TXTPerfil.TabIndex = 20;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 235);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 13);
            this.label4.TabIndex = 21;
            this.label4.Text = "Nombre del perfil:";
            // 
            // FRMGestionDePerfiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightBlue;
            this.ClientSize = new System.Drawing.Size(364, 341);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.TXTPerfil);
            this.Controls.Add(this.BTNAgregarPermiso);
            this.Controls.Add(this.BTNAgregarFamilia);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CBPermisos);
            this.Controls.Add(this.CBFamilias);
            this.Controls.Add(this.CBPerfiles);
            this.Controls.Add(this.BTNAplicar);
            this.Controls.Add(this.BTNEliminarPerfil);
            this.Controls.Add(this.BTNCrearPerfil);
            this.Controls.Add(this.BTNConfigurarFamilias);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FRMGestionDePerfiles";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestión de roles";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FRMGestionDePerfiles_FormClosing);
            this.Load += new System.EventHandler(this.FRMGestionDePerfiles_Load);
            this.VisibleChanged += new System.EventHandler(this.FRMGestionDePerfiles_VisibleChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button BTNConfigurarFamilias;
        private System.Windows.Forms.Button BTNCrearPerfil;
        private System.Windows.Forms.Button BTNEliminarPerfil;
        private System.Windows.Forms.Button BTNAplicar;
        private System.Windows.Forms.ComboBox CBPermisos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Button BTNAgregarFamilia;
        private System.Windows.Forms.Button BTNAgregarPermiso;
        private System.Windows.Forms.TextBox TXTPerfil;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.ComboBox CBFamilias;
        public System.Windows.Forms.ComboBox CBPerfiles;
    }
}