namespace ERP_NOMINAS.Views.Non_AutomaticPerceptions.FoodVauchers
{
    partial class FoodVaucherShow
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
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.CheckBox31 = new System.Windows.Forms.CheckBox();
            this.CheckBox21 = new System.Windows.Forms.CheckBox();
            this.CheckBox22 = new System.Windows.Forms.CheckBox();
            this.CheckBox23 = new System.Windows.Forms.CheckBox();
            this.CheckBox24 = new System.Windows.Forms.CheckBox();
            this.CheckBox25 = new System.Windows.Forms.CheckBox();
            this.CheckBox26 = new System.Windows.Forms.CheckBox();
            this.CheckBox27 = new System.Windows.Forms.CheckBox();
            this.CheckBox28 = new System.Windows.Forms.CheckBox();
            this.CheckBox29 = new System.Windows.Forms.CheckBox();
            this.CheckBox30 = new System.Windows.Forms.CheckBox();
            this.CheckBox11 = new System.Windows.Forms.CheckBox();
            this.CheckBox12 = new System.Windows.Forms.CheckBox();
            this.CheckBox13 = new System.Windows.Forms.CheckBox();
            this.CheckBox14 = new System.Windows.Forms.CheckBox();
            this.CheckBox15 = new System.Windows.Forms.CheckBox();
            this.CheckBox16 = new System.Windows.Forms.CheckBox();
            this.CheckBox17 = new System.Windows.Forms.CheckBox();
            this.CheckBox18 = new System.Windows.Forms.CheckBox();
            this.CheckBox19 = new System.Windows.Forms.CheckBox();
            this.CheckBox20 = new System.Windows.Forms.CheckBox();
            this.CheckBox10 = new System.Windows.Forms.CheckBox();
            this.CheckBox9 = new System.Windows.Forms.CheckBox();
            this.CheckBox8 = new System.Windows.Forms.CheckBox();
            this.CheckBox7 = new System.Windows.Forms.CheckBox();
            this.CheckBox6 = new System.Windows.Forms.CheckBox();
            this.CheckBox5 = new System.Windows.Forms.CheckBox();
            this.CheckBox4 = new System.Windows.Forms.CheckBox();
            this.CheckBox3 = new System.Windows.Forms.CheckBox();
            this.CheckBox2 = new System.Windows.Forms.CheckBox();
            this.CheckBox1 = new System.Windows.Forms.CheckBox();
            this.kitCrud1 = new ERP_NOMINAS.Components.Buttons.KitCrud();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            this.GroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // searchCatalog1
            // 
            this.searchCatalog1._TypeCatalog = ERP_NOMINAS.GlobalFunctions.Utilities.TypeCatalog.Employee;
            this.searchCatalog1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.searchCatalog1.Location = new System.Drawing.Point(12, 34);
            this.searchCatalog1.Name = "searchCatalog1";
            this.searchCatalog1.SelectedValue = null;
            this.searchCatalog1.Size = new System.Drawing.Size(336, 29);
            this.searchCatalog1.TabIndex = 0;
            this.searchCatalog1.TittleChange = "N. Empleado:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Dias Habiles:";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(103, 69);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(66, 20);
            this.numericUpDown1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 111);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Dias Laborales:";
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(103, 104);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(66, 20);
            this.numericUpDown2.TabIndex = 2;
            this.numericUpDown2.ValueChanged += new System.EventHandler(this.numericUpDown2_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 147);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Importe:";
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.DecimalPlaces = 2;
            this.numericUpDown3.Location = new System.Drawing.Point(103, 140);
            this.numericUpDown3.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(66, 20);
            this.numericUpDown3.TabIndex = 3;
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.CheckBox31);
            this.GroupBox1.Controls.Add(this.CheckBox21);
            this.GroupBox1.Controls.Add(this.CheckBox22);
            this.GroupBox1.Controls.Add(this.CheckBox23);
            this.GroupBox1.Controls.Add(this.CheckBox24);
            this.GroupBox1.Controls.Add(this.CheckBox25);
            this.GroupBox1.Controls.Add(this.CheckBox26);
            this.GroupBox1.Controls.Add(this.CheckBox27);
            this.GroupBox1.Controls.Add(this.CheckBox28);
            this.GroupBox1.Controls.Add(this.CheckBox29);
            this.GroupBox1.Controls.Add(this.CheckBox30);
            this.GroupBox1.Controls.Add(this.CheckBox11);
            this.GroupBox1.Controls.Add(this.CheckBox12);
            this.GroupBox1.Controls.Add(this.CheckBox13);
            this.GroupBox1.Controls.Add(this.CheckBox14);
            this.GroupBox1.Controls.Add(this.CheckBox15);
            this.GroupBox1.Controls.Add(this.CheckBox16);
            this.GroupBox1.Controls.Add(this.CheckBox17);
            this.GroupBox1.Controls.Add(this.CheckBox18);
            this.GroupBox1.Controls.Add(this.CheckBox19);
            this.GroupBox1.Controls.Add(this.CheckBox20);
            this.GroupBox1.Controls.Add(this.CheckBox10);
            this.GroupBox1.Controls.Add(this.CheckBox9);
            this.GroupBox1.Controls.Add(this.CheckBox8);
            this.GroupBox1.Controls.Add(this.CheckBox7);
            this.GroupBox1.Controls.Add(this.CheckBox6);
            this.GroupBox1.Controls.Add(this.CheckBox5);
            this.GroupBox1.Controls.Add(this.CheckBox4);
            this.GroupBox1.Controls.Add(this.CheckBox3);
            this.GroupBox1.Controls.Add(this.CheckBox2);
            this.GroupBox1.Controls.Add(this.CheckBox1);
            this.GroupBox1.Location = new System.Drawing.Point(22, 180);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(174, 262);
            this.GroupBox1.TabIndex = 47;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "Dias Trabajados";
            // 
            // CheckBox31
            // 
            this.CheckBox31.AutoSize = true;
            this.CheckBox31.Location = new System.Drawing.Point(121, 19);
            this.CheckBox31.Name = "CheckBox31";
            this.CheckBox31.Size = new System.Drawing.Size(38, 17);
            this.CheckBox31.TabIndex = 76;
            this.CheckBox31.Text = "31";
            this.CheckBox31.UseVisualStyleBackColor = true;
            // 
            // CheckBox21
            // 
            this.CheckBox21.AutoSize = true;
            this.CheckBox21.Location = new System.Drawing.Point(82, 19);
            this.CheckBox21.Name = "CheckBox21";
            this.CheckBox21.Size = new System.Drawing.Size(38, 17);
            this.CheckBox21.TabIndex = 75;
            this.CheckBox21.Text = "21";
            this.CheckBox21.UseVisualStyleBackColor = true;
            // 
            // CheckBox22
            // 
            this.CheckBox22.AutoSize = true;
            this.CheckBox22.Location = new System.Drawing.Point(82, 42);
            this.CheckBox22.Name = "CheckBox22";
            this.CheckBox22.Size = new System.Drawing.Size(38, 17);
            this.CheckBox22.TabIndex = 74;
            this.CheckBox22.Text = "22";
            this.CheckBox22.UseVisualStyleBackColor = true;
            // 
            // CheckBox23
            // 
            this.CheckBox23.AutoSize = true;
            this.CheckBox23.Location = new System.Drawing.Point(82, 65);
            this.CheckBox23.Name = "CheckBox23";
            this.CheckBox23.Size = new System.Drawing.Size(38, 17);
            this.CheckBox23.TabIndex = 73;
            this.CheckBox23.Text = "23";
            this.CheckBox23.UseVisualStyleBackColor = true;
            // 
            // CheckBox24
            // 
            this.CheckBox24.AutoSize = true;
            this.CheckBox24.Location = new System.Drawing.Point(82, 88);
            this.CheckBox24.Name = "CheckBox24";
            this.CheckBox24.Size = new System.Drawing.Size(38, 17);
            this.CheckBox24.TabIndex = 72;
            this.CheckBox24.Text = "24";
            this.CheckBox24.UseVisualStyleBackColor = true;
            // 
            // CheckBox25
            // 
            this.CheckBox25.AutoSize = true;
            this.CheckBox25.Location = new System.Drawing.Point(82, 111);
            this.CheckBox25.Name = "CheckBox25";
            this.CheckBox25.Size = new System.Drawing.Size(38, 17);
            this.CheckBox25.TabIndex = 71;
            this.CheckBox25.Text = "25";
            this.CheckBox25.UseVisualStyleBackColor = true;
            // 
            // CheckBox26
            // 
            this.CheckBox26.AutoSize = true;
            this.CheckBox26.Location = new System.Drawing.Point(82, 134);
            this.CheckBox26.Name = "CheckBox26";
            this.CheckBox26.Size = new System.Drawing.Size(38, 17);
            this.CheckBox26.TabIndex = 70;
            this.CheckBox26.Text = "26";
            this.CheckBox26.UseVisualStyleBackColor = true;
            // 
            // CheckBox27
            // 
            this.CheckBox27.AutoSize = true;
            this.CheckBox27.Location = new System.Drawing.Point(82, 157);
            this.CheckBox27.Name = "CheckBox27";
            this.CheckBox27.Size = new System.Drawing.Size(38, 17);
            this.CheckBox27.TabIndex = 69;
            this.CheckBox27.Text = "27";
            this.CheckBox27.UseVisualStyleBackColor = true;
            // 
            // CheckBox28
            // 
            this.CheckBox28.AutoSize = true;
            this.CheckBox28.Location = new System.Drawing.Point(82, 180);
            this.CheckBox28.Name = "CheckBox28";
            this.CheckBox28.Size = new System.Drawing.Size(38, 17);
            this.CheckBox28.TabIndex = 68;
            this.CheckBox28.Text = "28";
            this.CheckBox28.UseVisualStyleBackColor = true;
            // 
            // CheckBox29
            // 
            this.CheckBox29.AutoSize = true;
            this.CheckBox29.Location = new System.Drawing.Point(82, 203);
            this.CheckBox29.Name = "CheckBox29";
            this.CheckBox29.Size = new System.Drawing.Size(38, 17);
            this.CheckBox29.TabIndex = 67;
            this.CheckBox29.Text = "29";
            this.CheckBox29.UseVisualStyleBackColor = true;
            // 
            // CheckBox30
            // 
            this.CheckBox30.AutoSize = true;
            this.CheckBox30.Location = new System.Drawing.Point(82, 226);
            this.CheckBox30.Name = "CheckBox30";
            this.CheckBox30.Size = new System.Drawing.Size(38, 17);
            this.CheckBox30.TabIndex = 66;
            this.CheckBox30.Text = "30";
            this.CheckBox30.UseVisualStyleBackColor = true;
            // 
            // CheckBox11
            // 
            this.CheckBox11.AutoSize = true;
            this.CheckBox11.Location = new System.Drawing.Point(44, 19);
            this.CheckBox11.Name = "CheckBox11";
            this.CheckBox11.Size = new System.Drawing.Size(38, 17);
            this.CheckBox11.TabIndex = 65;
            this.CheckBox11.Text = "11";
            this.CheckBox11.UseVisualStyleBackColor = true;
            // 
            // CheckBox12
            // 
            this.CheckBox12.AutoSize = true;
            this.CheckBox12.Location = new System.Drawing.Point(44, 42);
            this.CheckBox12.Name = "CheckBox12";
            this.CheckBox12.Size = new System.Drawing.Size(38, 17);
            this.CheckBox12.TabIndex = 64;
            this.CheckBox12.Text = "12";
            this.CheckBox12.UseVisualStyleBackColor = true;
            // 
            // CheckBox13
            // 
            this.CheckBox13.AutoSize = true;
            this.CheckBox13.Location = new System.Drawing.Point(44, 65);
            this.CheckBox13.Name = "CheckBox13";
            this.CheckBox13.Size = new System.Drawing.Size(38, 17);
            this.CheckBox13.TabIndex = 63;
            this.CheckBox13.Text = "13";
            this.CheckBox13.UseVisualStyleBackColor = true;
            // 
            // CheckBox14
            // 
            this.CheckBox14.AutoSize = true;
            this.CheckBox14.Location = new System.Drawing.Point(44, 88);
            this.CheckBox14.Name = "CheckBox14";
            this.CheckBox14.Size = new System.Drawing.Size(38, 17);
            this.CheckBox14.TabIndex = 62;
            this.CheckBox14.Text = "14";
            this.CheckBox14.UseVisualStyleBackColor = true;
            // 
            // CheckBox15
            // 
            this.CheckBox15.AutoSize = true;
            this.CheckBox15.Location = new System.Drawing.Point(44, 111);
            this.CheckBox15.Name = "CheckBox15";
            this.CheckBox15.Size = new System.Drawing.Size(38, 17);
            this.CheckBox15.TabIndex = 61;
            this.CheckBox15.Text = "15";
            this.CheckBox15.UseVisualStyleBackColor = true;
            // 
            // CheckBox16
            // 
            this.CheckBox16.AutoSize = true;
            this.CheckBox16.Location = new System.Drawing.Point(44, 134);
            this.CheckBox16.Name = "CheckBox16";
            this.CheckBox16.Size = new System.Drawing.Size(38, 17);
            this.CheckBox16.TabIndex = 60;
            this.CheckBox16.Text = "16";
            this.CheckBox16.UseVisualStyleBackColor = true;
            // 
            // CheckBox17
            // 
            this.CheckBox17.AutoSize = true;
            this.CheckBox17.Location = new System.Drawing.Point(44, 157);
            this.CheckBox17.Name = "CheckBox17";
            this.CheckBox17.Size = new System.Drawing.Size(38, 17);
            this.CheckBox17.TabIndex = 59;
            this.CheckBox17.Text = "17";
            this.CheckBox17.UseVisualStyleBackColor = true;
            // 
            // CheckBox18
            // 
            this.CheckBox18.AutoSize = true;
            this.CheckBox18.Location = new System.Drawing.Point(44, 180);
            this.CheckBox18.Name = "CheckBox18";
            this.CheckBox18.Size = new System.Drawing.Size(38, 17);
            this.CheckBox18.TabIndex = 58;
            this.CheckBox18.Text = "18";
            this.CheckBox18.UseVisualStyleBackColor = true;
            // 
            // CheckBox19
            // 
            this.CheckBox19.AutoSize = true;
            this.CheckBox19.Location = new System.Drawing.Point(44, 203);
            this.CheckBox19.Name = "CheckBox19";
            this.CheckBox19.Size = new System.Drawing.Size(38, 17);
            this.CheckBox19.TabIndex = 57;
            this.CheckBox19.Text = "19";
            this.CheckBox19.UseVisualStyleBackColor = true;
            // 
            // CheckBox20
            // 
            this.CheckBox20.AutoSize = true;
            this.CheckBox20.Location = new System.Drawing.Point(44, 226);
            this.CheckBox20.Name = "CheckBox20";
            this.CheckBox20.Size = new System.Drawing.Size(38, 17);
            this.CheckBox20.TabIndex = 56;
            this.CheckBox20.Text = "20";
            this.CheckBox20.UseVisualStyleBackColor = true;
            // 
            // CheckBox10
            // 
            this.CheckBox10.AutoSize = true;
            this.CheckBox10.Location = new System.Drawing.Point(6, 226);
            this.CheckBox10.Name = "CheckBox10";
            this.CheckBox10.Size = new System.Drawing.Size(38, 17);
            this.CheckBox10.TabIndex = 55;
            this.CheckBox10.Text = "10";
            this.CheckBox10.UseVisualStyleBackColor = true;
            // 
            // CheckBox9
            // 
            this.CheckBox9.AutoSize = true;
            this.CheckBox9.Location = new System.Drawing.Point(6, 203);
            this.CheckBox9.Name = "CheckBox9";
            this.CheckBox9.Size = new System.Drawing.Size(32, 17);
            this.CheckBox9.TabIndex = 54;
            this.CheckBox9.Text = "9";
            this.CheckBox9.UseVisualStyleBackColor = true;
            // 
            // CheckBox8
            // 
            this.CheckBox8.AutoSize = true;
            this.CheckBox8.Location = new System.Drawing.Point(6, 180);
            this.CheckBox8.Name = "CheckBox8";
            this.CheckBox8.Size = new System.Drawing.Size(32, 17);
            this.CheckBox8.TabIndex = 53;
            this.CheckBox8.Text = "8";
            this.CheckBox8.UseVisualStyleBackColor = true;
            // 
            // CheckBox7
            // 
            this.CheckBox7.AutoSize = true;
            this.CheckBox7.Location = new System.Drawing.Point(6, 157);
            this.CheckBox7.Name = "CheckBox7";
            this.CheckBox7.Size = new System.Drawing.Size(32, 17);
            this.CheckBox7.TabIndex = 52;
            this.CheckBox7.Text = "7";
            this.CheckBox7.UseVisualStyleBackColor = true;
            // 
            // CheckBox6
            // 
            this.CheckBox6.AutoSize = true;
            this.CheckBox6.Location = new System.Drawing.Point(6, 134);
            this.CheckBox6.Name = "CheckBox6";
            this.CheckBox6.Size = new System.Drawing.Size(32, 17);
            this.CheckBox6.TabIndex = 51;
            this.CheckBox6.Text = "6";
            this.CheckBox6.UseVisualStyleBackColor = true;
            // 
            // CheckBox5
            // 
            this.CheckBox5.AutoSize = true;
            this.CheckBox5.Location = new System.Drawing.Point(6, 111);
            this.CheckBox5.Name = "CheckBox5";
            this.CheckBox5.Size = new System.Drawing.Size(32, 17);
            this.CheckBox5.TabIndex = 50;
            this.CheckBox5.Text = "5";
            this.CheckBox5.UseVisualStyleBackColor = true;
            // 
            // CheckBox4
            // 
            this.CheckBox4.AutoSize = true;
            this.CheckBox4.Location = new System.Drawing.Point(6, 88);
            this.CheckBox4.Name = "CheckBox4";
            this.CheckBox4.Size = new System.Drawing.Size(32, 17);
            this.CheckBox4.TabIndex = 49;
            this.CheckBox4.Text = "4";
            this.CheckBox4.UseVisualStyleBackColor = true;
            // 
            // CheckBox3
            // 
            this.CheckBox3.AutoSize = true;
            this.CheckBox3.Location = new System.Drawing.Point(6, 65);
            this.CheckBox3.Name = "CheckBox3";
            this.CheckBox3.Size = new System.Drawing.Size(32, 17);
            this.CheckBox3.TabIndex = 48;
            this.CheckBox3.Text = "3";
            this.CheckBox3.UseVisualStyleBackColor = true;
            // 
            // CheckBox2
            // 
            this.CheckBox2.AutoSize = true;
            this.CheckBox2.Location = new System.Drawing.Point(6, 42);
            this.CheckBox2.Name = "CheckBox2";
            this.CheckBox2.Size = new System.Drawing.Size(32, 17);
            this.CheckBox2.TabIndex = 47;
            this.CheckBox2.Text = "2";
            this.CheckBox2.UseVisualStyleBackColor = true;
            // 
            // CheckBox1
            // 
            this.CheckBox1.AutoSize = true;
            this.CheckBox1.Location = new System.Drawing.Point(6, 20);
            this.CheckBox1.Name = "CheckBox1";
            this.CheckBox1.Size = new System.Drawing.Size(32, 17);
            this.CheckBox1.TabIndex = 46;
            this.CheckBox1.Text = "1";
            this.CheckBox1.UseVisualStyleBackColor = true;
            // 
            // kitCrud1
            // 
            this.kitCrud1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.kitCrud1.Location = new System.Drawing.Point(196, 460);
            this.kitCrud1.Name = "kitCrud1";
            this.kitCrud1.Size = new System.Drawing.Size(152, 52);
            this.kitCrud1.TabIndex = 48;
            this.kitCrud1.Save += new System.EventHandler(this.kitCrud1_Save);
            this.kitCrud1.Edit += new System.EventHandler(this.kitCrud1_Edit);
            // 
            // FoodVaucherShow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(351, 521);
            this.Controls.Add(this.kitCrud1);
            this.Controls.Add(this.GroupBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numericUpDown3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numericUpDown2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.searchCatalog1);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "FoodVaucherShow";
            this.Text = "Vale Despensa";
            this.Load += new System.EventHandler(this.FoodVaucherShow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Components.Search.SearchCatalog searchCatalog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDown3;
        internal System.Windows.Forms.GroupBox GroupBox1;
        internal System.Windows.Forms.CheckBox CheckBox31;
        internal System.Windows.Forms.CheckBox CheckBox21;
        internal System.Windows.Forms.CheckBox CheckBox22;
        internal System.Windows.Forms.CheckBox CheckBox23;
        internal System.Windows.Forms.CheckBox CheckBox24;
        internal System.Windows.Forms.CheckBox CheckBox25;
        internal System.Windows.Forms.CheckBox CheckBox26;
        internal System.Windows.Forms.CheckBox CheckBox27;
        internal System.Windows.Forms.CheckBox CheckBox28;
        internal System.Windows.Forms.CheckBox CheckBox29;
        internal System.Windows.Forms.CheckBox CheckBox30;
        internal System.Windows.Forms.CheckBox CheckBox11;
        internal System.Windows.Forms.CheckBox CheckBox12;
        internal System.Windows.Forms.CheckBox CheckBox13;
        internal System.Windows.Forms.CheckBox CheckBox14;
        internal System.Windows.Forms.CheckBox CheckBox15;
        internal System.Windows.Forms.CheckBox CheckBox16;
        internal System.Windows.Forms.CheckBox CheckBox17;
        internal System.Windows.Forms.CheckBox CheckBox18;
        internal System.Windows.Forms.CheckBox CheckBox19;
        internal System.Windows.Forms.CheckBox CheckBox20;
        internal System.Windows.Forms.CheckBox CheckBox10;
        internal System.Windows.Forms.CheckBox CheckBox9;
        internal System.Windows.Forms.CheckBox CheckBox8;
        internal System.Windows.Forms.CheckBox CheckBox7;
        internal System.Windows.Forms.CheckBox CheckBox6;
        internal System.Windows.Forms.CheckBox CheckBox5;
        internal System.Windows.Forms.CheckBox CheckBox4;
        internal System.Windows.Forms.CheckBox CheckBox3;
        internal System.Windows.Forms.CheckBox CheckBox2;
        internal System.Windows.Forms.CheckBox CheckBox1;
        private Components.Buttons.KitCrud kitCrud1;
    }
}