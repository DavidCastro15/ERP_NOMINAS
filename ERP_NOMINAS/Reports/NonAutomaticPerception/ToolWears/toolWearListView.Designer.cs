namespace ERP_NOMINAS.Reports.NonAutomaticPerception.ToolWears
{
    partial class toolWearListView
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
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.ServerReport.BearerToken = null;
            // 
            // toolWearListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "toolWearListView";
            this.Load += new System.EventHandler(this.toolWearListView_Load);
            this.Shown += new System.EventHandler(this.toolWearListView_Shown);
            this.ResumeLayout(false);

        }

        #endregion
    }
}