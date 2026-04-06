namespace ERP_NOMINAS.Components.Buttons
{
    partial class KitForm
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
            this.buttonMEdit1 = new ERP_NOMINAS.Components.Buttons.buttonMEdit();
            this.buttonAdd1 = new ERP_NOMINAS.Components.Buttons.buttonAdd();
            this.buttonMDelete1 = new ERP_NOMINAS.Components.Buttons.buttonMDelete();
            this.buttonClose1 = new ERP_NOMINAS.Components.Buttons.buttonClose();
            this.SuspendLayout();
            // 
            // buttonMEdit1
            // 
            this.buttonMEdit1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonMEdit1.Location = new System.Drawing.Point(83, 3);
            this.buttonMEdit1.Name = "buttonMEdit1";
            this.buttonMEdit1.Size = new System.Drawing.Size(74, 50);
            this.buttonMEdit1.TabIndex = 1;
            // 
            // buttonAdd1
            // 
            this.buttonAdd1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonAdd1.Location = new System.Drawing.Point(3, 3);
            this.buttonAdd1.Name = "buttonAdd1";
            this.buttonAdd1.Size = new System.Drawing.Size(74, 50);
            this.buttonAdd1.TabIndex = 2;
            // 
            // buttonMDelete1
            // 
            this.buttonMDelete1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonMDelete1.Location = new System.Drawing.Point(163, 3);
            this.buttonMDelete1.Name = "buttonMDelete1";
            this.buttonMDelete1.Size = new System.Drawing.Size(73, 49);
            this.buttonMDelete1.TabIndex = 3;
            // 
            // buttonClose1
            // 
            this.buttonClose1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonClose1.Location = new System.Drawing.Point(242, 3);
            this.buttonClose1.Name = "buttonClose1";
            this.buttonClose1.Size = new System.Drawing.Size(73, 49);
            this.buttonClose1.TabIndex = 4;
            // 
            // KitForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Controls.Add(this.buttonClose1);
            this.Controls.Add(this.buttonMDelete1);
            this.Controls.Add(this.buttonAdd1);
            this.Controls.Add(this.buttonMEdit1);
            this.Name = "KitForm";
            this.Size = new System.Drawing.Size(317, 55);
            this.ResumeLayout(false);

        }

        #endregion
        private buttonMEdit buttonMEdit1;
        private buttonAdd buttonAdd1;
        private buttonMDelete buttonMDelete1;
        private buttonClose buttonClose1;
    }
}
