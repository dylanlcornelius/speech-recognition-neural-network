using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SpeechRecognitionNN
{
    public partial class MainForm : Form
    {
        /*
        [DllImport(@"C:\Users\Dylan\source\repos\XOR-NN\x64\Debug\XORNN.dll")]
        public static extern IntPtr CreateNetwork();

        [DllImport(@"C:\Users\Dylan\source\repos\XOR-NN\x64\Debug\XORNN.dll")]
        public static extern IntPtr CreateInputs(double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4);

        [DllImport(@"C:\Users\Dylan\source\repos\XOR-NN\x64\Debug\XORNN.dll")]
        public static extern IntPtr CreateExpected(double x1, double x2, double x3, double x4);

        //[DllImport(@"C:\Users\Dylan\source\repos\XOR-NN\x64\Debug\XORNN.dll")]
        //public static extern void Train(IntPtr network, int hiddenCount, int epochs, double learningRate);

        //[DllImport(@"C:\Users\Dylan\source\repos\XOR-NN\x64\Debug\XORNN.dll", CallingConvention = CallingConvention.Cdecl)]
        //public static extern IntPtr Train(IntPtr network, IntPtr inputs, IntPtr expected, int hiddenCount, int epochs, double learningRate);

        [DllImport(@"C:\Users\Dylan\source\repos\XOR-NN\x64\Debug\XORNN.dll")]
        public static extern void Init(IntPtr network, IntPtr inputs, int hiddenCount);

        [DllImport(@"C:\Users\Dylan\source\repos\XOR-NN\x64\Debug\XORNN.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern double Train(IntPtr network, IntPtr inputs, IntPtr expected, double learningRate);
        */

        [DllImport("XORNN.dll")]
        public static extern IntPtr CreateNetwork();

        [DllImport("XORNN.dll")]
        public static extern IntPtr CreateInputs(double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4);

        [DllImport("XORNN.dll")]
        public static extern IntPtr CreateExpected(double x1, double x2, double x3, double x4);

        [DllImport("XORNN.dll")]
        public static extern void Init(IntPtr network, IntPtr inputs, int hiddenCount);

        [DllImport("XORNN.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern double Train(IntPtr network, IntPtr inputs, IntPtr expected, double learningRate);
        

        const int RATE = 44100;
        const int SIZE_BUFFER = 2048;
        const int SIZE_X = 1000;
        const int SIZE_Y = 8000;
        const int SIZE_X2 = 25;
        const int SIZE_Y2 = 800;
        const int TICK = 10;

        string filePath;
        int hiddenCount;
        int epochs;
        double learningRate;

        Thread trainingThread;

        WaveIn input;
        BufferedWaveProvider buffer;
        //Queue<double> queue;

        public MainForm()
        {
            InitializeComponent();

            filePath = "No Training Data Selected";

            hiddenCount = Properties.Settings.Default.HiddenCount;
            epochs = Properties.Settings.Default.Epochs;
            learningRate = Properties.Settings.Default.LearningRate;
            tsbHiddenCount.Text = hiddenCount + "";
            tsbEpochs.Text = epochs + "";
            tsbLearningRate.Text = learningRate + "";


            showInputPlot((bool)Properties.Settings.Default["FFT"]);

            Color gridColor = Color.FromArgb(50, Color.Black);

            chartError.ChartAreas[0].AxisX.Minimum = 0;
            chartError.ChartAreas[0].AxisX.Maximum = epochs;
            chartError.ChartAreas[0].AxisY.Minimum = 0;
            chartError.ChartAreas[0].AxisY.Maximum = 100;
            chartError.ChartAreas[0].AxisX.MajorGrid.LineColor = gridColor;
            chartError.ChartAreas[0].AxisY.MajorGrid.LineColor = gridColor;
            chartError.Series[0].Points.AddXY(0, 0);

            chartAudio.ChartAreas[0].AxisX.Minimum = 0;
            chartAudio.ChartAreas[0].AxisX.Maximum = SIZE_X;
            chartAudio.ChartAreas[0].AxisY.Minimum = -SIZE_Y;
            chartAudio.ChartAreas[0].AxisY.Maximum = SIZE_Y;
            chartAudio.ChartAreas[0].AxisX.Interval = 100;
            chartAudio.ChartAreas[0].AxisX.MajorGrid.LineColor = gridColor;
            chartAudio.ChartAreas[0].AxisY.MajorGrid.LineColor = gridColor;
            chartAudio.Series[0].Points.AddXY(0, 0);

            chartFreq.ChartAreas[0].AxisX.Minimum = -1;
            chartFreq.ChartAreas[0].AxisX.Maximum = SIZE_X2;
            chartFreq.ChartAreas[0].AxisY.Maximum = SIZE_Y2;
            chartFreq.ChartAreas[0].AxisX.Interval = 5;
            chartFreq.ChartAreas[0].AxisX.IntervalOffset = 1;
            chartFreq.ChartAreas[0].AxisX.MajorGrid.LineColor = gridColor;
            chartFreq.ChartAreas[0].AxisY.MajorGrid.LineColor = gridColor;
            chartFreq.Series[0].Points.AddXY(0, 0);

            pbrEpochs.Maximum = epochs;

            input = new WaveIn();
            input.WaveFormat = new WaveFormat(RATE, 1);
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

            int SAMPLE_RESOLUTION = 16;
            int BYTES_PER_POINT = SAMPLE_RESOLUTION / 8;
            Int32[] vals = new Int32[frames.Length / BYTES_PER_POINT];
            double[] Ys = new double[frames.Length / BYTES_PER_POINT];
            double[] Xs = new double[frames.Length / BYTES_PER_POINT];
            double[] Ys2 = new double[frames.Length / BYTES_PER_POINT];
            double[] Xs2 = new double[frames.Length / BYTES_PER_POINT];
            for (int i = 0; i < vals.Length; i++)
            {
                byte hByte = frames[i * 2 + 1];
                byte lByte = frames[i * 2 + 0];
                vals[i] = (int)(short)((hByte << 8) | lByte);
                Xs[i] = i;
                Ys[i] = vals[i];
                Xs2[i] = (double)i / Ys.Length * RATE / 1000.0;
            }

            Series series = new Series();
            series.ChartType = SeriesChartType.FastLine;
            for (int i = 0; i < Xs.Length; i++)
            {
                series.Points.AddXY(Xs[i], Ys[i]);
            }
            chartAudio.Series.Clear();
            chartAudio.Series.Add(series);
            chartAudio.Update();

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
            double[] fft = new double[data.Length];
            Complex[] fftComplex = new Complex[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                fftComplex[i] = new Complex(data[i], 0.0);
            }
            Accord.Math.FourierTransform.FFT(fftComplex, Accord.Math.FourierTransform.Direction.Forward);
            for (int i = 0; i < data.Length; i++)
            {
                fft[i] = fftComplex[i].Magnitude;
            }
            return fft;
        }

        //old btn
        private void btnTrain_Click(object sender, EventArgs e)
        {
            while (true)
            {
                pbrEpochs.Value = 0;

                IntPtr network = CreateNetwork();
                IntPtr inputs = CreateInputs(0, 0, 1, 0, 0, 1, 1, 1);
                IntPtr expected = CreateExpected(0, 1, 1, 0);
                //Train(network, 16, 2000, .9);
                //string s = Marshal.PtrToStringAnsi(Train(network, inputs, expected, 16, 500, .9));
                //IntPtr ptr = Train(network, inputs, expected, 16, 500, .8);
                //double[] epochMSE = new double[epochs];
                //Marshal.Copy(ptr, epochMSE, 0, epochs);

                Init(network, inputs, Properties.Settings.Default.HiddenCount);

                Series series = new Series();
                series.ChartType = SeriesChartType.FastLine;
                chartError.Series.Clear();
                chartError.Series.Add(series);
                for (int i = 0; i < Properties.Settings.Default.Epochs; i++)
                {
                    double d = Train(network, inputs, expected, Properties.Settings.Default.LearningRate);

                    series.Points.AddXY(i, d);
                    chartError.Update();

                    rtbConsole.AppendText("Epoch: " + i + " Error: " + d + "\n");
                    rtbConsole.ScrollToCaret();
                    pbrEpochs.PerformStep();

                    if (d < 0.1)
                    {
                        pbrEpochs.Value = pbrEpochs.Maximum;
                        break;
                    }
                }

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
        }

        private void cbxTrain_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxTrain.Checked && trainingThread == null)
            {
                trainingThread = new Thread(StartTraining);
                trainingThread.IsBackground = true;
                trainingThread.Start();
            }
            else if (cbxTrain.Checked && !trainingThread.IsAlive)
            {
                trainingThread = new Thread(StartTraining);
                trainingThread.IsBackground = true;
                trainingThread.Start();
            }
        }

        private void StartTraining()
        {
            while (cbxTrain.Checked)
            {
                pbrEpochs.Invoke(new Action(() => pbrEpochs.Value = 0));

                IntPtr network = CreateNetwork();
                IntPtr inputs = CreateInputs(0, 0, 1, 0, 0, 1, 1, 1);
                IntPtr expected = CreateExpected(0, 1, 1, 0);

                Init(network, inputs, hiddenCount);

                //playground
                if (!filePath.Equals("No Training Data Selected"))
                {
                    using (WaveFileReader reader = new WaveFileReader(filePath))
                    {
                        int pow = (int)Math.Pow(2, Math.Ceiling(Math.Log(reader.Length) / Math.Log(2)));

                        for (int j = 0; j < pow; j += SIZE_BUFFER)
                        {
                            Console.WriteLine(j);
                            byte[] bytesBuffer = new byte[SIZE_BUFFER];
                            reader.Read(bytesBuffer, 0, bytesBuffer.Length);
                            //int read = reader.Read(bytesBuffer, 0, bytesBuffer.Length);
                            //int[] sampleBuffer = new int[read / 2];
                            //Buffer.BlockCopy(bytesBuffer, 0, sampleBuffer, 0, read);

                            int SAMPLE_RESOLUTION = 16;
                            int BYTES_PER_POINT = SAMPLE_RESOLUTION / 8;
                            Int32[] vals = new Int32[bytesBuffer.Length / BYTES_PER_POINT];
                            double[] Ys = new double[bytesBuffer.Length / BYTES_PER_POINT];
                            double[] Xs = new double[bytesBuffer.Length / BYTES_PER_POINT];
                            double[] Ys2 = new double[bytesBuffer.Length / BYTES_PER_POINT];
                            double[] Xs2 = new double[bytesBuffer.Length / BYTES_PER_POINT];
                            for (int i = 0; i < vals.Length; i++)
                            {
                                byte hByte = bytesBuffer[i * 2 + 1];
                                byte lByte = bytesBuffer[i * 2 + 0];
                                vals[i] = (int)(short)((hByte << 8) | lByte);
                                Xs[i] = i;
                                Ys[i] = vals[i];
                                Xs2[i] = (double)i / Ys.Length * RATE / 1000.0;
                            }

                            /*
                            foreach (double b in Ys)
                            {
                                Console.WriteLine(b);
                            }
                            */

                            Series series2 = new Series();
                            series2.ChartType = SeriesChartType.FastLine;
                            for (int i = 0; i < Xs.Length; i++)
                            {
                                series2.Points.AddXY(Xs[i], Ys[i]);
                            }
                            chartAudio.Invoke(new Action(() => chartAudio.Series.Clear()));
                            chartAudio.Invoke(new Action(() => chartAudio.Series.Add(series2)));
                            chartAudio.Invoke(new Action(() => chartAudio.Update()));

                            Ys2 = FFT(Ys);
                            series2 = new Series();
                            series2.ChartType = SeriesChartType.FastLine;
                            for (int i = 0; i < Xs2.Length / 2; i++)
                            {
                                series2.Points.AddXY(Xs2.Take(Xs2.Length / 2).ToArray()[i], Ys2.Take(Ys2.Length / 2).ToArray()[i]);
                            }
                            chartFreq.Invoke(new Action(() => chartFreq.Series.Clear()));
                            chartFreq.Invoke(new Action(() => chartFreq.Series.Add(series2)));
                            chartFreq.Invoke(new Action(() => chartFreq.Update()));
                        }
                    }
                }
                

                Series series = new Series();
                series.ChartType = SeriesChartType.FastLine;
                chartError.Invoke(new Action(() => chartError.ChartAreas[0].AxisX.Maximum = epochs));
                chartError.Invoke(new Action(() => chartError.Series.Clear()));
                chartError.Invoke(new Action(() => chartError.Series.Add(series)));
                for (int i = 0; i < epochs; i++)
                {
                    double d = Train(network, inputs, expected, learningRate);

                    chartError.Invoke(new Action(() => series.Points.AddXY(i, d)));
                    chartError.Invoke(new Action(() => chartError.Update()));

                    rtbConsole.Invoke(new Action(() => rtbConsole.AppendText("Epoch: " + i + " Error: " + d + "\n")));
                    rtbConsole.Invoke(new Action(() => rtbConsole.ScrollToCaret()));
                    pbrEpochs.Invoke(new Action(() => pbrEpochs.PerformStep()));
                    

                    if (d < 0.01)
                    {
                        pbrEpochs.Invoke(new Action(() => pbrEpochs.Value = pbrEpochs.Maximum));
                        break;
                    }
                }
                rtbConsole.Invoke(new Action(() => rtbConsole.AppendText("Training complete!\n")));
                rtbConsole.Invoke(new Action(() => rtbConsole.ScrollToCaret()));

                Thread.Sleep(2000);
            }
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

        private void cbxRun_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxRun.Checked)
            {
                input.StartRecording();
                timer1.Start();
            }
            else
            {
                input.StopRecording();
                timer1.Stop();
            }
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default["HiddenCount"] = hiddenCount;
            Properties.Settings.Default["Epochs"] = epochs;
            Properties.Settings.Default["LearningRate"] = learningRate;
            Properties.Settings.Default.Save();
        }

        private void tbxData_TextChanged(object sender, EventArgs e)
        {
            if (File.Exists(tbxData.Text))
            {
                if (Path.GetExtension(tbxData.Text) == ".wav")
                {
                    this.filePath = tbxData.Text;
                }
                else
                {
                    tbxData.Text = "No Training Data Selected";
                }
            }
            else
            {
                tbxData.Text = "No Training Data Selected";
            }
        }

        private void tsbHiddenCount_TextChanged(object sender, EventArgs e)
        {
            if (Int32.TryParse(tsbHiddenCount.Text, out int hiddenCount))
            {
                this.hiddenCount = hiddenCount;
            }
            else
            {
                tsbHiddenCount.Text = "";
            }
        }

        private void tsbEpochs_TextChanged(object sender, EventArgs e)
        {
            if (Int32.TryParse(tsbEpochs.Text, out int epochs))
            {
                this.epochs = epochs;
            }
            else
            {
                tsbEpochs.Text = "";
            }
        }

        private void tsbLearningRate_TextChanged(object sender, EventArgs e)
        {
            if (Double.TryParse(tsbLearningRate.Text, out double learningRate))
            {
                this.learningRate = learningRate;
            }
            else
            {
                tsbLearningRate.Text = "";
            }
        }
    }
}
