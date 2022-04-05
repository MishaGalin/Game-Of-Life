namespace Game_Of_Life
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnApply = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.textBoxS = new System.Windows.Forms.TextBox();
            this.textBoxB = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxTimer = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.labelGenCount = new System.Windows.Forms.Label();
            this.labelGen = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.numUpDownDensity = new System.Windows.Forms.NumericUpDown();
            this.labelDensity = new System.Windows.Forms.Label();
            this.numUpDownRes = new System.Windows.Forms.NumericUpDown();
            this.labelRes = new System.Windows.Forms.Label();
            this.pctrBox = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownDensity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownRes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctrBox)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnApply);
            this.splitContainer1.Panel1.Controls.Add(this.button1);
            this.splitContainer1.Panel1.Controls.Add(this.textBoxS);
            this.splitContainer1.Panel1.Controls.Add(this.textBoxB);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.comboBoxTimer);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.labelGenCount);
            this.splitContainer1.Panel1.Controls.Add(this.labelGen);
            this.splitContainer1.Panel1.Controls.Add(this.btnClear);
            this.splitContainer1.Panel1.Controls.Add(this.btnNext);
            this.splitContainer1.Panel1.Controls.Add(this.btnStop);
            this.splitContainer1.Panel1.Controls.Add(this.btnStart);
            this.splitContainer1.Panel1.Controls.Add(this.numUpDownDensity);
            this.splitContainer1.Panel1.Controls.Add(this.labelDensity);
            this.splitContainer1.Panel1.Controls.Add(this.numUpDownRes);
            this.splitContainer1.Panel1.Controls.Add(this.labelRes);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pctrBox);
            this.splitContainer1.Size = new System.Drawing.Size(1061, 750);
            this.splitContainer1.SplitterDistance = 100;
            this.splitContainer1.TabIndex = 0;
            // 
            // btnApply
            // 
            this.btnApply.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnApply.AutoSize = true;
            this.btnApply.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnApply.Location = new System.Drawing.Point(890, 17);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(124, 30);
            this.btnApply.TabIndex = 18;
            this.btnApply.Text = "Apply settings";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.BtnApply_Click);
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.button1.AutoSize = true;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(398, 17);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(161, 30);
            this.button1.TabIndex = 17;
            this.button1.Text = "Generate randomly";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.BtnRandom_Click);
            // 
            // textBoxS
            // 
            this.textBoxS.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxS.Location = new System.Drawing.Point(744, 55);
            this.textBoxS.Name = "textBoxS";
            this.textBoxS.Size = new System.Drawing.Size(130, 27);
            this.textBoxS.TabIndex = 16;
            this.textBoxS.Text = "2 3";
            // 
            // textBoxB
            // 
            this.textBoxB.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxB.Location = new System.Drawing.Point(744, 19);
            this.textBoxB.Name = "textBoxB";
            this.textBoxB.Size = new System.Drawing.Size(130, 27);
            this.textBoxB.TabIndex = 15;
            this.textBoxB.Text = "3";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(728, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 20);
            this.label3.TabIndex = 14;
            this.label3.Text = "S";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(727, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 20);
            this.label2.TabIndex = 13;
            this.label2.Text = "B";
            // 
            // comboBoxTimer
            // 
            this.comboBoxTimer.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.comboBoxTimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxTimer.Items.AddRange(new object[] {
            "25",
            "50",
            "100",
            "250",
            "500",
            "1000"});
            this.comboBoxTimer.Location = new System.Drawing.Point(113, 65);
            this.comboBoxTimer.Name = "comboBoxTimer";
            this.comboBoxTimer.Size = new System.Drawing.Size(80, 28);
            this.comboBoxTimer.TabIndex = 12;
            this.comboBoxTimer.Tag = "";
            this.comboBoxTimer.TextChanged += new System.EventHandler(this.ComboBoxTimer_TextChanged);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(10, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 20);
            this.label1.TabIndex = 11;
            this.label1.Text = "Timer (ms):";
            // 
            // labelGenCount
            // 
            this.labelGenCount.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelGenCount.AutoSize = true;
            this.labelGenCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelGenCount.Location = new System.Drawing.Point(670, 38);
            this.labelGenCount.Name = "labelGenCount";
            this.labelGenCount.Size = new System.Drawing.Size(18, 20);
            this.labelGenCount.TabIndex = 10;
            this.labelGenCount.Text = "0";
            // 
            // labelGen
            // 
            this.labelGen.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelGen.AutoSize = true;
            this.labelGen.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelGen.Location = new System.Drawing.Point(568, 38);
            this.labelGen.Name = "labelGen";
            this.labelGen.Size = new System.Drawing.Size(96, 20);
            this.labelGen.TabIndex = 9;
            this.labelGen.Text = "Generation:";
            // 
            // btnClear
            // 
            this.btnClear.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnClear.AutoSize = true;
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnClear.Location = new System.Drawing.Point(317, 53);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 30);
            this.btnClear.TabIndex = 8;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnNext
            // 
            this.btnNext.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnNext.AutoSize = true;
            this.btnNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnNext.Location = new System.Drawing.Point(317, 17);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 30);
            this.btnNext.TabIndex = 7;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.BtnNext_Click);
            // 
            // btnStop
            // 
            this.btnStop.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnStop.AutoSize = true;
            this.btnStop.Enabled = false;
            this.btnStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnStop.Location = new System.Drawing.Point(236, 53);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 30);
            this.btnStop.TabIndex = 6;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.BtnStop_Click);
            // 
            // btnStart
            // 
            this.btnStart.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnStart.AutoSize = true;
            this.btnStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnStart.Location = new System.Drawing.Point(236, 17);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 30);
            this.btnStart.TabIndex = 5;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // numUpDownDensity
            // 
            this.numUpDownDensity.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.numUpDownDensity.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.numUpDownDensity.Location = new System.Drawing.Point(132, 36);
            this.numUpDownDensity.Maximum = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.numUpDownDensity.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numUpDownDensity.Name = "numUpDownDensity";
            this.numUpDownDensity.Size = new System.Drawing.Size(57, 27);
            this.numUpDownDensity.TabIndex = 4;
            this.numUpDownDensity.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // labelDensity
            // 
            this.labelDensity.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelDensity.AutoSize = true;
            this.labelDensity.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelDensity.Location = new System.Drawing.Point(10, 38);
            this.labelDensity.Name = "labelDensity";
            this.labelDensity.Size = new System.Drawing.Size(116, 20);
            this.labelDensity.TabIndex = 3;
            this.labelDensity.Text = "Density (2-40)";
            // 
            // numUpDownRes
            // 
            this.numUpDownRes.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.numUpDownRes.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.numUpDownRes.Location = new System.Drawing.Point(154, 5);
            this.numUpDownRes.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numUpDownRes.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numUpDownRes.Name = "numUpDownRes";
            this.numUpDownRes.Size = new System.Drawing.Size(57, 27);
            this.numUpDownRes.TabIndex = 2;
            this.numUpDownRes.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            // 
            // labelRes
            // 
            this.labelRes.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelRes.AutoSize = true;
            this.labelRes.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelRes.Location = new System.Drawing.Point(10, 7);
            this.labelRes.Name = "labelRes";
            this.labelRes.Size = new System.Drawing.Size(138, 20);
            this.labelRes.TabIndex = 1;
            this.labelRes.Text = "Resolution (2-20)";
            // 
            // pctrBox
            // 
            this.pctrBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pctrBox.Location = new System.Drawing.Point(0, 0);
            this.pctrBox.Name = "pctrBox";
            this.pctrBox.Size = new System.Drawing.Size(1057, 642);
            this.pctrBox.TabIndex = 0;
            this.pctrBox.TabStop = false;
            this.pctrBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pctrBox_MouseClick);
            this.pctrBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pctrBox_MouseClick);
            // 
            // timer1
            // 
            this.timer1.Interval = 50;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1061, 750);
            this.Controls.Add(this.splitContainer1);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1050, 600);
            this.Name = "Form1";
            this.Text = "Game of Life";
            this.ResizeEnd += new System.EventHandler(this.Form1_Resize);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownDensity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownRes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctrBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.NumericUpDown numUpDownRes;
        private System.Windows.Forms.Label labelRes;
        private System.Windows.Forms.PictureBox pctrBox;
        private System.Windows.Forms.NumericUpDown numUpDownDensity;
        private System.Windows.Forms.Label labelDensity;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label labelGenCount;
        private System.Windows.Forms.Label labelGen;
        private System.Windows.Forms.ComboBox comboBoxTimer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxS;
        private System.Windows.Forms.TextBox textBoxB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnApply;
    }
}

