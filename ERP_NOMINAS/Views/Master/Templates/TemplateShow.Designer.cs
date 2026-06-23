namespace ERP_NOMINAS.Views.Master.Templates
{
    partial class TemplateShow
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.kitCrud1 = new ERP_NOMINAS.Components.Buttons.KitCrud();
            this.searchCatalog1 = new ERP_NOMINAS.Components.Search.SearchCatalog();
            this.searchCatalog2 = new ERP_NOMINAS.Components.Search.SearchCatalog();
            this.searchCatalog3 = new ERP_NOMINAS.Components.Search.SearchCatalog();
            this.searchCatalog4 = new ERP_NOMINAS.Components.Search.SearchCatalog();
            this.searchTurn1 = new ERP_NOMINAS.Components.SearchTurn();
            this.searchTurn2 = new ERP_NOMINAS.Components.SearchTurn();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Location = new System.Drawing.Point(338, 159);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(102, 73);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Estado";
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(6, 42);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(76, 17);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Cancelado";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(6, 19);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(72, 17);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Habilitado";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // kitCrud1
            // 
            this.kitCrud1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.kitCrud1.Location = new System.Drawing.Point(288, 252);
            this.kitCrud1.Name = "kitCrud1";
            this.kitCrud1.Size = new System.Drawing.Size(152, 52);
            this.kitCrud1.TabIndex = 13;
            this.kitCrud1.Save += new System.EventHandler(this.kitCrud1_Save);
            this.kitCrud1.Edit += new System.EventHandler(this.kitCrud1_Edit);
            // 
            // searchCatalog1
            // 
            this.searchCatalog1._TypeCatalog = ERP_SHARED.GlobalFunctions.Enums.TypeCatalog.Employee;
            this.searchCatalog1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.searchCatalog1.Location = new System.Drawing.Point(12, 12);
            this.searchCatalog1.Name = "searchCatalog1";
            this.searchCatalog1.SelectedValue = null;
            this.searchCatalog1.Size = new System.Drawing.Size(428, 29);
            this.searchCatalog1.TabIndex = 14;
            this.searchCatalog1.TittleChange = "No. Empleado:";
            // 
            // searchCatalog2
            // 
            this.searchCatalog2._TypeCatalog = ERP_SHARED.GlobalFunctions.Enums.TypeCatalog.Use;
            this.searchCatalog2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.searchCatalog2.Location = new System.Drawing.Point(12, 47);
            this.searchCatalog2.Name = "searchCatalog2";
            this.searchCatalog2.SelectedValue = null;
            this.searchCatalog2.Size = new System.Drawing.Size(428, 29);
            this.searchCatalog2.TabIndex = 15;
            this.searchCatalog2.TittleChange = "Uso:";
            // 
            // searchCatalog3
            // 
            this.searchCatalog3._TypeCatalog = ERP_SHARED.GlobalFunctions.Enums.TypeCatalog.Category;
            this.searchCatalog3.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.searchCatalog3.Location = new System.Drawing.Point(12, 82);
            this.searchCatalog3.Name = "searchCatalog3";
            this.searchCatalog3.SelectedValue = null;
            this.searchCatalog3.Size = new System.Drawing.Size(428, 29);
            this.searchCatalog3.TabIndex = 16;
            this.searchCatalog3.TittleChange = "Categoria Zafra:";
            // 
            // searchCatalog4
            // 
            this.searchCatalog4._TypeCatalog = ERP_SHARED.GlobalFunctions.Enums.TypeCatalog.Category;
            this.searchCatalog4.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.searchCatalog4.Location = new System.Drawing.Point(12, 117);
            this.searchCatalog4.Name = "searchCatalog4";
            this.searchCatalog4.SelectedValue = null;
            this.searchCatalog4.Size = new System.Drawing.Size(428, 29);
            this.searchCatalog4.TabIndex = 17;
            this.searchCatalog4.TittleChange = "Categoria Rep.:";
            // 
            // searchTurn1
            // 
            this.searchTurn1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.searchTurn1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.searchTurn1.Location = new System.Drawing.Point(12, 160);
            this.searchTurn1.Name = "searchTurn1";
            this.searchTurn1.SelectedTurntId = 1;
            this.searchTurn1.Size = new System.Drawing.Size(149, 35);
            this.searchTurn1.TabIndex = 18;
            this.searchTurn1.TittleLabelTurn = "Turno Periodo:    ";
            // 
            // searchTurn2
            // 
            this.searchTurn2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.searchTurn2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.searchTurn2.Location = new System.Drawing.Point(12, 201);
            this.searchTurn2.Name = "searchTurn2";
            this.searchTurn2.SelectedTurntId = 1;
            this.searchTurn2.Size = new System.Drawing.Size(149, 35);
            this.searchTurn2.TabIndex = 19;
            this.searchTurn2.TittleLabelTurn = "Turno Trabajado:";
            // 
            // TemplateShow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(446, 304);
            this.Controls.Add(this.searchTurn2);
            this.Controls.Add(this.searchTurn1);
            this.Controls.Add(this.searchCatalog4);
            this.Controls.Add(this.searchCatalog3);
            this.Controls.Add(this.searchCatalog2);
            this.Controls.Add(this.searchCatalog1);
            this.Controls.Add(this.kitCrud1);
            this.Controls.Add(this.groupBox1);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "TemplateShow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Plantilla";
            this.Load += new System.EventHandler(this.TemplateShow_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private Components.Buttons.KitCrud kitCrud1;
        private Components.Search.SearchCatalog searchCatalog1;
        private Components.Search.SearchCatalog searchCatalog2;
        private Components.Search.SearchCatalog searchCatalog3;
        private Components.Search.SearchCatalog searchCatalog4;
        private Components.SearchTurn searchTurn1;
        private Components.SearchTurn searchTurn2;
    }
}