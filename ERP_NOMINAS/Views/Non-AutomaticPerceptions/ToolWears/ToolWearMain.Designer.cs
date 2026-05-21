namespace ERP_NOMINAS.Views.Non_AutomaticPerceptions.ToolWears
{
    partial class ToolWearMain
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
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonImportDb1 = new ERP_NOMINAS.Components.Buttons.buttonImportDb();
            this.dateRange1 = new ERP_NOMINAS.Components.Search.DateRange();
            this.kitForm1 = new ERP_NOMINAS.Components.Buttons.KitForm();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonSave1 = new ERP_NOMINAS.Components.Buttons.buttonSave();
            this.selectPayWeek1 = new ERP_NOMINAS.Components.ItemForms.SelectPayWeek();
            this.buttonPrint1 = new ERP_NOMINAS.Components.Buttons.buttonPrint();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
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
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7});
            this.dataGridView1.Location = new System.Drawing.Point(12, 98);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(805, 415);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "No. Empleado";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Nombre Completo";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 200;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Dias";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 60;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Id Cat.";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 80;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Nombre Categoria";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 200;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Importe";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 80;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Uso";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Width = 80;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonImportDb1);
            this.groupBox1.Controls.Add(this.dateRange1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(224, 80);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Cargar Listas";
            // 
            // buttonImportDb1
            // 
            this.buttonImportDb1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonImportDb1.Location = new System.Drawing.Point(154, 17);
            this.buttonImportDb1.Name = "buttonImportDb1";
            this.buttonImportDb1.Size = new System.Drawing.Size(58, 53);
            this.buttonImportDb1.TabIndex = 1;
            this.buttonImportDb1.OnBotonImportDbClick += new System.EventHandler(this.buttonImportDb1_OnBotonImportDbClick);
            // 
            // dateRange1
            // 
            this.dateRange1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.dateRange1.EndDate = new System.DateTime(2026, 5, 20, 12, 23, 7, 776);
            this.dateRange1.Location = new System.Drawing.Point(6, 19);
            this.dateRange1.Name = "dateRange1";
            this.dateRange1.Size = new System.Drawing.Size(133, 51);
            this.dateRange1.StartDate = new System.DateTime(2026, 5, 20, 12, 23, 7, 776);
            this.dateRange1.TabIndex = 0;
            // 
            // kitForm1
            // 
            this.kitForm1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.kitForm1.Location = new System.Drawing.Point(500, 533);
            this.kitForm1.Name = "kitForm1";
            this.kitForm1.Size = new System.Drawing.Size(317, 55);
            this.kitForm1.TabIndex = 2;
            this.kitForm1.Add += new System.EventHandler(this.kitForm1_Add);
            this.kitForm1.MEdit += new System.EventHandler(this.kitForm1_MEdit);
            this.kitForm1.MDelete += new System.EventHandler(this.kitForm1_MDelete);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonSave1);
            this.groupBox2.Controls.Add(this.selectPayWeek1);
            this.groupBox2.Location = new System.Drawing.Point(12, 519);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(246, 69);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Guardar Desgaste Herramienta";
            // 
            // buttonSave1
            // 
            this.buttonSave1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonSave1.Location = new System.Drawing.Point(168, 15);
            this.buttonSave1.Name = "buttonSave1";
            this.buttonSave1.Size = new System.Drawing.Size(72, 48);
            this.buttonSave1.TabIndex = 1;
            this.buttonSave1.VisibleBotonSave = false;
            this.buttonSave1.OnBotonSaveClick += new System.EventHandler(this.buttonSave1_OnBotonSaveClick);
            // 
            // selectPayWeek1
            // 
            this.selectPayWeek1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.selectPayWeek1.Location = new System.Drawing.Point(6, 30);
            this.selectPayWeek1.Name = "selectPayWeek1";
            this.selectPayWeek1.PayWeek = 0;
            this.selectPayWeek1.PayWeekType = 0;
            this.selectPayWeek1.Size = new System.Drawing.Size(126, 28);
            this.selectPayWeek1.TabIndex = 0;
            // 
            // buttonPrint1
            // 
            this.buttonPrint1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonPrint1.Location = new System.Drawing.Point(18, 21);
            this.buttonPrint1.Name = "buttonPrint1";
            this.buttonPrint1.Size = new System.Drawing.Size(74, 49);
            this.buttonPrint1.TabIndex = 4;
            this.buttonPrint1.OnBotonPrintClick += new System.EventHandler(this.buttonPrint1_OnBotonPrintClick);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.buttonPrint1);
            this.groupBox3.Location = new System.Drawing.Point(707, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(110, 80);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Reporte Temporal";
            // 
            // ToolWearMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(831, 600);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.kitForm1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridView1);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "ToolWearMain";
            this.Text = "Desgaste de Herramientas";
            this.Load += new System.EventHandler(this.ToolWearMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private Components.Buttons.buttonImportDb buttonImportDb1;
        private Components.Search.DateRange dateRange1;
        private Components.Buttons.KitForm kitForm1;
        private System.Windows.Forms.GroupBox groupBox2;
        private Components.ItemForms.SelectPayWeek selectPayWeek1;
        private Components.Buttons.buttonSave buttonSave1;
        private Components.Buttons.buttonPrint buttonPrint1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
    }
}