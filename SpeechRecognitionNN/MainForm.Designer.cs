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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title3 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title4 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.btnRun = new System.Windows.Forms.Button();
            this.chartError = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.chartAudio = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnTrain = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnData = new System.Windows.Forms.Button();
            this.tbxData = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.chartError)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartAudio)).BeginInit();
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
            chartArea3.Name = "ChartArea1";
            this.chartError.ChartAreas.Add(chartArea3);
            legend3.Enabled = false;
            legend3.Name = "Legend1";
            this.chartError.Legends.Add(legend3);
            this.chartError.Location = new System.Drawing.Point(12, 41);
            this.chartError.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chartError.Name = "chartError";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Legend = "Legend1";
            series3.Name = "MSE";
            this.chartError.Series.Add(series3);
            this.chartError.Size = new System.Drawing.Size(575, 300);
            this.chartError.TabIndex = 1;
            this.chartError.Text = "chart1";
            title3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title3.Name = "chartTitle";
            title3.Text = "Network MSE";
            this.chartError.Titles.Add(title3);
            // 
            // timer1
            // 
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // chartAudio
            // 
            chartArea4.Name = "ChartArea1";
            this.chartAudio.ChartAreas.Add(chartArea4);
            legend4.Enabled = false;
            legend4.Name = "Legend1";
            this.chartAudio.Legends.Add(legend4);
            this.chartAudio.Location = new System.Drawing.Point(596, 41);
            this.chartAudio.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chartAudio.Name = "chartAudio";
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series4.Legend = "Legend1";
            series4.Name = "Input";
            this.chartAudio.Series.Add(series4);
            this.chartAudio.Size = new System.Drawing.Size(575, 300);
            this.chartAudio.TabIndex = 2;
            this.chartAudio.Text = "chart1";
            title4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title4.Name = "chartTitle";
            title4.Text = "Audio Input";
            this.chartAudio.Titles.Add(title4);
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
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 561);
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
    }
}

