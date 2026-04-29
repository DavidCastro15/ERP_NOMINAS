namespace ERP_NOMINAS.Views.Non_AutomaticPerceptions.OffsetsGratuities
{
    partial class OffsetGratuityShow
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonAdd1 = new ERP_NOMINAS.Components.Buttons.buttonAdd();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.searchCatalog2 = new ERP_NOMINAS.Components.Search.SearchCatalog();
            this.searchCatalog1 = new ERP_NOMINAS.Components.Search.SearchCatalog();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.kitCrud1 = new ERP_NOMINAS.Components.Buttons.KitCrud();
            this.selectCycle1 = new ERP_NOMINAS.Components.ItemForms.SelectCycle();
            this.selectPayWeek1 = new ERP_NOMINAS.Components.ItemForms.SelectPayWeek();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonAdd1);
            this.groupBox2.Controls.Add(this.richTextBox1);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.numericUpDown3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.searchCatalog2);
            this.groupBox2.Controls.Add(this.searchCatalog1);
            this.groupBox2.Location = new System.Drawing.Point(12, 81);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(530, 174);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Agregar Empleado";
            // 
            // buttonAdd1
            // 
            this.buttonAdd1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonAdd1.Location = new System.Drawing.Point(443, 117);
            this.buttonAdd1.Name = "buttonAdd1";
            this.buttonAdd1.Size = new System.Drawing.Size(74, 50);
            this.buttonAdd1.TabIndex = 6;
            this.buttonAdd1.OnBotonAddClick += new System.EventHandler(this.buttonAdd1_OnBotonAddClick);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(80, 99);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(342, 68);
            this.richTextBox1.TabIndex = 5;
            this.richTextBox1.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Comentarios:";
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.DecimalPlaces = 2;
            this.numericUpDown3.Location = new System.Drawing.Point(435, 54);
            this.numericUpDown3.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(89, 20);
            this.numericUpDown3.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(452, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Importe:";
            // 
            // searchCatalog2
            // 
            this.searchCatalog2._TypeCatalog = ERP_NOMINAS.GlobalFunctions.Utilities.TypeCatalog.Use;
            this.searchCatalog2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.searchCatalog2.Location = new System.Drawing.Point(6, 54);
            this.searchCatalog2.Name = "searchCatalog2";
            this.searchCatalog2.SelectedValue = null;
            this.searchCatalog2.Size = new System.Drawing.Size(416, 29);
            this.searchCatalog2.TabIndex = 1;
            this.searchCatalog2.TittleChange = "Uso:";
            // 
            // searchCatalog1
            // 
            this.searchCatalog1._TypeCatalog = ERP_NOMINAS.GlobalFunctions.Utilities.TypeCatalog.Employee;
            this.searchCatalog1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.searchCatalog1.Location = new System.Drawing.Point(6, 19);
            this.searchCatalog1.Name = "searchCatalog1";
            this.searchCatalog1.SelectedValue = null;
            this.searchCatalog1.Size = new System.Drawing.Size(416, 29);
            this.searchCatalog1.TabIndex = 0;
            this.searchCatalog1.TittleChange = "N. Empleado:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5});
            this.dataGridView1.Location = new System.Drawing.Point(12, 261);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(530, 200);
            this.dataGridView1.TabIndex = 5;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
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
            this.Column2.DataPropertyName = "Amount";
            this.Column2.HeaderText = "Importe";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "Use";
            this.Column3.HeaderText = "Uso";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "Comments";
            this.Column4.HeaderText = "Comentarios";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 165;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Eliminar";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Text = "Eliminar";
            this.Column5.UseColumnTextForButtonValue = true;
            this.Column5.Width = 60;
            // 
            // kitCrud1
            // 
            this.kitCrud1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.kitCrud1.Location = new System.Drawing.Point(390, 467);
            this.kitCrud1.Name = "kitCrud1";
            this.kitCrud1.Size = new System.Drawing.Size(152, 52);
            this.kitCrud1.TabIndex = 6;
            this.kitCrud1.Save += new System.EventHandler(this.kitCrud1_Save);
            // 
            // selectCycle1
            // 
            this.selectCycle1.Location = new System.Drawing.Point(18, 12);
            this.selectCycle1.Name = "selectCycle1";
            this.selectCycle1.SelectValue = "Zafra";
            this.selectCycle1.Size = new System.Drawing.Size(92, 59);
            this.selectCycle1.TabIndex = 7;
            // 
            // selectPayWeek1
            // 
            this.selectPayWeek1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.selectPayWeek1.Location = new System.Drawing.Point(116, 31);
            this.selectPayWeek1.Name = "selectPayWeek1";
            this.selectPayWeek1.PayWeek = 0;
            this.selectPayWeek1.PayWeekType = 0;
            this.selectPayWeek1.Size = new System.Drawing.Size(126, 28);
            this.selectPayWeek1.TabIndex = 8;
            // 
            // OffsetGratuityShow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(548, 521);
            this.Controls.Add(this.selectPayWeek1);
            this.Controls.Add(this.selectCycle1);
            this.Controls.Add(this.kitCrud1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox2);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "OffsetGratuityShow";
            this.Text = "Compensacion";
            this.Load += new System.EventHandler(this.OffsetShow_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox2;
        private Components.Buttons.buttonAdd buttonAdd1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDown3;
        private System.Windows.Forms.Label label2;
        private Components.Search.SearchCatalog searchCatalog2;
        private Components.Search.SearchCatalog searchCatalog1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private Components.Buttons.KitCrud kitCrud1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewButtonColumn Column5;
        private Components.ItemForms.SelectCycle selectCycle1;
        private Components.ItemForms.SelectPayWeek selectPayWeek1;
    }
}