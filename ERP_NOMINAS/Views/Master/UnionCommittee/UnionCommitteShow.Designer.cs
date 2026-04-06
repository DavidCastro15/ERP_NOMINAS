namespace ERP_NOMINAS.Views.Master.UnionCommittee
{
    partial class UnionCommitteShow
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
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.kitCrud1 = new ERP_NOMINAS.Components.Buttons.KitCrud();
            this.searchCatalog1 = new ERP_NOMINAS.Components.Search.SearchCatalog();
            this.searchCatalog2 = new ERP_NOMINAS.Components.Search.SearchCatalog();
            this.searchCatalog3 = new ERP_NOMINAS.Components.Search.SearchCatalog();
            this.searchCatalog4 = new ERP_NOMINAS.Components.Search.SearchCatalog();
            this.searchCatalog5 = new ERP_NOMINAS.Components.Search.SearchCatalog();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 213);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Porcentaje:";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(103, 211);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(65, 20);
            this.numericUpDown1.TabIndex = 8;
            // 
            // kitCrud1
            // 
            this.kitCrud1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.kitCrud1.Location = new System.Drawing.Point(189, 236);
            this.kitCrud1.Name = "kitCrud1";
            this.kitCrud1.Size = new System.Drawing.Size(152, 52);
            this.kitCrud1.TabIndex = 20;
            this.kitCrud1.Save += new System.EventHandler(this.kitCrud1_Save);
            this.kitCrud1.Edit += new System.EventHandler(this.kitCrud1_Edit);
            // 
            // searchCatalog1
            // 
            this.searchCatalog1._TypeCatalog = ERP_NOMINAS.GlobalFunctions.Utilities.TypeCatalog.Employee;
            this.searchCatalog1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.searchCatalog1.Location = new System.Drawing.Point(16, 21);
            this.searchCatalog1.Name = "searchCatalog1";
            this.searchCatalog1.SelectedValue = null;
            this.searchCatalog1.Size = new System.Drawing.Size(325, 29);
            this.searchCatalog1.TabIndex = 26;
            this.searchCatalog1.TittleChange = "No. Empleado:";
            // 
            // searchCatalog2
            // 
            this.searchCatalog2._TypeCatalog = ERP_NOMINAS.GlobalFunctions.Utilities.TypeCatalog.Category;
            this.searchCatalog2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.searchCatalog2.Location = new System.Drawing.Point(16, 56);
            this.searchCatalog2.Name = "searchCatalog2";
            this.searchCatalog2.SelectedValue = null;
            this.searchCatalog2.Size = new System.Drawing.Size(325, 29);
            this.searchCatalog2.TabIndex = 27;
            this.searchCatalog2.TittleChange = "Categoria Zafra:";
            // 
            // searchCatalog3
            // 
            this.searchCatalog3._TypeCatalog = ERP_NOMINAS.GlobalFunctions.Utilities.TypeCatalog.Category;
            this.searchCatalog3.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.searchCatalog3.Location = new System.Drawing.Point(16, 91);
            this.searchCatalog3.Name = "searchCatalog3";
            this.searchCatalog3.SelectedValue = null;
            this.searchCatalog3.Size = new System.Drawing.Size(325, 29);
            this.searchCatalog3.TabIndex = 28;
            this.searchCatalog3.TittleChange = "Categoria Rep:";
            // 
            // searchCatalog4
            // 
            this.searchCatalog4._TypeCatalog = ERP_NOMINAS.GlobalFunctions.Utilities.TypeCatalog.Concept;
            this.searchCatalog4.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.searchCatalog4.Location = new System.Drawing.Point(16, 128);
            this.searchCatalog4.Name = "searchCatalog4";
            this.searchCatalog4.SelectedValue = null;
            this.searchCatalog4.Size = new System.Drawing.Size(325, 29);
            this.searchCatalog4.TabIndex = 29;
            this.searchCatalog4.TittleChange = "Concepto:";
            // 
            // searchCatalog5
            // 
            this.searchCatalog5._TypeCatalog = ERP_NOMINAS.GlobalFunctions.Utilities.TypeCatalog.Use;
            this.searchCatalog5.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.searchCatalog5.Location = new System.Drawing.Point(16, 163);
            this.searchCatalog5.Name = "searchCatalog5";
            this.searchCatalog5.SelectedValue = null;
            this.searchCatalog5.Size = new System.Drawing.Size(325, 29);
            this.searchCatalog5.TabIndex = 30;
            this.searchCatalog5.TittleChange = "Uso:";
            // 
            // UnionCommitteShow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(351, 288);
            this.Controls.Add(this.searchCatalog5);
            this.Controls.Add(this.searchCatalog4);
            this.Controls.Add(this.searchCatalog3);
            this.Controls.Add(this.searchCatalog2);
            this.Controls.Add(this.searchCatalog1);
            this.Controls.Add(this.kitCrud1);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.label2);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "UnionCommitteShow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Comite Sindical";
            this.Load += new System.EventHandler(this.UnionCommitteShow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private Components.Buttons.KitCrud kitCrud1;
        private Components.Search.SearchCatalog searchCatalog1;
        private Components.Search.SearchCatalog searchCatalog2;
        private Components.Search.SearchCatalog searchCatalog3;
        private Components.Search.SearchCatalog searchCatalog4;
        private Components.Search.SearchCatalog searchCatalog5;
    }
}