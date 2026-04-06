namespace ERP_NOMINAS.Views.AttendanceControl.Attendance
{
    partial class AttendShow
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
            this.searchCatalog1 = new ERP_NOMINAS.Components.Search.SearchCatalog();
            this.searchCatalog2 = new ERP_NOMINAS.Components.Search.SearchCatalog();
            this.searchCatalog3 = new ERP_NOMINAS.Components.Search.SearchCatalog();
            this.searchTurn1 = new ERP_NOMINAS.Components.SearchTurn();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.kitCrud1 = new ERP_NOMINAS.Components.Buttons.KitCrud();
            this.SuspendLayout();
            // 
            // searchCatalog1
            // 
            this.searchCatalog1._TypeCatalog = ERP_NOMINAS.GlobalFunctions.Utilities.TypeCatalog.Employee;
            this.searchCatalog1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.searchCatalog1.Location = new System.Drawing.Point(12, 22);
            this.searchCatalog1.Name = "searchCatalog1";
            this.searchCatalog1.SelectedValue = null;
            this.searchCatalog1.Size = new System.Drawing.Size(428, 29);
            this.searchCatalog1.TabIndex = 2;
            this.searchCatalog1.TittleChange = "Empleado:";
            this.searchCatalog1.OnItemSelected += new System.EventHandler(this.searchCatalog1_OnItemSelected);
            // 
            // searchCatalog2
            // 
            this.searchCatalog2._TypeCatalog = ERP_NOMINAS.GlobalFunctions.Utilities.TypeCatalog.Category;
            this.searchCatalog2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.searchCatalog2.Location = new System.Drawing.Point(12, 57);
            this.searchCatalog2.Name = "searchCatalog2";
            this.searchCatalog2.SelectedValue = null;
            this.searchCatalog2.Size = new System.Drawing.Size(428, 29);
            this.searchCatalog2.TabIndex = 3;
            this.searchCatalog2.TittleChange = "Categoria:";
            // 
            // searchCatalog3
            // 
            this.searchCatalog3._TypeCatalog = ERP_NOMINAS.GlobalFunctions.Utilities.TypeCatalog.Use;
            this.searchCatalog3.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.searchCatalog3.Location = new System.Drawing.Point(12, 92);
            this.searchCatalog3.Name = "searchCatalog3";
            this.searchCatalog3.SelectedValue = null;
            this.searchCatalog3.Size = new System.Drawing.Size(428, 29);
            this.searchCatalog3.TabIndex = 4;
            this.searchCatalog3.TittleChange = "Uso:";
            // 
            // searchTurn1
            // 
            this.searchTurn1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.searchTurn1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.searchTurn1.Location = new System.Drawing.Point(12, 127);
            this.searchTurn1.Name = "searchTurn1";
            this.searchTurn1.SelectedTurntId = 1;
            this.searchTurn1.Size = new System.Drawing.Size(97, 35);
            this.searchTurn1.TabIndex = 5;
            this.searchTurn1.TittleLabelTurn = "Turno:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 175);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Fecha:";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "yyyy-MM-dd";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(55, 168);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(104, 20);
            this.dateTimePicker1.TabIndex = 7;
            // 
            // kitCrud1
            // 
            this.kitCrud1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.kitCrud1.Location = new System.Drawing.Point(283, 190);
            this.kitCrud1.Name = "kitCrud1";
            this.kitCrud1.Size = new System.Drawing.Size(152, 52);
            this.kitCrud1.TabIndex = 8;
            this.kitCrud1.Save += new System.EventHandler(this.kitCrud1_Save);
            this.kitCrud1.Edit += new System.EventHandler(this.kitCrud1_Edit);
            // 
            // AttendShow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(447, 245);
            this.Controls.Add(this.kitCrud1);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.searchTurn1);
            this.Controls.Add(this.searchCatalog3);
            this.Controls.Add(this.searchCatalog2);
            this.Controls.Add(this.searchCatalog1);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "AttendShow";
            this.Text = "Asistencia";
            this.Load += new System.EventHandler(this.AttendShow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Components.Search.SearchCatalog searchCatalog1;
        private Components.Search.SearchCatalog searchCatalog2;
        private Components.Search.SearchCatalog searchCatalog3;
        private Components.SearchTurn searchTurn1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private Components.Buttons.KitCrud kitCrud1;
    }
}