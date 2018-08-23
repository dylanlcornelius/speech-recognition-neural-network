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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea7 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend7 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series7 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title7 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea8 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend8 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series8 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title8 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea9 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend9 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series9 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title9 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.chartError = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.chartAudio = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnTrain = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnData = new System.Windows.Forms.Button();
            this.tbxData = new System.Windows.Forms.TextBox();
            this.pbrEpochs = new System.Windows.Forms.ProgressBar();
            this.rtbConsole = new System.Windows.Forms.RichTextBox();
            this.cbxFFT = new System.Windows.Forms.CheckBox();
            this.chartFreq = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.cbxTrain = new System.Windows.Forms.CheckBox();
            this.cbxRun = new System.Windows.Forms.CheckBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tsbHiddenCount = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tsbEpochs = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.tsbLearningRate = new System.Windows.Forms.ToolStripTextBox();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.chartError)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartAudio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartFreq)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chartError
            // 
            chartArea7.Name = "ChartArea1";
            this.chartError.ChartAreas.Add(chartArea7);
            legend7.Enabled = false;
            legend7.Name = "Legend1";
            this.chartError.Legends.Add(legend7);
            this.chartError.Location = new System.Drawing.Point(12, 41);
            this.chartError.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chartError.Name = "chartError";
            series7.ChartArea = "ChartArea1";
            series7.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series7.Legend = "Legend1";
            series7.Name = "MSE";
            this.chartError.Series.Add(series7);
            this.chartError.Size = new System.Drawing.Size(575, 300);
            this.chartError.TabIndex = 1;
            this.chartError.Text = "chart1";
            title7.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title7.Name = "chartTitle";
            title7.Text = "Network MSE";
            this.chartError.Titles.Add(title7);
            // 
            // timer1
            // 
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // chartAudio
            // 
            chartArea8.Name = "ChartArea1";
            this.chartAudio.ChartAreas.Add(chartArea8);
            legend8.Enabled = false;
            legend8.Name = "Legend1";
            this.chartAudio.Legends.Add(legend8);
            this.chartAudio.Location = new System.Drawing.Point(596, 41);
            this.chartAudio.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chartAudio.Name = "chartAudio";
            series8.ChartArea = "ChartArea1";
            series8.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series8.Legend = "Legend1";
            series8.Name = "Input";
            this.chartAudio.Series.Add(series8);
            this.chartAudio.Size = new System.Drawing.Size(575, 300);
            this.chartAudio.TabIndex = 2;
            this.chartAudio.Text = "chart1";
            title8.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title8.Name = "chartTitle";
            title8.Text = "Audio Input";
            this.chartAudio.Titles.Add(title8);
            // 
            // btnTrain
            // 
            this.btnTrain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnTrain.FlatAppearance.BorderSize = 0;
            this.btnTrain.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTrain.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTrain.Location = new System.Drawing.Point(513, 12);
            this.btnTrain.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnTrain.Name = "btnTrain";
            this.btnTrain.Size = new System.Drawing.Size(74, 23);
            this.btnTrain.TabIndex = 3;
            this.btnTrain.Text = "Train";
            this.btnTrain.UseVisualStyleBackColor = false;
            this.btnTrain.Visible = false;
            this.btnTrain.Click += new System.EventHandler(this.btnTrain_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "wav";
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Text files (*.wav)|*.wav|All files (*.*)|*.*";
            this.openFileDialog1.InitialDirectory = "C:\\Users\\Dylan\\Desktop";
            // 
            // btnData
            // 
            this.btnData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnData.FlatAppearance.BorderSize = 0;
            this.btnData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnData.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnData.Location = new System.Drawing.Point(327, 9);
            this.btnData.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnData.Name = "btnData";
            this.btnData.Size = new System.Drawing.Size(100, 25);
            this.btnData.TabIndex = 5;
            this.btnData.Text = "Select Data";
            this.btnData.UseVisualStyleBackColor = false;
            this.btnData.Click += new System.EventHandler(this.btnData_Click);
            // 
            // tbxData
            // 
            this.tbxData.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxData.Location = new System.Drawing.Point(92, 9);
            this.tbxData.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbxData.Name = "tbxData";
            this.tbxData.Size = new System.Drawing.Size(231, 25);
            this.tbxData.TabIndex = 6;
            this.tbxData.Text = "No Training Data Selected";
            this.tbxData.TextChanged += new System.EventHandler(this.tbxData_TextChanged);
            // 
            // pbrEpochs
            // 
            this.pbrEpochs.Location = new System.Drawing.Point(12, 626);
            this.pbrEpochs.Name = "pbrEpochs";
            this.pbrEpochs.Size = new System.Drawing.Size(1159, 23);
            this.pbrEpochs.Step = 1;
            this.pbrEpochs.TabIndex = 7;
            // 
            // rtbConsole
            // 
            this.rtbConsole.Location = new System.Drawing.Point(12, 347);
            this.rtbConsole.Name = "rtbConsole";
            this.rtbConsole.ReadOnly = true;
            this.rtbConsole.Size = new System.Drawing.Size(1159, 273);
            this.rtbConsole.TabIndex = 8;
            this.rtbConsole.Text = "";
            // 
            // cbxFFT
            // 
            this.cbxFFT.AutoSize = true;
            this.cbxFFT.Location = new System.Drawing.Point(596, 17);
            this.cbxFFT.Name = "cbxFFT";
            this.cbxFFT.Size = new System.Drawing.Size(106, 17);
            this.cbxFFT.TabIndex = 9;
            this.cbxFFT.Text = "Show Frequency";
            this.cbxFFT.UseVisualStyleBackColor = true;
            this.cbxFFT.CheckedChanged += new System.EventHandler(this.cbxFFT_CheckedChanged);
            // 
            // chartFreq
            // 
            chartArea9.Name = "ChartArea1";
            this.chartFreq.ChartAreas.Add(chartArea9);
            legend9.Enabled = false;
            legend9.Name = "Legend1";
            this.chartFreq.Legends.Add(legend9);
            this.chartFreq.Location = new System.Drawing.Point(598, 40);
            this.chartFreq.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chartFreq.Name = "chartFreq";
            series9.ChartArea = "ChartArea1";
            series9.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series9.Legend = "Legend1";
            series9.Name = "Input";
            this.chartFreq.Series.Add(series9);
            this.chartFreq.Size = new System.Drawing.Size(575, 300);
            this.chartFreq.TabIndex = 10;
            this.chartFreq.Text = "chart1";
            title9.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title9.Name = "chartTitle";
            title9.Text = "Audio Frequency";
            this.chartFreq.Titles.Add(title9);
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
            this.cbxTrain.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxTrain.Location = new System.Drawing.Point(12, 9);
            this.cbxTrain.Name = "cbxTrain";
            this.cbxTrain.Size = new System.Drawing.Size(75, 25);
            this.cbxTrain.TabIndex = 11;
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
            this.cbxRun.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxRun.Location = new System.Drawing.Point(1096, 8);
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
            this.tsbHiddenCount,
            this.toolStripLabel2,
            this.tsbEpochs,
            this.toolStripLabel3,
            this.tsbLearningRate,
            this.tsbSave});
            this.toolStrip1.Location = new System.Drawing.Point(0, 656);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1184, 25);
            this.toolStrip1.TabIndex = 13;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(136, 22);
            this.toolStripLabel1.Text = "Count of Hidden Nodes:";
            // 
            // tsbHiddenCount
            // 
            this.tsbHiddenCount.Name = "tsbHiddenCount";
            this.tsbHiddenCount.Size = new System.Drawing.Size(100, 25);
            this.tsbHiddenCount.TextChanged += new System.EventHandler(this.tsbHiddenCount_TextChanged);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(106, 22);
            this.toolStripLabel2.Text = "Number of Epochs";
            // 
            // tsbEpochs
            // 
            this.tsbEpochs.Name = "tsbEpochs";
            this.tsbEpochs.Size = new System.Drawing.Size(100, 25);
            this.tsbEpochs.TextChanged += new System.EventHandler(this.tsbEpochs_TextChanged);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(93, 22);
            this.toolStripLabel3.Text = "Rate of Learning";
            // 
            // tsbLearningRate
            // 
            this.tsbLearningRate.Name = "tsbLearningRate";
            this.tsbLearningRate.Size = new System.Drawing.Size(100, 25);
            this.tsbLearningRate.TextChanged += new System.EventHandler(this.tsbLearningRate_TextChanged);
            // 
            // tsbSave
            // 
            this.tsbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbSave.Image = ((System.Drawing.Image)(resources.GetObject("tsbSave.Image")));
            this.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSave.Name = "tsbSave";
            this.tsbSave.Size = new System.Drawing.Size(80, 22);
            this.tsbSave.Text = "Save Settings";
            this.tsbSave.Click += new System.EventHandler(this.tsbSave_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 681);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.cbxRun);
            this.Controls.Add(this.cbxTrain);
            this.Controls.Add(this.chartFreq);
            this.Controls.Add(this.cbxFFT);
            this.Controls.Add(this.rtbConsole);
            this.Controls.Add(this.pbrEpochs);
            this.Controls.Add(this.tbxData);
            this.Controls.Add(this.btnData);
            this.Controls.Add(this.btnTrain);
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataVisualization.Charting.Chart chartError;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartAudio;
        private System.Windows.Forms.Button btnTrain;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnData;
        private System.Windows.Forms.TextBox tbxData;
        private System.Windows.Forms.ProgressBar pbrEpochs;
        private System.Windows.Forms.RichTextBox rtbConsole;
        private System.Windows.Forms.CheckBox cbxFFT;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartFreq;
        private System.Windows.Forms.CheckBox cbxTrain;
        private System.Windows.Forms.CheckBox cbxRun;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox tsbHiddenCount;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripTextBox tsbEpochs;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripTextBox tsbLearningRate;
        private System.Windows.Forms.ToolStripButton tsbSave;
    }
}

