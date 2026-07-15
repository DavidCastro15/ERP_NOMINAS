namespace ERP_NOMINAS.Views.Non_AutomaticDeductions.SkipSaving
{
    partial class SkipSavingMain
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
            this.filterByT1 = new ERP_NOMINAS.Components.Search.FilterByT();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonClose1 = new ERP_NOMINAS.Components.Buttons.buttonClose();
            this.buttonAdd1 = new ERP_NOMINAS.Components.Buttons.buttonAdd();
            this.buttonFDelete1 = new ERP_NOMINAS.Components.Buttons.buttonFDelete();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.searchCatalog1 = new ERP_NOMINAS.Components.Search.SearchCatalog();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // filterByT1
            // 
            this.filterByT1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.filterByT1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.filterByT1.FilterBy = null;
            this.filterByT1.Location = new System.Drawing.Point(12, 24);
            this.filterByT1.Name = "filterByT1";
            this.filterByT1.Size = new System.Drawing.Size(268, 49);
            this.filterByT1.TabIndex = 0;
            this.filterByT1.TittleChange = "Nombre/No. Empleado:";
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
            this.dataGridView1.Location = new System.Drawing.Point(12, 79);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(354, 528);
            this.dataGridView1.TabIndex = 1;
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
            this.Column2.HeaderText = "No. Empleado";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
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
            this.buttonClose1.Location = new System.Drawing.Point(293, 613);
            this.buttonClose1.Name = "buttonClose1";
            this.buttonClose1.Size = new System.Drawing.Size(73, 49);
            this.buttonClose1.TabIndex = 2;
            // 
            // buttonAdd1
            // 
            this.buttonAdd1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonAdd1.Location = new System.Drawing.Point(137, 613);
            this.buttonAdd1.Name = "buttonAdd1";
            this.buttonAdd1.Size = new System.Drawing.Size(74, 50);
            this.buttonAdd1.TabIndex = 3;
            this.buttonAdd1.OnBotonAddClick += new System.EventHandler(this.buttonAdd1_OnBotonAddClick);
            // 
            // buttonFDelete1
            // 
            this.buttonFDelete1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonFDelete1.Location = new System.Drawing.Point(217, 613);
            this.buttonFDelete1.Name = "buttonFDelete1";
            this.buttonFDelete1.Size = new System.Drawing.Size(73, 50);
            this.buttonFDelete1.TabIndex = 4;
            this.buttonFDelete1.OnBotonDeleteClick += new System.EventHandler(this.buttonFDelete1_OnBotonDeleteClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.searchCatalog1);
            this.groupBox1.Location = new System.Drawing.Point(12, 226);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(354, 126);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Agregar Empleado";
            this.groupBox1.Visible = false;
            // 
            // searchCatalog1
            // 
            this.searchCatalog1._TypeCatalog = ERP_SHARED.GlobalFunctions.Enums.TypeCatalog.Employee;
            this.searchCatalog1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.searchCatalog1.Location = new System.Drawing.Point(6, 34);
            this.searchCatalog1.Name = "searchCatalog1";
            this.searchCatalog1.SelectedText = "";
            this.searchCatalog1.SelectedValue = null;
            this.searchCatalog1.Size = new System.Drawing.Size(342, 29);
            this.searchCatalog1.TabIndex = 0;
            this.searchCatalog1.TittleChange = "Empleado:";
            // 
            // button1
            // 
            this.button1.Image = global::ERP_NOMINAS.Properties.Resources.icons8_x_28;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button1.Location = new System.Drawing.Point(271, 69);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 51);
            this.button1.TabIndex = 1;
            this.button1.Text = "Salir";
            this.button1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Image = global::ERP_NOMINAS.Properties.Resources.icons8_guardar_28;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button2.Location = new System.Drawing.Point(193, 69);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 51);
            this.button2.TabIndex = 2;
            this.button2.Text = "Guardar";
            this.button2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // SkipSavingMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(373, 669);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonFDelete1);
            this.Controls.Add(this.buttonAdd1);
            this.Controls.Add(this.buttonClose1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.filterByT1);
            this.Name = "SkipSavingMain";
            this.Text = "Omitir Ahorro Voluntario 10%";
            this.Load += new System.EventHandler(this.SkipSavingMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Components.Search.FilterByT filterByT1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private Components.Buttons.buttonClose buttonClose1;
        private Components.Buttons.buttonAdd buttonAdd1;
        private Components.Buttons.buttonFDelete buttonFDelete1;
        private System.Windows.Forms.GroupBox groupBox1;
        private Components.Search.SearchCatalog searchCatalog1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
    }
}