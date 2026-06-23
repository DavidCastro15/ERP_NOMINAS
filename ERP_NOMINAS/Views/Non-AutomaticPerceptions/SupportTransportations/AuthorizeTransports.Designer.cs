namespace ERP_NOMINAS.Views.Non_AutomaticPerceptions.SupportTransportations
{
    partial class AuthorizeTransports
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonClose1 = new ERP_NOMINAS.Components.Buttons.buttonClose();
            this.buttonMDelete1 = new ERP_NOMINAS.Components.Buttons.buttonMDelete();
            this.buttonAdd1 = new ERP_NOMINAS.Components.Buttons.buttonAdd();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonSave1 = new ERP_NOMINAS.Components.Buttons.buttonSave();
            this.searchCatalog1 = new ERP_NOMINAS.Components.Search.SearchCatalog();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            this.dataGridView1.Location = new System.Drawing.Point(12, 23);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(336, 369);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Id";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Visible = false;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Numero Empleado";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 80;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Nombre Completo";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 250;
            // 
            // buttonClose1
            // 
            this.buttonClose1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonClose1.Location = new System.Drawing.Point(275, 404);
            this.buttonClose1.Name = "buttonClose1";
            this.buttonClose1.Size = new System.Drawing.Size(73, 49);
            this.buttonClose1.TabIndex = 1;
            // 
            // buttonMDelete1
            // 
            this.buttonMDelete1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonMDelete1.Location = new System.Drawing.Point(196, 404);
            this.buttonMDelete1.Name = "buttonMDelete1";
            this.buttonMDelete1.Size = new System.Drawing.Size(73, 49);
            this.buttonMDelete1.TabIndex = 2;
            this.buttonMDelete1.OnBotonMDeleteClick += new System.EventHandler(this.buttonMDelete1_OnBotonMDeleteClick);
            // 
            // buttonAdd1
            // 
            this.buttonAdd1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonAdd1.Location = new System.Drawing.Point(116, 404);
            this.buttonAdd1.Name = "buttonAdd1";
            this.buttonAdd1.Size = new System.Drawing.Size(74, 50);
            this.buttonAdd1.TabIndex = 3;
            this.buttonAdd1.OnBotonAddClick += new System.EventHandler(this.buttonAdd1_OnBotonAddClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.buttonSave1);
            this.groupBox1.Controls.Add(this.searchCatalog1);
            this.groupBox1.Location = new System.Drawing.Point(12, 133);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(336, 115);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Agregar Empleado";
            this.groupBox1.Visible = false;
            // 
            // button1
            // 
            this.button1.Image = global::ERP_NOMINAS.Properties.Resources.icons8_x_28;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button1.Location = new System.Drawing.Point(263, 54);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(67, 48);
            this.button1.TabIndex = 7;
            this.button1.Text = "Salir";
            this.button1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonSave1
            // 
            this.buttonSave1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonSave1.Location = new System.Drawing.Point(179, 54);
            this.buttonSave1.Name = "buttonSave1";
            this.buttonSave1.Size = new System.Drawing.Size(72, 48);
            this.buttonSave1.TabIndex = 6;
            this.buttonSave1.VisibleBotonSave = false;
            this.buttonSave1.OnBotonSaveClick += new System.EventHandler(this.buttonSave1_OnBotonSaveClick);
            // 
            // searchCatalog1
            // 
            this.searchCatalog1._TypeCatalog = ERP_SHARED.GlobalFunctions.Enums.TypeCatalog.Employee;
            this.searchCatalog1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.searchCatalog1.Location = new System.Drawing.Point(6, 19);
            this.searchCatalog1.Name = "searchCatalog1";
            this.searchCatalog1.SelectedText = "";
            this.searchCatalog1.SelectedValue = null;
            this.searchCatalog1.Size = new System.Drawing.Size(324, 29);
            this.searchCatalog1.TabIndex = 0;
            this.searchCatalog1.TittleChange = "Empleado:";
            // 
            // AuthorizeTransports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(363, 465);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonAdd1);
            this.Controls.Add(this.buttonMDelete1);
            this.Controls.Add(this.buttonClose1);
            this.Controls.Add(this.dataGridView1);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "AuthorizeTransports";
            this.Text = "Autorizar Empleados";
            this.Load += new System.EventHandler(this.AuthorizeTransports_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private Components.Buttons.buttonClose buttonClose1;
        private Components.Buttons.buttonMDelete buttonMDelete1;
        private Components.Buttons.buttonAdd buttonAdd1;
        private System.Windows.Forms.GroupBox groupBox1;
        private Components.Search.SearchCatalog searchCatalog1;
        private Components.Buttons.buttonSave buttonSave1;
        private System.Windows.Forms.Button button1;
    }
}