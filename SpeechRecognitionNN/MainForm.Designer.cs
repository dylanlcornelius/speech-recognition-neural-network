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
            this.btnRun = new System.Windows.Forms.Button();
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
            ((System.ComponentModel.ISupportInitialize)(this.chartError)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartAudio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartFreq)).BeginInit();
            this.SuspendLayout();
            // 
            // btnRun
            // 
            this.btnRun.BackColor = System.Drawing.Color.DarkGray;
            this.btnRun.FlatAppearance.BorderSize = 0;
            this.btnRun.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRun.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRun.Location = new System.Drawing.Point(1097, 12);
            this.btnRun.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(74, 23);
            this.btnRun.TabIndex = 0;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = false;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // chartError
            // 
            chartArea1.Name = "ChartArea1";
            this.chartError.ChartAreas.Add(chartArea1);
            legend1.Enabled = false;
            legend1.Name = "Legend1";
            this.chartError.Legends.Add(legend1);
            this.chartError.Location = new System.Drawing.Point(12, 41);
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
            this.chartAudio.Location = new System.Drawing.Point(596, 41);
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
            // btnTrain
            // 
            this.btnTrain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnTrain.FlatAppearance.BorderSize = 0;
            this.btnTrain.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTrain.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTrain.Location = new System.Drawing.Point(12, 12);
            this.btnTrain.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnTrain.Name = "btnTrain";
            this.btnTrain.Size = new System.Drawing.Size(74, 23);
            this.btnTrain.TabIndex = 3;
            this.btnTrain.Text = "Train";
            this.btnTrain.UseVisualStyleBackColor = false;
            this.btnTrain.Click += new System.EventHandler(this.btnTrain_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "txt";
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            // 
            // btnData
            // 
            this.btnData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnData.FlatAppearance.BorderSize = 0;
            this.btnData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnData.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnData.Location = new System.Drawing.Point(330, 12);
            this.btnData.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnData.Name = "btnData";
            this.btnData.Size = new System.Drawing.Size(104, 23);
            this.btnData.TabIndex = 5;
            this.btnData.Text = "Select Data";
            this.btnData.UseVisualStyleBackColor = false;
            this.btnData.Click += new System.EventHandler(this.btnData_Click);
            // 
            // tbxData
            // 
            this.tbxData.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxData.Location = new System.Drawing.Point(95, 12);
            this.tbxData.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbxData.Name = "tbxData";
            this.tbxData.Size = new System.Drawing.Size(231, 25);
            this.tbxData.TabIndex = 6;
            this.tbxData.Text = "No Training Data Selected";
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
            chartArea3.Name = "ChartArea1";
            this.chartFreq.ChartAreas.Add(chartArea3);
            legend3.Enabled = false;
            legend3.Name = "Legend1";
            this.chartFreq.Legends.Add(legend3);
            this.chartFreq.Location = new System.Drawing.Point(598, 40);
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
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 661);
            this.Controls.Add(this.chartFreq);
            this.Controls.Add(this.cbxFFT);
            this.Controls.Add(this.rtbConsole);
            this.Controls.Add(this.pbrEpochs);
            this.Controls.Add(this.tbxData);
            this.Controls.Add(this.btnData);
            this.Controls.Add(this.btnTrain);
            this.Controls.Add(this.chartAudio);
            this.Controls.Add(this.chartError);
            this.Controls.Add(this.btnRun);
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Speech Recognition Neural Network";
            ((System.ComponentModel.ISupportInitialize)(this.chartError)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartAudio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartFreq)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRun;
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
    }
}

