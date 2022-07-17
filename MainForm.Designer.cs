namespace Game_Of_Life
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.labelY = new System.Windows.Forms.Label();
            this.labelX = new System.Windows.Forms.Label();
            this.labelZoom = new System.Windows.Forms.Label();
            this.numericUpDownRank = new System.Windows.Forms.NumericUpDown();
            this.labelRank = new System.Windows.Forms.Label();
            this.labelPopulation = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.labelSpeed = new System.Windows.Forms.Label();
            this.btnApply = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.textBoxS = new System.Windows.Forms.TextBox();
            this.textBoxB = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxTimer = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
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
            this.MainTimer = new System.Windows.Forms.Timer(this.components);
            this.SpeedMeasurementTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRank)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownDensity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownRes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctrBox)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.labelY);
            this.splitContainer1.Panel1.Controls.Add(this.labelX);
            this.splitContainer1.Panel1.Controls.Add(this.labelZoom);
            this.splitContainer1.Panel1.Controls.Add(this.numericUpDownRank);
            this.splitContainer1.Panel1.Controls.Add(this.labelRank);
            this.splitContainer1.Panel1.Controls.Add(this.labelPopulation);
            this.splitContainer1.Panel1.Controls.Add(this.btnSearch);
            this.splitContainer1.Panel1.Controls.Add(this.labelSpeed);
            this.splitContainer1.Panel1.Controls.Add(this.btnApply);
            this.splitContainer1.Panel1.Controls.Add(this.button1);
            this.splitContainer1.Panel1.Controls.Add(this.textBoxS);
            this.splitContainer1.Panel1.Controls.Add(this.textBoxB);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.comboBoxTimer);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.labelGen);
            this.splitContainer1.Panel1.Controls.Add(this.btnClear);
            this.splitContainer1.Panel1.Controls.Add(this.btnNext);
            this.splitContainer1.Panel1.Controls.Add(this.btnStop);
            this.splitContainer1.Panel1.Controls.Add(this.btnStart);
            this.splitContainer1.Panel1.Controls.Add(this.numUpDownDensity);
            this.splitContainer1.Panel1.Controls.Add(this.labelDensity);
            this.splitContainer1.Panel1.Controls.Add(this.numUpDownRes);
            this.splitContainer1.Panel1.Controls.Add(this.labelRes);
            this.splitContainer1.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pctrBox);
            this.splitContainer1.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer1.Size = new System.Drawing.Size(1382, 953);
            this.splitContainer1.SplitterDistance = 110;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 0;
            // 
            // labelY
            // 
            this.labelY.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelY.AutoSize = true;
            this.labelY.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelY.Location = new System.Drawing.Point(877, 72);
            this.labelY.Name = "labelY";
            this.labelY.Size = new System.Drawing.Size(32, 20);
            this.labelY.TabIndex = 29;
            this.labelY.Text = "Y: 0";
            // 
            // labelX
            // 
            this.labelX.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelX.AutoSize = true;
            this.labelX.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX.Location = new System.Drawing.Point(877, 43);
            this.labelX.Name = "labelX";
            this.labelX.Size = new System.Drawing.Size(33, 20);
            this.labelX.TabIndex = 28;
            this.labelX.Text = "X: 0";
            // 
            // labelZoom
            // 
            this.labelZoom.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelZoom.AutoSize = true;
            this.labelZoom.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelZoom.Location = new System.Drawing.Point(877, 15);
            this.labelZoom.Name = "labelZoom";
            this.labelZoom.Size = new System.Drawing.Size(71, 20);
            this.labelZoom.TabIndex = 27;
            this.labelZoom.Text = "Zoom: 1x";
            // 
            // numericUpDownRank
            // 
            this.numericUpDownRank.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.numericUpDownRank.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownRank.Location = new System.Drawing.Point(762, 73);
            this.numericUpDownRank.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownRank.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownRank.Name = "numericUpDownRank";
            this.numericUpDownRank.Size = new System.Drawing.Size(57, 27);
            this.numericUpDownRank.TabIndex = 26;
            this.numericUpDownRank.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownRank.ValueChanged += new System.EventHandler(this.numericUpDownRank_ValueChanged);
            // 
            // labelRank
            // 
            this.labelRank.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelRank.AutoSize = true;
            this.labelRank.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRank.Location = new System.Drawing.Point(715, 75);
            this.labelRank.Name = "labelRank";
            this.labelRank.Size = new System.Drawing.Size(41, 20);
            this.labelRank.TabIndex = 25;
            this.labelRank.Text = "Rank";
            // 
            // labelPopulation
            // 
            this.labelPopulation.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelPopulation.AutoSize = true;
            this.labelPopulation.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPopulation.Location = new System.Drawing.Point(555, 45);
            this.labelPopulation.Name = "labelPopulation";
            this.labelPopulation.Size = new System.Drawing.Size(95, 20);
            this.labelPopulation.TabIndex = 23;
            this.labelPopulation.Text = "Population: 0";
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnSearch.AutoSize = true;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Location = new System.Drawing.Point(223, 72);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(304, 30);
            this.btnSearch.TabIndex = 21;
            this.btnSearch.Text = "Structure search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // labelSpeed
            // 
            this.labelSpeed.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelSpeed.AutoSize = true;
            this.labelSpeed.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSpeed.Location = new System.Drawing.Point(555, 75);
            this.labelSpeed.Name = "labelSpeed";
            this.labelSpeed.Size = new System.Drawing.Size(117, 20);
            this.labelSpeed.TabIndex = 19;
            this.labelSpeed.Text = "Speed (gen/s): 0";
            // 
            // btnApply
            // 
            this.btnApply.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnApply.AutoSize = true;
            this.btnApply.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApply.Location = new System.Drawing.Point(377, 40);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(150, 30);
            this.btnApply.TabIndex = 18;
            this.btnApply.Text = "Apply settings";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.BtnApply_Click);
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.button1.AutoSize = true;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(377, 9);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(150, 30);
            this.button1.TabIndex = 17;
            this.button1.Text = "Generate randomly";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.BtnRandom_Click);
            // 
            // textBoxS
            // 
            this.textBoxS.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.textBoxS.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxS.Location = new System.Drawing.Point(730, 42);
            this.textBoxS.Name = "textBoxS";
            this.textBoxS.Size = new System.Drawing.Size(130, 27);
            this.textBoxS.TabIndex = 16;
            this.textBoxS.Text = "2 3";
            // 
            // textBoxB
            // 
            this.textBoxB.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.textBoxB.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxB.Location = new System.Drawing.Point(730, 12);
            this.textBoxB.Name = "textBoxB";
            this.textBoxB.Size = new System.Drawing.Size(130, 27);
            this.textBoxB.TabIndex = 15;
            this.textBoxB.Text = "3";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(715, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 20);
            this.label3.TabIndex = 14;
            this.label3.Text = "S";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(715, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(18, 20);
            this.label2.TabIndex = 13;
            this.label2.Text = "B";
            // 
            // comboBoxTimer
            // 
            this.comboBoxTimer.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.comboBoxTimer.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxTimer.Items.AddRange(new object[] {
            "16",
            "25",
            "33",
            "50",
            "100",
            "250",
            "500",
            "1000"});
            this.comboBoxTimer.Location = new System.Drawing.Point(116, 73);
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
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 20);
            this.label1.TabIndex = 11;
            this.label1.Text = "Timer (ms):";
            // 
            // labelGen
            // 
            this.labelGen.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelGen.AutoSize = true;
            this.labelGen.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelGen.Location = new System.Drawing.Point(555, 15);
            this.labelGen.Name = "labelGen";
            this.labelGen.Size = new System.Drawing.Size(97, 20);
            this.labelGen.TabIndex = 9;
            this.labelGen.Text = "Generation: 0";
            // 
            // btnClear
            // 
            this.btnClear.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnClear.AutoSize = true;
            this.btnClear.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Location = new System.Drawing.Point(300, 40);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 30);
            this.btnClear.TabIndex = 8;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // btnNext
            // 
            this.btnNext.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnNext.AutoSize = true;
            this.btnNext.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.Location = new System.Drawing.Point(300, 9);
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
            this.btnStop.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStop.Location = new System.Drawing.Point(223, 40);
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
            this.btnStart.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.Location = new System.Drawing.Point(223, 9);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 30);
            this.btnStart.TabIndex = 5;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.BtnStart_Click);
            // 
            // numUpDownDensity
            // 
            this.numUpDownDensity.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.numUpDownDensity.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numUpDownDensity.Location = new System.Drawing.Point(139, 41);
            this.numUpDownDensity.Maximum = new decimal(new int[] {
            1000,
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
            this.numUpDownDensity.ValueChanged += new System.EventHandler(this.numUpDownDensity_ValueChanged);
            // 
            // labelDensity
            // 
            this.labelDensity.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelDensity.AutoSize = true;
            this.labelDensity.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDensity.Location = new System.Drawing.Point(10, 43);
            this.labelDensity.Name = "labelDensity";
            this.labelDensity.Size = new System.Drawing.Size(118, 20);
            this.labelDensity.TabIndex = 3;
            this.labelDensity.Text = "Density (2-1000)";
            // 
            // numUpDownRes
            // 
            this.numUpDownRes.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.numUpDownRes.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numUpDownRes.Location = new System.Drawing.Point(139, 10);
            this.numUpDownRes.Maximum = new decimal(new int[] {
            40,
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
            this.numUpDownRes.ValueChanged += new System.EventHandler(this.numUpDownRes_ValueChanged);
            // 
            // labelRes
            // 
            this.labelRes.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelRes.AutoSize = true;
            this.labelRes.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRes.Location = new System.Drawing.Point(10, 12);
            this.labelRes.Name = "labelRes";
            this.labelRes.Size = new System.Drawing.Size(123, 20);
            this.labelRes.TabIndex = 1;
            this.labelRes.Text = "Resolution (2-40)";
            // 
            // pctrBox
            // 
            this.pctrBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pctrBox.Location = new System.Drawing.Point(0, 0);
            this.pctrBox.Name = "pctrBox";
            this.pctrBox.Size = new System.Drawing.Size(1380, 840);
            this.pctrBox.TabIndex = 0;
            this.pctrBox.TabStop = false;
            this.pctrBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PctrBox_MouseClick);
            this.pctrBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pctrBox_MouseDown);
            this.pctrBox.MouseLeave += new System.EventHandler(this.pctrBox_MouseLeave);
            this.pctrBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PctrBox_MouseClick);
            this.pctrBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pctrBox_MouseUp);
            // 
            // MainTimer
            // 
            this.MainTimer.Interval = 50;
            this.MainTimer.Tick += new System.EventHandler(this.MainTimer_Tick);
            // 
            // SpeedMeasurementTimer
            // 
            this.SpeedMeasurementTimer.Enabled = true;
            this.SpeedMeasurementTimer.Interval = 1000;
            this.SpeedMeasurementTimer.Tick += new System.EventHandler(this.SpeedMeasurementTimer_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1382, 953);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1000, 800);
            this.Name = "MainForm";
            this.Text = "Game of Life";
            this.ResizeBegin += new System.EventHandler(this.MainForm_ResizeBegin);
            this.ResizeEnd += new System.EventHandler(this.MainForm_Resize);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRank)).EndInit();
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
        private System.Windows.Forms.Timer MainTimer;
        private System.Windows.Forms.Label labelGen;
        private System.Windows.Forms.ComboBox comboBoxTimer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxS;
        private System.Windows.Forms.TextBox textBoxB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Label labelSpeed;
        private System.Windows.Forms.Timer SpeedMeasurementTimer;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label labelPopulation;
        private System.Windows.Forms.NumericUpDown numericUpDownRank;
        private System.Windows.Forms.Label labelRank;
        private System.Windows.Forms.Label labelZoom;
        private System.Windows.Forms.Label labelY;
        private System.Windows.Forms.Label labelX;
    }
}

