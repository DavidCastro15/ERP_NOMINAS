namespace ERP_NOMINAS.Components.Buttons
{
    partial class KitCrud
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonSave1 = new ERP_NOMINAS.Components.Buttons.buttonSave();
            this.buttonFEdit1 = new ERP_NOMINAS.Components.Buttons.buttonFEdit();
            this.buttonClose1 = new ERP_NOMINAS.Components.Buttons.buttonClose();
            this.SuspendLayout();
            // 
            // buttonSave1
            // 
            this.buttonSave1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonSave1.Location = new System.Drawing.Point(0, 0);
            this.buttonSave1.Name = "buttonSave1";
            this.buttonSave1.Size = new System.Drawing.Size(72, 48);
            this.buttonSave1.TabIndex = 0;
            this.buttonSave1.VisibleBotonSave = true;
            // 
            // buttonFEdit1
            // 
            this.buttonFEdit1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonFEdit1.Location = new System.Drawing.Point(0, 0);
            this.buttonFEdit1.Name = "buttonFEdit1";
            this.buttonFEdit1.Size = new System.Drawing.Size(72, 49);
            this.buttonFEdit1.TabIndex = 2;
            this.buttonFEdit1.VisibleBotonEdit = true;
            // 
            // buttonClose1
            // 
            this.buttonClose1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonClose1.Location = new System.Drawing.Point(78, -1);
            this.buttonClose1.Name = "buttonClose1";
            this.buttonClose1.Size = new System.Drawing.Size(73, 49);
            this.buttonClose1.TabIndex = 1;
            // 
            // KitCrud
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Controls.Add(this.buttonClose1);
            this.Controls.Add(this.buttonSave1);
            this.Controls.Add(this.buttonFEdit1);
            this.Name = "KitCrud";
            this.Size = new System.Drawing.Size(152, 52);
            this.ResumeLayout(false);

        }

        #endregion

        private buttonSave buttonSave1;
        private buttonClose buttonClose1;
        private buttonFEdit buttonFEdit1;
    }
}
