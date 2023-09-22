namespace BrushMotorApp
{
    partial class revolutions
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.revolutionVSTme = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.rps = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.amp = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.revolutionInit = new System.Windows.Forms.TextBox();
            this.rpsInit = new System.Windows.Forms.TextBox();
            this.ampInit = new System.Windows.Forms.TextBox();
            this.time_second = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.appLoadPanel = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.outputToExcel = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.mMRinWithRK4 = new System.Windows.Forms.Button();
            this.rowsTextBox = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(21, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(90, 30);
            this.button1.TabIndex = 0;
            this.button1.Text = "load constants";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(21, 537);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(112, 28);
            this.button2.TabIndex = 1;
            this.button2.Text = "quit";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(21, 70);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(136, 41);
            this.button3.TabIndex = 2;
            this.button3.Text = "makeMotorRun";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // revolutionVSTme
            // 
            this.revolutionVSTme.Location = new System.Drawing.Point(470, 12);
            this.revolutionVSTme.Name = "revolutionVSTme";
            this.revolutionVSTme.Size = new System.Drawing.Size(398, 151);
            this.revolutionVSTme.TabIndex = 3;
            this.revolutionVSTme.Paint += new System.Windows.Forms.PaintEventHandler(this.revolutionVSTme_Paint);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(390, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "{revolutions}";
            // 
            // rps
            // 
            this.rps.Location = new System.Drawing.Point(470, 183);
            this.rps.Name = "rps";
            this.rps.Size = new System.Drawing.Size(398, 151);
            this.rps.TabIndex = 4;
            this.rps.Paint += new System.Windows.Forms.PaintEventHandler(this.rps_Paint);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(390, 240);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "{rps}";
            // 
            // amp
            // 
            this.amp.Location = new System.Drawing.Point(470, 365);
            this.amp.Name = "amp";
            this.amp.Size = new System.Drawing.Size(398, 151);
            this.amp.TabIndex = 5;
            this.amp.Paint += new System.Windows.Forms.PaintEventHandler(this.amp_Paint);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(390, 429);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "{amp}";
            // 
            // revolutionInit
            // 
            this.revolutionInit.Location = new System.Drawing.Point(370, 96);
            this.revolutionInit.MaxLength = 8;
            this.revolutionInit.Name = "revolutionInit";
            this.revolutionInit.Size = new System.Drawing.Size(85, 20);
            this.revolutionInit.TabIndex = 7;
            this.revolutionInit.Text = "0";
            this.revolutionInit.TextChanged += new System.EventHandler(this.revolutionInit_TextChanged);
            // 
            // rpsInit
            // 
            this.rpsInit.Location = new System.Drawing.Point(370, 268);
            this.rpsInit.MaxLength = 8;
            this.rpsInit.Name = "rpsInit";
            this.rpsInit.Size = new System.Drawing.Size(81, 20);
            this.rpsInit.TabIndex = 8;
            this.rpsInit.Text = "0";
            this.rpsInit.TextChanged += new System.EventHandler(this.rpsInit_TextChanged);
            // 
            // ampInit
            // 
            this.ampInit.Location = new System.Drawing.Point(370, 459);
            this.ampInit.MaxLength = 8;
            this.ampInit.Name = "ampInit";
            this.ampInit.Size = new System.Drawing.Size(81, 20);
            this.ampInit.TabIndex = 9;
            this.ampInit.Text = "11.2";
            this.ampInit.TextChanged += new System.EventHandler(this.ampInit_TextChanged);
            // 
            // time_second
            // 
            this.time_second.AutoSize = true;
            this.time_second.Location = new System.Drawing.Point(125, 346);
            this.time_second.Name = "time_second";
            this.time_second.Size = new System.Drawing.Size(69, 13);
            this.time_second.TabIndex = 11;
            this.time_second.Text = "time{second}";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 240);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "magnitude{mNm}";
            // 
            // appLoadPanel
            // 
            this.appLoadPanel.Location = new System.Drawing.Point(128, 152);
            this.appLoadPanel.Name = "appLoadPanel";
            this.appLoadPanel.Size = new System.Drawing.Size(241, 164);
            this.appLoadPanel.TabIndex = 13;
            this.appLoadPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.appLoadPanel_Paint);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(1, 303);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(121, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "neg. stall current {mNM}";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 137);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "stall cuurent {mNM}";
            // 
            // outputToExcel
            // 
            this.outputToExcel.AutoSize = true;
            this.outputToExcel.Location = new System.Drawing.Point(167, 120);
            this.outputToExcel.Name = "outputToExcel";
            this.outputToExcel.Size = new System.Drawing.Size(97, 17);
            this.outputToExcel.TabIndex = 17;
            this.outputToExcel.Text = "output to Excel";
            this.outputToExcel.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(393, 508);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "zero amps";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(382, 358);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "stall current";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(394, 315);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(44, 13);
            this.label9.TabIndex = 20;
            this.label9.Text = "zero rps";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(387, 182);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 13);
            this.label10.TabIndex = 21;
            this.label10.Text = "no load rps";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(398, 146);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(27, 13);
            this.label11.TabIndex = 22;
            this.label11.Text = "zero";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(388, 16);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(55, 13);
            this.label12.TabIndex = 23;
            this.label12.Text = "max value";
            // 
            // mMRinWithRK4
            // 
            this.mMRinWithRK4.Location = new System.Drawing.Point(167, 70);
            this.mMRinWithRK4.Name = "mMRinWithRK4";
            this.mMRinWithRK4.Size = new System.Drawing.Size(151, 41);
            this.mMRinWithRK4.TabIndex = 24;
            this.mMRinWithRK4.Text = "makeMotorRunWithRK4";
            this.mMRinWithRK4.UseVisualStyleBackColor = true;
            this.mMRinWithRK4.Click += new System.EventHandler(this.mMRinWithRK4_Click);
            // 
            // rowsTextBox
            // 
            this.rowsTextBox.Location = new System.Drawing.Point(202, 459);
            this.rowsTextBox.MaxLength = 8;
            this.rowsTextBox.Name = "rowsTextBox";
            this.rowsTextBox.Size = new System.Drawing.Size(83, 20);
            this.rowsTextBox.TabIndex = 25;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(88, 460);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(91, 13);
            this.label13.TabIndex = 26;
            this.label13.Text = "rows written to XL";
            // 
            // revolutions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(972, 577);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.rowsTextBox);
            this.Controls.Add(this.mMRinWithRK4);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.outputToExcel);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.appLoadPanel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.time_second);
            this.Controls.Add(this.ampInit);
            this.Controls.Add(this.rpsInit);
            this.Controls.Add(this.revolutionInit);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.amp);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.rps);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.revolutionVSTme);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "revolutions";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Panel revolutionVSTme;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel rps;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel amp;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox revolutionInit;
        private System.Windows.Forms.TextBox rpsInit;
        private System.Windows.Forms.TextBox ampInit;
        private System.Windows.Forms.Label time_second;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel appLoadPanel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox outputToExcel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button mMRinWithRK4;
        private System.Windows.Forms.TextBox rowsTextBox;
        private System.Windows.Forms.Label label13;
    }
}

