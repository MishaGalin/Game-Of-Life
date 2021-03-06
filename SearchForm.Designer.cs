namespace Game_Of_Life
{
    partial class SearchForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numericUpDownRank = new System.Windows.Forms.NumericUpDown();
            this.numUpDownHeight = new System.Windows.Forms.NumericUpDown();
            this.labelRank = new System.Windows.Forms.Label();
            this.textBoxS = new System.Windows.Forms.TextBox();
            this.textBoxB = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labelRes = new System.Windows.Forms.Label();
            this.numUpDownWidth = new System.Windows.Forms.NumericUpDown();
            this.btnSearch = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.labelTime = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRank)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownWidth)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.numUpDownHeight);
            this.groupBox1.Controls.Add(this.textBoxS);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.numericUpDownRank);
            this.groupBox1.Controls.Add(this.labelRank);
            this.groupBox1.Controls.Add(this.textBoxB);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.labelRes);
            this.groupBox1.Controls.Add(this.numUpDownWidth);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(535, 87);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Field";
            // 
            // numericUpDownRank
            // 
            this.numericUpDownRank.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.numericUpDownRank.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownRank.Location = new System.Drawing.Point(415, 21);
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
            this.numericUpDownRank.TabIndex = 28;
            this.numericUpDownRank.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numUpDownHeight
            // 
            this.numUpDownHeight.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.numUpDownHeight.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.numUpDownHeight.Location = new System.Drawing.Point(120, 51);
            this.numUpDownHeight.Maximum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.numUpDownHeight.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numUpDownHeight.Name = "numUpDownHeight";
            this.numUpDownHeight.Size = new System.Drawing.Size(57, 27);
            this.numUpDownHeight.TabIndex = 21;
            this.numUpDownHeight.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // labelRank
            // 
            this.labelRank.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelRank.AutoSize = true;
            this.labelRank.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRank.Location = new System.Drawing.Point(368, 23);
            this.labelRank.Name = "labelRank";
            this.labelRank.Size = new System.Drawing.Size(41, 20);
            this.labelRank.TabIndex = 27;
            this.labelRank.Text = "Rank";
            // 
            // textBoxS
            // 
            this.textBoxS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxS.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBoxS.Location = new System.Drawing.Point(260, 50);
            this.textBoxS.Name = "textBoxS";
            this.textBoxS.Size = new System.Drawing.Size(87, 27);
            this.textBoxS.TabIndex = 20;
            this.textBoxS.Text = "2 3";
            // 
            // textBoxB
            // 
            this.textBoxB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxB.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBoxB.Location = new System.Drawing.Point(260, 20);
            this.textBoxB.Name = "textBoxB";
            this.textBoxB.Size = new System.Drawing.Size(87, 27);
            this.textBoxB.TabIndex = 19;
            this.textBoxB.Text = "3";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(234, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 20);
            this.label3.TabIndex = 18;
            this.label3.Text = "S";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(234, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 20);
            this.label2.TabIndex = 17;
            this.label2.Text = "B";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label1.Location = new System.Drawing.Point(6, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Height (3-6)";
            // 
            // labelRes
            // 
            this.labelRes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelRes.AutoSize = true;
            this.labelRes.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelRes.Location = new System.Drawing.Point(6, 23);
            this.labelRes.Name = "labelRes";
            this.labelRes.Size = new System.Drawing.Size(85, 20);
            this.labelRes.TabIndex = 5;
            this.labelRes.Text = "Width (3-6)";
            // 
            // numUpDownWidth
            // 
            this.numUpDownWidth.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.numUpDownWidth.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.numUpDownWidth.Location = new System.Drawing.Point(120, 18);
            this.numUpDownWidth.Maximum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.numUpDownWidth.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numUpDownWidth.Name = "numUpDownWidth";
            this.numUpDownWidth.Size = new System.Drawing.Size(57, 27);
            this.numUpDownWidth.TabIndex = 3;
            this.numUpDownWidth.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // btnSearch
            // 
            this.btnSearch.AutoSize = true;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnSearch.Location = new System.Drawing.Point(12, 104);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(144, 32);
            this.btnSearch.TabIndex = 19;
            this.btnSearch.Text = "Start searching";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(162, 105);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(289, 30);
            this.progressBar1.TabIndex = 20;
            // 
            // labelTime
            // 
            this.labelTime.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTime.AutoSize = true;
            this.labelTime.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelTime.Location = new System.Drawing.Point(457, 110);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(78, 20);
            this.labelTime.TabIndex = 22;
            this.labelTime.Text = "Time: 0.0 s";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // SearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(559, 144);
            this.Controls.Add(this.labelTime);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "SearchForm";
            this.Text = "Structure search";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRank)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownWidth)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown numUpDownWidth;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelRes;
        private System.Windows.Forms.TextBox textBoxS;
        private System.Windows.Forms.TextBox textBoxB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.NumericUpDown numUpDownHeight;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.NumericUpDown numericUpDownRank;
        private System.Windows.Forms.Label labelRank;
    }
}