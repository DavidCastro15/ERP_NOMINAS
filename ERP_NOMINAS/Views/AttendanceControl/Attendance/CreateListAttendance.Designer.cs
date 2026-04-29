namespace ERP_NOMINAS.Views.AttendanceControl.Attendance
{
    partial class CreateListAttendance
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
            this.buttonClose1 = new ERP_NOMINAS.Components.Buttons.buttonClose();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.searchTurn1 = new ERP_NOMINAS.Components.SearchTurn();
            this.button1 = new System.Windows.Forms.Button();
            this.selectPayWeek1 = new ERP_NOMINAS.Components.ItemForms.SelectPayWeek();
            this.selectCycle1 = new ERP_NOMINAS.Components.ItemForms.SelectCycle();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonClose1
            // 
            this.buttonClose1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonClose1.Location = new System.Drawing.Point(172, 287);
            this.buttonClose1.Name = "buttonClose1";
            this.buttonClose1.Size = new System.Drawing.Size(73, 49);
            this.buttonClose1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Location = new System.Drawing.Point(10, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(116, 65);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Estado";
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(6, 42);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(95, 17);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.Text = "Solo el 66.66%";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(6, 19);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(58, 17);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Normal";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 144);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Fecha Lista:";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "yyyy-MM-dd";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(95, 137);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(92, 20);
            this.dateTimePicker1.TabIndex = 6;
            // 
            // searchTurn1
            // 
            this.searchTurn1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.searchTurn1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.searchTurn1.Location = new System.Drawing.Point(9, 172);
            this.searchTurn1.Name = "searchTurn1";
            this.searchTurn1.SelectedTurntId = 1;
            this.searchTurn1.Size = new System.Drawing.Size(101, 35);
            this.searchTurn1.TabIndex = 7;
            this.searchTurn1.TittleLabelTurn = "Turno:";
            // 
            // button1
            // 
            this.button1.Image = global::ERP_NOMINAS.Properties.Resources.icons8_asistencia_28;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button1.Location = new System.Drawing.Point(15, 213);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(230, 57);
            this.button1.TabIndex = 8;
            this.button1.Text = "Crear Lista de Asistencia";
            this.button1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // selectPayWeek1
            // 
            this.selectPayWeek1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.selectPayWeek1.Location = new System.Drawing.Point(16, 103);
            this.selectPayWeek1.Name = "selectPayWeek1";
            this.selectPayWeek1.PayWeek = 0;
            this.selectPayWeek1.PayWeekType = 0;
            this.selectPayWeek1.Size = new System.Drawing.Size(126, 28);
            this.selectPayWeek1.TabIndex = 9;
            // 
            // selectCycle1
            // 
            this.selectCycle1.Location = new System.Drawing.Point(132, 24);
            this.selectCycle1.Name = "selectCycle1";
            this.selectCycle1.SelectValue = "Zafra";
            this.selectCycle1.Size = new System.Drawing.Size(92, 65);
            this.selectCycle1.TabIndex = 10;
            // 
            // CreateListAttendance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(252, 341);
            this.Controls.Add(this.selectCycle1);
            this.Controls.Add(this.selectPayWeek1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.searchTurn1);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonClose1);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "CreateListAttendance";
            this.Text = "Crear Lista de Asistencia";
            this.Load += new System.EventHandler(this.CreateListAttendance_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Components.Buttons.buttonClose buttonClose1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private Components.SearchTurn searchTurn1;
        private System.Windows.Forms.Button button1;
        private Components.ItemForms.SelectPayWeek selectPayWeek1;
        private Components.ItemForms.SelectCycle selectCycle1;
    }
}