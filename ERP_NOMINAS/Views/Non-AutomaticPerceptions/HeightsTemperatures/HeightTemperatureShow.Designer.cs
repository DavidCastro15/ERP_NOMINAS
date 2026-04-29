namespace ERP_NOMINAS.Views.Non_AutomaticPerceptions.HeightsTemperatures
{
    partial class HeightTemperatureShow
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
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.selectPayWeek1 = new ERP_NOMINAS.Components.ItemForms.SelectPayWeek();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonAdd1 = new ERP_NOMINAS.Components.Buttons.buttonAdd();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.searchCatalog3 = new ERP_NOMINAS.Components.Search.SearchCatalog();
            this.searchCatalog2 = new ERP_NOMINAS.Components.Search.SearchCatalog();
            this.searchCatalog1 = new ERP_NOMINAS.Components.Search.SearchCatalog();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.kitCrud1 = new ERP_NOMINAS.Components.Buttons.KitCrud();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.selectCycle1 = new ERP_NOMINAS.Components.ItemForms.SelectCycle();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "yyyy-MM-dd";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(58, 25);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(92, 20);
            this.dateTimePicker1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Fecha:";
            // 
            // selectPayWeek1
            // 
            this.selectPayWeek1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.selectPayWeek1.Location = new System.Drawing.Point(167, 20);
            this.selectPayWeek1.Name = "selectPayWeek1";
            this.selectPayWeek1.PayWeek = 0;
            this.selectPayWeek1.PayWeekType = 0;
            this.selectPayWeek1.Size = new System.Drawing.Size(126, 28);
            this.selectPayWeek1.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonAdd1);
            this.groupBox2.Controls.Add(this.numericUpDown1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.searchCatalog3);
            this.groupBox2.Controls.Add(this.searchCatalog2);
            this.groupBox2.Controls.Add(this.searchCatalog1);
            this.groupBox2.Location = new System.Drawing.Point(15, 70);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(565, 123);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Agregar  Empleado";
            // 
            // buttonAdd1
            // 
            this.buttonAdd1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonAdd1.Location = new System.Drawing.Point(480, 68);
            this.buttonAdd1.Name = "buttonAdd1";
            this.buttonAdd1.Size = new System.Drawing.Size(74, 50);
            this.buttonAdd1.TabIndex = 6;
            this.buttonAdd1.OnBotonAddClick += new System.EventHandler(this.buttonAdd1_OnBotonAddClick);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.DecimalPlaces = 2;
            this.numericUpDown1.Location = new System.Drawing.Point(480, 42);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(74, 20);
            this.numericUpDown1.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(499, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Horas:";
            // 
            // searchCatalog3
            // 
            this.searchCatalog3._TypeCatalog = ERP_NOMINAS.GlobalFunctions.Utilities.TypeCatalog.Category;
            this.searchCatalog3.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.searchCatalog3.Location = new System.Drawing.Point(6, 89);
            this.searchCatalog3.Name = "searchCatalog3";
            this.searchCatalog3.SelectedValue = null;
            this.searchCatalog3.Size = new System.Drawing.Size(454, 29);
            this.searchCatalog3.TabIndex = 3;
            this.searchCatalog3.TittleChange = "Categoria:";
            // 
            // searchCatalog2
            // 
            this.searchCatalog2._TypeCatalog = ERP_NOMINAS.GlobalFunctions.Utilities.TypeCatalog.Use;
            this.searchCatalog2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.searchCatalog2.Location = new System.Drawing.Point(6, 54);
            this.searchCatalog2.Name = "searchCatalog2";
            this.searchCatalog2.SelectedValue = null;
            this.searchCatalog2.Size = new System.Drawing.Size(454, 29);
            this.searchCatalog2.TabIndex = 2;
            this.searchCatalog2.TittleChange = "Uso:";
            // 
            // searchCatalog1
            // 
            this.searchCatalog1._TypeCatalog = ERP_NOMINAS.GlobalFunctions.Utilities.TypeCatalog.Employee;
            this.searchCatalog1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.searchCatalog1.Location = new System.Drawing.Point(6, 19);
            this.searchCatalog1.Name = "searchCatalog1";
            this.searchCatalog1.SelectedValue = null;
            this.searchCatalog1.Size = new System.Drawing.Size(454, 29);
            this.searchCatalog1.TabIndex = 1;
            this.searchCatalog1.TittleChange = "Empleado:";
            this.searchCatalog1.OnItemSelected += new System.EventHandler(this.searchCatalog1_OnItemSelected);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column7,
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6});
            this.dataGridView1.Location = new System.Drawing.Point(15, 199);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(685, 240);
            this.dataGridView1.TabIndex = 5;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "Id";
            this.Column7.HeaderText = "Id";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Visible = false;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "NumberEmployee";
            this.Column1.HeaderText = "N. Empleado";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "FullName";
            this.Column2.HeaderText = "Nombre Completo";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 180;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "CategoryId";
            this.Column3.HeaderText = "Categoria";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "Hours";
            this.Column4.HeaderText = "Horas";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "Use";
            this.Column5.HeaderText = "Uso";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Eliminar";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Text = "Eliminar";
            this.Column6.UseColumnTextForButtonValue = true;
            // 
            // kitCrud1
            // 
            this.kitCrud1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.kitCrud1.Location = new System.Drawing.Point(548, 445);
            this.kitCrud1.Name = "kitCrud1";
            this.kitCrud1.Size = new System.Drawing.Size(152, 52);
            this.kitCrud1.TabIndex = 6;
            this.kitCrud1.Save += new System.EventHandler(this.kitCrud1_Save);
            this.kitCrud1.Edit += new System.EventHandler(this.kitCrud1_Edit);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBox1);
            this.groupBox3.Location = new System.Drawing.Point(583, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(116, 48);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Folio:";
            this.groupBox3.Visible = false;
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(10, 19);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.textBox2);
            this.groupBox4.Location = new System.Drawing.Point(586, 138);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(116, 50);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Empleados Totales:";
            // 
            // textBox2
            // 
            this.textBox2.Enabled = false;
            this.textBox2.Location = new System.Drawing.Point(10, 24);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 1;
            // 
            // selectCycle1
            // 
            this.selectCycle1.Location = new System.Drawing.Point(301, 5);
            this.selectCycle1.Name = "selectCycle1";
            this.selectCycle1.SelectValue = "Zafra";
            this.selectCycle1.Size = new System.Drawing.Size(92, 59);
            this.selectCycle1.TabIndex = 9;
            // 
            // HeightTemperatureShow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(711, 500);
            this.Controls.Add(this.selectCycle1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.kitCrud1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.selectPayWeek1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateTimePicker1);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "HeightTemperatureShow";
            this.Text = "Alturas y Temperaturas";
            this.Load += new System.EventHandler(this.HeightTemperatureShow_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label1;
        private Components.ItemForms.SelectPayWeek selectPayWeek1;
        private System.Windows.Forms.GroupBox groupBox2;
        private Components.Search.SearchCatalog searchCatalog3;
        private Components.Search.SearchCatalog searchCatalog2;
        private Components.Search.SearchCatalog searchCatalog1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label2;
        private Components.Buttons.buttonAdd buttonAdd1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private Components.Buttons.KitCrud kitCrud1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewButtonColumn Column6;
        private Components.ItemForms.SelectCycle selectCycle1;
    }
}