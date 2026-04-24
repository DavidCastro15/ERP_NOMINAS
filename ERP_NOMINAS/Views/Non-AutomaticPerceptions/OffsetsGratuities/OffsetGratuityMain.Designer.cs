namespace ERP_NOMINAS.Views.Non_AutomaticPerceptions.OffsetsGratuities
{
    partial class OffsetGratuityMain
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
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kitForm1 = new ERP_NOMINAS.Components.Buttons.KitForm();
            this.filterByT1 = new ERP_NOMINAS.Components.Search.FilterByT();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonPrint1 = new ERP_NOMINAS.Components.Buttons.buttonPrint();
            this.searchPayWeek2 = new ERP_NOMINAS.Components.Search.SearchPayWeek();
            this.buttonPrint2 = new ERP_NOMINAS.Components.Buttons.buttonPrint();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.searchPayWeek1 = new ERP_NOMINAS.Components.Search.SearchPayWeek();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column9,
            this.Column1,
            this.Column2,
            this.Column8,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7});
            this.dataGridView1.Location = new System.Drawing.Point(12, 74);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(955, 295);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // Column9
            // 
            this.Column9.HeaderText = "Id";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.Visible = false;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Id Nomina";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "N. Empleado";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "Nombre Completo";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Width = 200;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Importe";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Ciclo";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Periodo";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Uso";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Comentarios";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Width = 150;
            // 
            // kitForm1
            // 
            this.kitForm1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.kitForm1.Location = new System.Drawing.Point(650, 388);
            this.kitForm1.Name = "kitForm1";
            this.kitForm1.Size = new System.Drawing.Size(317, 55);
            this.kitForm1.TabIndex = 2;
            this.kitForm1.Add += new System.EventHandler(this.kitForm1_Add);
            this.kitForm1.MEdit += new System.EventHandler(this.kitForm1_MEdit);
            this.kitForm1.MDelete += new System.EventHandler(this.kitForm1_MDelete);
            // 
            // filterByT1
            // 
            this.filterByT1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.filterByT1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.filterByT1.FilterBy = null;
            this.filterByT1.Location = new System.Drawing.Point(12, 12);
            this.filterByT1.Name = "filterByT1";
            this.filterByT1.Size = new System.Drawing.Size(287, 49);
            this.filterByT1.TabIndex = 4;
            this.filterByT1.TittleChange = "Nombre o Semana Pago:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonPrint1);
            this.groupBox1.Controls.Add(this.searchPayWeek1);
            this.groupBox1.Location = new System.Drawing.Point(12, 375);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(229, 68);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Informe Cifras";
            // 
            // buttonPrint1
            // 
            this.buttonPrint1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonPrint1.Location = new System.Drawing.Point(149, 13);
            this.buttonPrint1.Name = "buttonPrint1";
            this.buttonPrint1.Size = new System.Drawing.Size(74, 49);
            this.buttonPrint1.TabIndex = 2;
            this.buttonPrint1.OnBotonPrintClick += new System.EventHandler(this.buttonPrint1_OnBotonPrintClick);
            // 
            // searchPayWeek2
            // 
            this.searchPayWeek2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.searchPayWeek2.Location = new System.Drawing.Point(19, 31);
            this.searchPayWeek2.Name = "searchPayWeek2";
            this.searchPayWeek2.SelectedValue = new decimal(new int[] {
            2026170,
            0,
            0,
            0});
            this.searchPayWeek2.Size = new System.Drawing.Size(126, 27);
            this.searchPayWeek2.TabIndex = 2;
            // 
            // buttonPrint2
            // 
            this.buttonPrint2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonPrint2.Location = new System.Drawing.Point(151, 13);
            this.buttonPrint2.Name = "buttonPrint2";
            this.buttonPrint2.Size = new System.Drawing.Size(74, 49);
            this.buttonPrint2.TabIndex = 2;
            this.buttonPrint2.OnBotonPrintClick += new System.EventHandler(this.buttonPrint2_OnBotonPrintClick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonPrint2);
            this.groupBox2.Controls.Add(this.searchPayWeek2);
            this.groupBox2.Location = new System.Drawing.Point(260, 375);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(232, 68);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Informe Desgaste Herramienta";
            // 
            // searchPayWeek1
            // 
            this.searchPayWeek1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.searchPayWeek1.Location = new System.Drawing.Point(6, 31);
            this.searchPayWeek1.Name = "searchPayWeek1";
            this.searchPayWeek1.SelectedValue = new decimal(new int[] {
            2026170,
            0,
            0,
            0});
            this.searchPayWeek1.Size = new System.Drawing.Size(126, 27);
            this.searchPayWeek1.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Image = global::ERP_NOMINAS.Properties.Resources.icons8_herramienta_28;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button1.Location = new System.Drawing.Point(498, 388);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(84, 49);
            this.button1.TabIndex = 7;
            this.button1.Text = "D.Herramienta";
            this.button1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            // 
            // OffsetGratuityMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(975, 445);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.filterByT1);
            this.Controls.Add(this.kitForm1);
            this.Controls.Add(this.dataGridView1);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "OffsetGratuityMain";
            this.Text = "Compensaciones";
            this.Load += new System.EventHandler(this.OffsetMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private Components.Buttons.KitForm kitForm1;
        private Components.Search.FilterByT filterByT1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.GroupBox groupBox1;
        private Components.Buttons.buttonPrint buttonPrint2;
        private Components.Search.SearchPayWeek searchPayWeek2;
        private System.Windows.Forms.GroupBox groupBox2;
        private Components.Buttons.buttonPrint buttonPrint1;
        private Components.Search.SearchPayWeek searchPayWeek1;
        private System.Windows.Forms.Button button1;
    }
}