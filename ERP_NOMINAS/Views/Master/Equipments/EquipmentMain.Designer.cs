namespace ERP_NOMINAS.Views.Master.Equipments
{
    partial class EquipmentMain
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
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kitForm1 = new ERP_NOMINAS.Components.Buttons.KitForm();
            this.filterByT1 = new ERP_NOMINAS.Components.Search.FilterByT();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
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
            this.Column5});
            this.dataGridView1.Location = new System.Drawing.Point(12, 87);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(358, 379);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
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
            this.Column2.HeaderText = "Equipo";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Descripcion";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 150;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Grupo";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // kitForm1
            // 
            this.kitForm1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.kitForm1.Location = new System.Drawing.Point(53, 472);
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
            this.filterByT1.Size = new System.Drawing.Size(268, 49);
            this.filterByT1.TabIndex = 3;
            this.filterByT1.TittleChange = "Equipo:";
            // 
            // EquipmentMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 532);
            this.Controls.Add(this.filterByT1);
            this.Controls.Add(this.kitForm1);
            this.Controls.Add(this.dataGridView1);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "EquipmentMain";
            this.Text = " Equipos";
            this.Load += new System.EventHandler(this.EquipmentMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private Components.Buttons.KitForm kitForm1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private Components.Search.FilterByT filterByT1;
    }
}