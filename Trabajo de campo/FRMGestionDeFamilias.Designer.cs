
namespace Trabajo_de_campo
{
    partial class FRMGestionDeFamilias
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FRMGestionDeFamilias));
            this.BTNAgregarPermiso = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.CBPermisos = new System.Windows.Forms.ComboBox();
            this.CBFamilias = new System.Windows.Forms.ComboBox();
            this.BTNAplicar = new System.Windows.Forms.Button();
            this.BTNEliminarFamilia = new System.Windows.Forms.Button();
            this.BTNCrearFamilia = new System.Windows.Forms.Button();
            this.TXTFamilia = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // BTNAgregarPermiso
            // 
            this.BTNAgregarPermiso.Location = new System.Drawing.Point(12, 110);
            this.BTNAgregarPermiso.Name = "BTNAgregarPermiso";
            this.BTNAgregarPermiso.Size = new System.Drawing.Size(129, 23);
            this.BTNAgregarPermiso.TabIndex = 27;
            this.BTNAgregarPermiso.Text = "Agregar permiso";
            this.BTNAgregarPermiso.UseVisualStyleBackColor = true;
            this.BTNAgregarPermiso.Click += new System.EventHandler(this.BTNAgregarPermiso_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 24;
            this.label3.Text = "Permisos";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "Familias";
            // 
            // CBPermisos
            // 
            this.CBPermisos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBPermisos.FormattingEnabled = true;
            this.CBPermisos.Location = new System.Drawing.Point(12, 83);
            this.CBPermisos.Name = "CBPermisos";
            this.CBPermisos.Size = new System.Drawing.Size(129, 21);
            this.CBPermisos.TabIndex = 22;
            // 
            // CBFamilias
            // 
            this.CBFamilias.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBFamilias.FormattingEnabled = true;
            this.CBFamilias.Location = new System.Drawing.Point(12, 29);
            this.CBFamilias.Name = "CBFamilias";
            this.CBFamilias.Size = new System.Drawing.Size(129, 21);
            this.CBFamilias.TabIndex = 21;
            this.CBFamilias.SelectedIndexChanged += new System.EventHandler(this.CBFamilias_SelectedIndexChanged);
            // 
            // BTNAplicar
            // 
            this.BTNAplicar.Location = new System.Drawing.Point(152, 230);
            this.BTNAplicar.Name = "BTNAplicar";
            this.BTNAplicar.Size = new System.Drawing.Size(214, 23);
            this.BTNAplicar.TabIndex = 20;
            this.BTNAplicar.Text = "Aplicar";
            this.BTNAplicar.UseVisualStyleBackColor = true;
            this.BTNAplicar.Click += new System.EventHandler(this.BTNAplicar_Click);
            // 
            // BTNEliminarFamilia
            // 
            this.BTNEliminarFamilia.Location = new System.Drawing.Point(12, 230);
            this.BTNEliminarFamilia.Name = "BTNEliminarFamilia";
            this.BTNEliminarFamilia.Size = new System.Drawing.Size(129, 23);
            this.BTNEliminarFamilia.TabIndex = 28;
            this.BTNEliminarFamilia.Text = "Eliminar familia";
            this.BTNEliminarFamilia.UseVisualStyleBackColor = true;
            this.BTNEliminarFamilia.Click += new System.EventHandler(this.BTNEliminarFamilia_Click);
            // 
            // BTNCrearFamilia
            // 
            this.BTNCrearFamilia.Location = new System.Drawing.Point(12, 201);
            this.BTNCrearFamilia.Name = "BTNCrearFamilia";
            this.BTNCrearFamilia.Size = new System.Drawing.Size(129, 23);
            this.BTNCrearFamilia.TabIndex = 29;
            this.BTNCrearFamilia.Text = "Crear familia";
            this.BTNCrearFamilia.UseVisualStyleBackColor = true;
            this.BTNCrearFamilia.Click += new System.EventHandler(this.BTNCrearFamilia_Click);
            // 
            // TXTFamilia
            // 
            this.TXTFamilia.Location = new System.Drawing.Point(12, 175);
            this.TXTFamilia.Name = "TXTFamilia";
            this.TXTFamilia.Size = new System.Drawing.Size(129, 20);
            this.TXTFamilia.TabIndex = 30;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 159);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 13);
            this.label1.TabIndex = 31;
            this.label1.Text = "Nombre de la familia:";
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(152, 12);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(214, 212);
            this.treeView1.TabIndex = 25;
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            // 
            // FRMGestionDeFamilias
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightBlue;
            this.ClientSize = new System.Drawing.Size(370, 261);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TXTFamilia);
            this.Controls.Add(this.BTNCrearFamilia);
            this.Controls.Add(this.BTNEliminarFamilia);
            this.Controls.Add(this.BTNAgregarPermiso);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.CBPermisos);
            this.Controls.Add(this.CBFamilias);
            this.Controls.Add(this.BTNAplicar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FRMGestionDeFamilias";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestion de familias";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FRMGestionDeFamilias_FormClosing);
            this.Load += new System.EventHandler(this.FRMGestionDeFamilias_Load);
            this.VisibleChanged += new System.EventHandler(this.FRMGestionDeFamilias_VisibleChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BTNAgregarPermiso;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox CBPermisos;
        private System.Windows.Forms.ComboBox CBFamilias;
        private System.Windows.Forms.Button BTNAplicar;
        private System.Windows.Forms.Button BTNEliminarFamilia;
        private System.Windows.Forms.Button BTNCrearFamilia;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TreeView treeView1;
        public System.Windows.Forms.TextBox TXTFamilia;
    }
}