namespace SpeechRecognitionNN
{
    partial class MainForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title3 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.chartError = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.chartAudio = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.pbrEpochs = new System.Windows.Forms.ProgressBar();
            this.rtbConsole = new System.Windows.Forms.RichTextBox();
            this.cbxFFT = new System.Windows.Forms.CheckBox();
            this.chartFreq = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.cbxTrain = new System.Windows.Forms.CheckBox();
            this.cbxRun = new System.Windows.Forms.CheckBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.sbxHiddenCount = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel5 = new System.Windows.Forms.ToolStripLabel();
            this.sbxHiddenCount2 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.sbxEpochs = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.sbxLearningRate = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.sbxMomentum = new System.Windows.Forms.ToolStripTextBox();
            this.sbnSave = new System.Windows.Forms.ToolStripButton();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.btnTest = new System.Windows.Forms.Button();
            this.cbxAbort = new System.Windows.Forms.CheckBox();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.sbxData = new System.Windows.Forms.ToolStripTextBox();
            this.sbnData = new System.Windows.Forms.ToolStripButton();
            this.sbxWeights = new System.Windows.Forms.ToolStripTextBox();
            this.sbnWeights = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.chartError)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartAudio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartFreq)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // chartError
            // 
            chartArea1.Name = "ChartArea1";
            this.chartError.ChartAreas.Add(chartArea1);
            legend1.Enabled = false;
            legend1.Name = "Legend1";
            this.chartError.Legends.Add(legend1);
            this.chartError.Location = new System.Drawing.Point(11, 66);
            this.chartError.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chartError.Name = "chartError";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.Name = "MSE";
            this.chartError.Series.Add(series1);
            this.chartError.Size = new System.Drawing.Size(575, 300);
            this.chartError.TabIndex = 1;
            this.chartError.Text = "chart1";
            title1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title1.Name = "chartTitle";
            title1.Text = "Network MSE";
            this.chartError.Titles.Add(title1);
            // 
            // timer1
            // 
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // chartAudio
            // 
            chartArea2.Name = "ChartArea1";
            this.chartAudio.ChartAreas.Add(chartArea2);
            legend2.Enabled = false;
            legend2.Name = "Legend1";
            this.chartAudio.Legends.Add(legend2);
            this.chartAudio.Location = new System.Drawing.Point(598, 66);
            this.chartAudio.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chartAudio.Name = "chartAudio";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series2.Legend = "Legend1";
            series2.Name = "Input";
            this.chartAudio.Series.Add(series2);
            this.chartAudio.Size = new System.Drawing.Size(575, 300);
            this.chartAudio.TabIndex = 2;
            this.chartAudio.Text = "chart1";
            title2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title2.Name = "chartTitle";
            title2.Text = "Audio Input";
            this.chartAudio.Titles.Add(title2);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "wav";
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            this.openFileDialog1.InitialDirectory = "C:\\Users\\Dylan\\Desktop";
            // 
            // pbrEpochs
            // 
            this.pbrEpochs.Location = new System.Drawing.Point(12, 651);
            this.pbrEpochs.Name = "pbrEpochs";
            this.pbrEpochs.Size = new System.Drawing.Size(1160, 23);
            this.pbrEpochs.Step = 1;
            this.pbrEpochs.TabIndex = 7;
            // 
            // rtbConsole
            // 
            this.rtbConsole.Location = new System.Drawing.Point(13, 372);
            this.rtbConsole.Name = "rtbConsole";
            this.rtbConsole.ReadOnly = true;
            this.rtbConsole.Size = new System.Drawing.Size(1160, 273);
            this.rtbConsole.TabIndex = 8;
            this.rtbConsole.Text = "";
            // 
            // cbxFFT
            // 
            this.cbxFFT.AutoSize = true;
            this.cbxFFT.Location = new System.Drawing.Point(1066, 5);
            this.cbxFFT.Name = "cbxFFT";
            this.cbxFFT.Size = new System.Drawing.Size(106, 17);
            this.cbxFFT.TabIndex = 9;
            this.cbxFFT.Text = "Show Frequency";
            this.cbxFFT.UseVisualStyleBackColor = true;
            this.cbxFFT.CheckedChanged += new System.EventHandler(this.cbxFFT_CheckedChanged);
            // 
            // chartFreq
            // 
            chartArea3.Name = "ChartArea1";
            this.chartFreq.ChartAreas.Add(chartArea3);
            legend3.Enabled = false;
            legend3.Name = "Legend1";
            this.chartFreq.Legends.Add(legend3);
            this.chartFreq.Location = new System.Drawing.Point(597, 66);
            this.chartFreq.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chartFreq.Name = "chartFreq";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series3.Legend = "Legend1";
            series3.Name = "Input";
            this.chartFreq.Series.Add(series3);
            this.chartFreq.Size = new System.Drawing.Size(575, 300);
            this.chartFreq.TabIndex = 10;
            this.chartFreq.Text = "chart1";
            title3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title3.Name = "chartTitle";
            title3.Text = "Audio Frequency";
            this.chartFreq.Titles.Add(title3);
            // 
            // cbxTrain
            // 
            this.cbxTrain.Appearance = System.Windows.Forms.Appearance.Button;
            this.cbxTrain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cbxTrain.Cursor = System.Windows.Forms.Cursors.Default;
            this.cbxTrain.FlatAppearance.BorderSize = 0;
            this.cbxTrain.FlatAppearance.CheckedBackColor = System.Drawing.Color.Red;
            this.cbxTrain.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cbxTrain.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.cbxTrain.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbxTrain.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxTrain.Location = new System.Drawing.Point(13, 36);
            this.cbxTrain.Name = "cbxTrain";
            this.cbxTrain.Size = new System.Drawing.Size(75, 25);
            this.cbxTrain.TabIndex = 0;
            this.cbxTrain.Text = "Train";
            this.cbxTrain.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cbxTrain.UseVisualStyleBackColor = false;
            this.cbxTrain.CheckedChanged += new System.EventHandler(this.cbxTrain_CheckedChanged);
            // 
            // cbxRun
            // 
            this.cbxRun.Appearance = System.Windows.Forms.Appearance.Button;
            this.cbxRun.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cbxRun.FlatAppearance.BorderSize = 0;
            this.cbxRun.FlatAppearance.CheckedBackColor = System.Drawing.Color.Red;
            this.cbxRun.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cbxRun.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.cbxRun.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbxRun.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxRun.Location = new System.Drawing.Point(1097, 36);
            this.cbxRun.Name = "cbxRun";
            this.cbxRun.Size = new System.Drawing.Size(75, 25);
            this.cbxRun.TabIndex = 12;
            this.cbxRun.Text = "Run";
            this.cbxRun.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cbxRun.UseVisualStyleBackColor = false;
            this.cbxRun.CheckedChanged += new System.EventHandler(this.cbxRun_CheckedChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.sbxHiddenCount,
            this.toolStripLabel5,
            this.sbxHiddenCount2,
            this.toolStripLabel2,
            this.sbxEpochs,
            this.toolStripLabel4,
            this.sbxLearningRate,
            this.toolStripLabel3,
            this.sbxMomentum,
            this.sbnSave});
            this.toolStrip1.Location = new System.Drawing.Point(0, 681);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1184, 25);
            this.toolStrip1.TabIndex = 13;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(126, 22);
            this.toolStripLabel1.Text = "Hidden Layer 1 Nodes:";
            // 
            // sbxHiddenCount
            // 
            this.sbxHiddenCount.Name = "sbxHiddenCount";
            this.sbxHiddenCount.Size = new System.Drawing.Size(50, 25);
            this.sbxHiddenCount.TextChanged += new System.EventHandler(this.tsbHiddenCount_TextChanged);
            // 
            // toolStripLabel5
            // 
            this.toolStripLabel5.Name = "toolStripLabel5";
            this.toolStripLabel5.Size = new System.Drawing.Size(126, 22);
            this.toolStripLabel5.Text = "Hidden Layer 2 Nodes:";
            // 
            // sbxHiddenCount2
            // 
            this.sbxHiddenCount2.Name = "sbxHiddenCount2";
            this.sbxHiddenCount2.Size = new System.Drawing.Size(50, 25);
            this.sbxHiddenCount2.TextChanged += new System.EventHandler(this.tsbHiddenCount2_TextChanged);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(106, 22);
            this.toolStripLabel2.Text = "Number of Epochs";
            // 
            // sbxEpochs
            // 
            this.sbxEpochs.Name = "sbxEpochs";
            this.sbxEpochs.Size = new System.Drawing.Size(50, 25);
            this.sbxEpochs.TextChanged += new System.EventHandler(this.tsbEpochs_TextChanged);
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(93, 22);
            this.toolStripLabel4.Text = "Rate of Learning";
            // 
            // sbxLearningRate
            // 
            this.sbxLearningRate.Name = "sbxLearningRate";
            this.sbxLearningRate.Size = new System.Drawing.Size(50, 25);
            this.sbxLearningRate.TextChanged += new System.EventHandler(this.tsbLearningRate_TextChanged);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(112, 22);
            this.toolStripLabel3.Text = "Update Momentum";
            // 
            // sbxMomentum
            // 
            this.sbxMomentum.Name = "sbxMomentum";
            this.sbxMomentum.Size = new System.Drawing.Size(50, 25);
            this.sbxMomentum.TextChanged += new System.EventHandler(this.tsbMomentum_TextChanged);
            // 
            // sbnSave
            // 
            this.sbnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.sbnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.sbnSave.Image = ((System.Drawing.Image)(resources.GetObject("sbnSave.Image")));
            this.sbnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.sbnSave.Name = "sbnSave";
            this.sbnSave.Size = new System.Drawing.Size(80, 22);
            this.sbnSave.Text = "Save Settings";
            this.sbnSave.Click += new System.EventHandler(this.tsbSave_Click);
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.Description = "Select training data folder";
            // 
            // btnTest
            // 
            this.btnTest.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnTest.FlatAppearance.BorderSize = 0;
            this.btnTest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTest.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTest.Location = new System.Drawing.Point(511, 36);
            this.btnTest.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 25);
            this.btnTest.TabIndex = 16;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = false;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // cbxAbort
            // 
            this.cbxAbort.Appearance = System.Windows.Forms.Appearance.Button;
            this.cbxAbort.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cbxAbort.Cursor = System.Windows.Forms.Cursors.Default;
            this.cbxAbort.FlatAppearance.BorderSize = 0;
            this.cbxAbort.FlatAppearance.CheckedBackColor = System.Drawing.Color.Red;
            this.cbxAbort.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cbxAbort.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.cbxAbort.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbxAbort.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxAbort.Location = new System.Drawing.Point(94, 36);
            this.cbxAbort.Name = "cbxAbort";
            this.cbxAbort.Size = new System.Drawing.Size(75, 25);
            this.cbxAbort.TabIndex = 17;
            this.cbxAbort.Text = "Abort";
            this.cbxAbort.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cbxAbort.UseVisualStyleBackColor = false;
            // 
            // toolStrip2
            // 
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sbxData,
            this.sbnData,
            this.sbxWeights,
            this.sbnWeights});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStrip2.Size = new System.Drawing.Size(1184, 25);
            this.toolStrip2.TabIndex = 18;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // sbxData
            // 
            this.sbxData.Name = "sbxData";
            this.sbxData.Size = new System.Drawing.Size(230, 25);
            this.sbxData.TextChanged += new System.EventHandler(this.sbxData_TextChanged);
            // 
            // sbnData
            // 
            this.sbnData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.sbnData.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.sbnData.Image = ((System.Drawing.Image)(resources.GetObject("sbnData.Image")));
            this.sbnData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.sbnData.Name = "sbnData";
            this.sbnData.Size = new System.Drawing.Size(69, 22);
            this.sbnData.Text = "Select Data";
            this.sbnData.Click += new System.EventHandler(this.sbnData_Click);
            // 
            // sbxWeights
            // 
            this.sbxWeights.Name = "sbxWeights";
            this.sbxWeights.Size = new System.Drawing.Size(100, 25);
            this.sbxWeights.TextChanged += new System.EventHandler(this.sbxWeights_TextChanged);
            // 
            // sbnWeights
            // 
            this.sbnWeights.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.sbnWeights.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.sbnWeights.Image = ((System.Drawing.Image)(resources.GetObject("sbnWeights.Image")));
            this.sbnWeights.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.sbnWeights.Name = "sbnWeights";
            this.sbnWeights.Size = new System.Drawing.Size(88, 22);
            this.sbnWeights.Text = "Select Weights";
            this.sbnWeights.Click += new System.EventHandler(this.sbnWeights_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 706);
            this.Controls.Add(this.cbxFFT);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.cbxAbort);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.cbxRun);
            this.Controls.Add(this.cbxTrain);
            this.Controls.Add(this.chartFreq);
            this.Controls.Add(this.rtbConsole);
            this.Controls.Add(this.pbrEpochs);
            this.Controls.Add(this.chartAudio);
            this.Controls.Add(this.chartError);
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Speech Recognition Neural Network";
            ((System.ComponentModel.ISupportInitialize)(this.chartError)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartAudio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartFreq)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataVisualization.Charting.Chart chartError;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartAudio;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ProgressBar pbrEpochs;
        private System.Windows.Forms.RichTextBox rtbConsole;
        private System.Windows.Forms.CheckBox cbxFFT;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartFreq;
        private System.Windows.Forms.CheckBox cbxTrain;
        private System.Windows.Forms.CheckBox cbxRun;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox sbxHiddenCount;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripTextBox sbxEpochs;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripTextBox sbxLearningRate;
        private System.Windows.Forms.ToolStripButton sbnSave;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.ToolStripTextBox sbxMomentum;
        private System.Windows.Forms.CheckBox cbxAbort;
        private System.Windows.Forms.ToolStripLabel toolStripLabel5;
        private System.Windows.Forms.ToolStripTextBox sbxHiddenCount2;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripTextBox sbxData;
        private System.Windows.Forms.ToolStripButton sbnData;
        private System.Windows.Forms.ToolStripTextBox sbxWeights;
        private System.Windows.Forms.ToolStripButton sbnWeights;
    }
}

