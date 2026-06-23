namespace ERP_NOMINAS.Views.Non_AutomaticPerceptions.Subsidies
{
    partial class SubsidyShow
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
            this.selectPayWeek1 = new ERP_NOMINAS.Components.ItemForms.SelectPayWeek();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.kitCrud1 = new ERP_NOMINAS.Components.Buttons.KitCrud();
            this.selectCycle1 = new ERP_NOMINAS.Components.ItemForms.SelectCycle();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // searchCatalog1
            // 
            this.searchCatalog1._TypeCatalog = ERP_SHARED.GlobalFunctions.Enums.TypeCatalog.Employee;
            this.searchCatalog1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.searchCatalog1.Location = new System.Drawing.Point(12, 27);
            this.searchCatalog1.Name = "searchCatalog1";
            this.searchCatalog1.SelectedValue = null;
            this.searchCatalog1.Size = new System.Drawing.Size(392, 29);
            this.searchCatalog1.TabIndex = 0;
            this.searchCatalog1.TittleChange = "N. Empleado:";
            // 
            // searchCatalog2
            // 
            this.searchCatalog2._TypeCatalog = ERP_SHARED.GlobalFunctions.Enums.TypeCatalog.Use;
            this.searchCatalog2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.searchCatalog2.Location = new System.Drawing.Point(12, 62);
            this.searchCatalog2.Name = "searchCatalog2";
            this.searchCatalog2.SelectedValue = null;
            this.searchCatalog2.Size = new System.Drawing.Size(392, 29);
            this.searchCatalog2.TabIndex = 1;
            this.searchCatalog2.TittleChange = "Uso:";
            // 
            // selectPayWeek1
            // 
            this.selectPayWeek1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.selectPayWeek1.Location = new System.Drawing.Point(15, 108);
            this.selectPayWeek1.Name = "selectPayWeek1";
            this.selectPayWeek1.PayWeek = 0;
            this.selectPayWeek1.PayWeekType = 0;
            this.selectPayWeek1.Size = new System.Drawing.Size(126, 28);
            this.selectPayWeek1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(162, 123);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Importe:";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.DecimalPlaces = 2;
            this.numericUpDown1.Location = new System.Drawing.Point(213, 116);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(105, 20);
            this.numericUpDown1.TabIndex = 4;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(15, 165);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(490, 96);
            this.richTextBox1.TabIndex = 6;
            this.richTextBox1.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 149);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Comentarios:";
            // 
            // kitCrud1
            // 
            this.kitCrud1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.kitCrud1.Location = new System.Drawing.Point(353, 267);
            this.kitCrud1.Name = "kitCrud1";
            this.kitCrud1.Size = new System.Drawing.Size(152, 52);
            this.kitCrud1.TabIndex = 8;
            this.kitCrud1.Save += new System.EventHandler(this.kitCrud1_Save);
            this.kitCrud1.Edit += new System.EventHandler(this.kitCrud1_Edit);
            // 
            // selectCycle1
            // 
            this.selectCycle1.Location = new System.Drawing.Point(410, 27);
            this.selectCycle1.Name = "selectCycle1";
            this.selectCycle1.SelectValue = "Zafra";
            this.selectCycle1.Size = new System.Drawing.Size(92, 59);
            this.selectCycle1.TabIndex = 9;
            // 
            // SubsidyShow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(513, 319);
            this.Controls.Add(this.selectCycle1);
            this.Controls.Add(this.kitCrud1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.selectPayWeek1);
            this.Controls.Add(this.searchCatalog2);
            this.Controls.Add(this.searchCatalog1);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "SubsidyShow";
            this.Text = "Subsidio";
            this.Load += new System.EventHandler(this.SubsidyShow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Components.Search.SearchCatalog searchCatalog1;
        private Components.Search.SearchCatalog searchCatalog2;
        private Components.ItemForms.SelectPayWeek selectPayWeek1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label2;
        private Components.Buttons.KitCrud kitCrud1;
        private Components.ItemForms.SelectCycle selectCycle1;
    }
}