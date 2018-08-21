using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SpeechRecognitionNN
{
    public partial class MainForm : Form
    {
        [DllImport(@"C:\Users\Dylan\source\repos\XOR-NN\x64\Debug\XORNN.dll")]
        public static extern IntPtr CreateNetwork();

        [DllImport(@"C:\Users\Dylan\source\repos\XOR-NN\x64\Debug\XORNN.dll")]
        public static extern IntPtr CreateInputs(double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4);

        [DllImport(@"C:\Users\Dylan\source\repos\XOR-NN\x64\Debug\XORNN.dll")]
        public static extern IntPtr CreateExpected(double x1, double x2, double x3, double x4);

        //[DllImport(@"C:\Users\Dylan\source\repos\XOR-NN\x64\Debug\XORNN.dll")]
        //public static extern void Train(IntPtr network, );

        [DllImport(@"C:\Users\Dylan\source\repos\XOR-NN\x64\Debug\XORNN.dll")]
        public static extern int HelloWorld();

        const int RATE = 44100;
        const int SIZE_BUFFER = 2048;
        const int SIZE_X = 1000;
        const int SIZE_Y = 8000;
        const int SIZE_X2 = 25;
        const int SIZE_Y2 = 800;
        const int TICK = 10;

        const int epochs = 500;

        bool isRunning;

        WaveIn input;
        BufferedWaveProvider buffer;
        //Queue<double> queue;

        public MainForm()
        {
            InitializeComponent();

            isRunning = false;

            showInputPlot((bool)Properties.Settings.Default["FFT"]);

            Color gridColor = Color.FromArgb(50, Color.Black);

            chartAudio.ChartAreas[0].AxisX.Minimum = 0;
            chartAudio.ChartAreas[0].AxisX.Maximum = SIZE_X;
            chartAudio.ChartAreas[0].AxisY.Minimum = -SIZE_Y;
            chartAudio.ChartAreas[0].AxisY.Maximum = SIZE_Y;
            chartAudio.ChartAreas[0].AxisX.Interval = 100;
            chartAudio.ChartAreas[0].AxisX.MajorGrid.LineColor = gridColor;
            chartAudio.ChartAreas[0].AxisY.MajorGrid.LineColor = gridColor;

            chartFreq.ChartAreas[0].AxisX.Minimum = -1;
            chartFreq.ChartAreas[0].AxisX.Maximum = SIZE_X2;
            chartFreq.ChartAreas[0].AxisY.Maximum = SIZE_Y2;
            chartFreq.ChartAreas[0].AxisX.Interval = 5;
            chartFreq.ChartAreas[0].AxisX.IntervalOffset = 1;
            chartFreq.ChartAreas[0].AxisX.MajorGrid.LineColor = gridColor;
            chartFreq.ChartAreas[0].AxisY.MajorGrid.LineColor = gridColor;

            pbrEpochs.Maximum = epochs;

            input = new WaveIn();
            input.WaveFormat = new WaveFormat(RATE, 1);
            //input.BufferMilliseconds = 100;
            input.BufferMilliseconds = (int)((double)SIZE_BUFFER / (double)RATE * 1000.0);
            input.DataAvailable += new EventHandler<WaveInEventArgs>(DataAvailable);

            buffer = new BufferedWaveProvider(input.WaveFormat);
            buffer.BufferLength = SIZE_BUFFER * 2;
            buffer.DiscardOnBufferOverflow = true;

            //queue = new Queue<double>(Enumerable.Repeat(0.0, SIZE_X).ToList());

            //chartAudio.Series["Input"].Points.DataBindY(queue);
            //chartFreq.Series["Input"].Points.DataBindY(queue);

            timer1.Interval = TICK;
        }

        private void DataAvailable(object sender, WaveInEventArgs e)
        {
            buffer.AddSamples(e.Buffer, 0, e.BytesRecorded);
            /*
            for (int i = 0; i < e.BytesRecorded; i += 2)
            {
                queue.Enqueue(BitConverter.ToInt16(e.Buffer, i));
                queue.Dequeue();
            }
            */
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //chartAudio.Series["Input"].Points.DataBindY(queue);

            int frameSize = SIZE_BUFFER;
            var frames = new byte[frameSize];
            buffer.Read(frames, 0, frameSize);
            if (frames.Length == 0) return;
            if (frames[frameSize - 2] == 0) return;

            timer1.Enabled = false;

            // convert it to int32 manually (and a double for scottplot)
            int SAMPLE_RESOLUTION = 16;
            int BYTES_PER_POINT = SAMPLE_RESOLUTION / 8;
            Int32[] vals = new Int32[frames.Length / BYTES_PER_POINT];
            double[] Ys = new double[frames.Length / BYTES_PER_POINT];
            double[] Xs = new double[frames.Length / BYTES_PER_POINT];
            double[] Ys2 = new double[frames.Length / BYTES_PER_POINT];
            double[] Xs2 = new double[frames.Length / BYTES_PER_POINT];
            for (int i = 0; i < vals.Length; i++)
            {
                // bit shift the byte buffer into the right variable format
                byte hByte = frames[i * 2 + 1];
                byte lByte = frames[i * 2 + 0];
                vals[i] = (int)(short)((hByte << 8) | lByte);
                Xs[i] = i;
                Ys[i] = vals[i];
                Xs2[i] = (double)i / Ys.Length * RATE / 1000.0; // units are in kHz
            }

            // update scottplot (PCM, time domain)
            Series series = new Series();
            series.ChartType = SeriesChartType.FastLine;
            for (int i = 0; i < Xs.Length; i++)
            {
                series.Points.AddXY(Xs[i], Ys[i]);
            }
            chartAudio.Series.Clear();
            chartAudio.Series.Add(series);
            chartAudio.Update();

            //update scottplot (FFT, frequency domain)
            Ys2 = FFT(Ys);
            series = new Series();
            series.ChartType = SeriesChartType.FastLine;
            for (int i = 0; i < Xs2.Length / 2; i++)
            {
                series.Points.AddXY(Xs2.Take(Xs2.Length / 2).ToArray()[i], Ys2.Take(Ys2.Length / 2).ToArray()[i]);
            }
            chartFreq.Series.Clear();
            chartFreq.Series.Add(series);
            chartFreq.Update();

            timer1.Enabled = true;
        }

        private double[] FFT(double[] data)
        {
            double[] fft = new double[data.Length]; // this is where we will store the output (fft)
            Complex[] fftComplex = new Complex[data.Length]; // the FFT function requires complex format
            for (int i = 0; i < data.Length; i++)
            {
                fftComplex[i] = new Complex(data[i], 0.0); // make it complex format (imaginary = 0)
            }
            Accord.Math.FourierTransform.FFT(fftComplex, Accord.Math.FourierTransform.Direction.Forward);
            for (int i = 0; i < data.Length; i++)
            {
                fft[i] = fftComplex[i].Magnitude; // back to double
                //fft[i] = Math.Log10(fft[i]); // convert to dB
            }
            return fft;
            //todo: this could be much faster by reusing variables
        }

        private void btnTrain_Click(object sender, EventArgs e)
        {
            rtbConsole.AppendText(HelloWorld()+"");
            /*
            pbrEpochs.Increment(-epochs);
            for (int i = 0; i < epochs; i++)
            {
                pbrEpochs.PerformStep();
            }
            */
            rtbConsole.AppendText("Training complete!\n");
            rtbConsole.ScrollToCaret();
        }

        private void btnData_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                tbxData.Text = openFileDialog1.FileName;
            }
        }

        private void cbxFFT_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxFFT.Checked)
            {
                Properties.Settings.Default["FFT"] = true;
            }
            else
            {
                Properties.Settings.Default["FFT"] = false;
            }
            Properties.Settings.Default.Save();
            showInputPlot((bool)Properties.Settings.Default["FFT"]);
        }

        private void showInputPlot(bool isFFT)
        {
            if (isFFT)
            {
                if (!cbxFFT.Checked)
                {
                    cbxFFT.Checked = true;
                }
                chartAudio.Hide();
                chartFreq.Show();
            }
            else
            {
                chartAudio.Show();
                chartFreq.Hide();
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            if (isRunning = !isRunning)
            {
                input.StartRecording();
                timer1.Start();
                btnRun.BackColor = Color.Red;
            }
            else
            {
                input.StopRecording();
                timer1.Stop();
                btnRun.BackColor = Color.DarkGray;
            }
        }
    }
}
