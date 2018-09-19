using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        
        [DllImport(@"C:\Users\Dylan\source\repos\XOR-NN\x64\Debug\XORNN.dll")]
        public static extern IntPtr CreateNetwork();

        //[DllImport(@"C:\Users\Dylan\source\repos\XOR-NN\x64\Debug\XORNN.dll")]
        //public static extern IntPtr CreateMatrixList(double[] data, int rows, int columns);

        [DllImport(@"C:\Users\Dylan\source\repos\XOR-NN\x64\Debug\XORNN.dll")]
        public static extern IntPtr CreateMatrixVector(double[] data, int rows, int columns);

        [DllImport(@"C:\Users\Dylan\source\repos\XOR-NN\x64\Debug\XORNN.dll")]
        public static extern void Init(IntPtr network, int inputCount, int hiddenCount, int hiddenCount2, int outputCount, [MarshalAs(UnmanagedType.LPStr)] string weightsPath);

        [DllImport(@"C:\Users\Dylan\source\repos\XOR-NN\x64\Debug\XORNN.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern double Train(IntPtr network, IntPtr inputs, IntPtr expected, double learningRate, double momentum);

        [DllImport(@"C:\Users\Dylan\source\repos\XOR-NN\x64\Debug\XORNN.dll")]
        public static extern void Export(IntPtr network, [MarshalAs(UnmanagedType.LPStr)] string weightsPath);

        [DllImport(@"C:\Users\Dylan\source\repos\XOR-NN\x64\Debug\XORNN.dll")]
        public static extern int GetInputSize(IntPtr network, [MarshalAs(UnmanagedType.LPStr)] string weightsPath);

        [DllImport(@"C:\Users\Dylan\source\repos\XOR-NN\x64\Debug\XORNN.dll")]
        public static extern IntPtr Run(IntPtr network, double[] data, int columns);

        /*
        [DllImport("XORNN.dll")]
        public static extern IntPtr CreateNetwork();

        [DllImport("XORNN.dll")]
        public static extern IntPtr CreateInputs(double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4);

        [DllImport("XORNN.dll")]
        public static extern IntPtr CreateExpected(double x1, double x2, double x3, double x4);

        [DllImport("XORNN.dll")]
        public static extern void Init(IntPtr network, IntPtr inputs, int hiddenCount, int outputCount);

        [DllImport("XORNN.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern double Train(IntPtr network, IntPtr inputs, IntPtr expected, double learningRate);
        */

        //private const int RATE = 44100;
        private const int RATE = 16000;
        //private const int SIZE_BUFFER = 2048;
        private const int SIZE_BUFFER = 1024;
        private const int SIZE_X = 1000;
        private const int SIZE_Y = 8000;
        private const int SIZE_X2 = 25;
        private const int SIZE_Y2 = 100; //800
        private const int TICK = 10;

        /*
        private Dictionary<char, double[]> l2n = new Dictionary<char, double[]>()
        {
            { 'h', new double[]{ 0, 1, 1, 1, 1 } },
            { 'e', new double[]{ 1, 0, 1, 1, 1 } },
            { 'l', new double[]{ 1, 1, 0, 1, 1 } },
            { 'o', new double[]{ 1, 1, 1, 0, 1 } },
            { ' ', new double[]{ 1, 1, 1, 1, 0 } }
        };
        */
        
        private Dictionary<char, double[]> l2n = new Dictionary<char, double[]>()
        {
            { 'h', new double[]{ 1, 0, 0, 0 } },
            { 'e', new double[]{ 0, 1, 0, 0 } },
            { 'l', new double[]{ 0, 0, 1, 0 } },
            { 'o', new double[]{ 0, 0, 0, 1 } },
        };
        

        private Dictionary<int, char> n2l = new Dictionary<int, char>()
        {
            { 0, 'h' },
            { 1, 'e' },
            { 2, 'l' },
            { 3, 'o' },
        };

        private IntPtr network;

        private string dataPath;
        private string[] data;
        private string[] labels;
        private string weightsPath;
        private int hiddenCount;
        private int hiddenCount2;
        private int outputCount;
        private int epochs;
        private double learningRate;
        private double momentum;

        private Thread trainingThread;

        private WaveIn input;
        private BufferedWaveProvider buffer;

        public MainForm()
        {
            InitializeComponent();

            #region SETTINGS

            dataPath = Properties.Settings.Default.DataPath;
            sbxData.Text = dataPath;
            LoadData(sbxData.Text);
            weightsPath = Properties.Settings.Default.WeightsPath;
            sbxWeights.Text = weightsPath;
            hiddenCount = Properties.Settings.Default.HiddenCount;
            hiddenCount2 = Properties.Settings.Default.HiddenCount2;
            //outputCount = (int)Math.Ceiling( Math.Log(l2n.Count) / Math.Log(2));
            outputCount = 4;
            epochs = Properties.Settings.Default.Epochs;
            learningRate = Properties.Settings.Default.LearningRate;
            momentum = Properties.Settings.Default.Momentum;
            sbxHiddenCount.Text = hiddenCount + "";
            sbxHiddenCount2.Text = hiddenCount2 + "";
            sbxEpochs.Text = epochs + "";
            sbxLearningRate.Text = learningRate + "";
            sbxMomentum.Text = momentum + "";

            #endregion

            #region CHARTS INITIALIZATION

            showInputPlot((bool)Properties.Settings.Default.FFT);

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

            #endregion

            rtbConsole.SelectionTabs = new int[] { 100, 200, 300, 400 };
            //pbrEpochs.Maximum = epochs;

            input = new WaveIn();
            input.WaveFormat = new WaveFormat(RATE, 1);
            //input.BufferMilliseconds = (int)((double)SIZE_BUFFER / RATE * 1000.0);
            input.BufferMilliseconds = 10;
            input.DataAvailable += new EventHandler<WaveInEventArgs>(DataAvailable);

            buffer = new BufferedWaveProvider(input.WaveFormat);
            buffer.BufferLength = 160 * 2;
            buffer.DiscardOnBufferOverflow = true;

            timer1.Interval = TICK;
        }

        /// <summary>
        /// Adds microphone input to a buffer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataAvailable(object sender, WaveInEventArgs e)
        {
            buffer.AddSamples(e.Buffer, 0, e.BytesRecorded);
        }

        /// <summary>
        /// Processes microphone input when the buffer is full
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            var frames = new byte[512];
            buffer.Read(frames, 0, 512);
            if (frames.Length == 0) return;
            if (frames[320 - 2] == 0) return;

            timer1.Enabled = false;

            /*
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
                vals[i] = (short)((hByte << 8) | lByte);
                Xs[i] = i;
                Ys[i] = vals[i];
                Xs2[i] = (double)i / Ys.Length * RATE / 1000.0;
            }
            */

            //List<double[]> FFTsList = new List<double[]>();
            double[] d = new double[256];
            for (int i = 0; i < 512; i+= 2)
            {
                double c = (short)((frames[i + 1] << 8) | frames[i]);
                c = (c / 32768.0);
                d[i / 2] = c;
            }
            //FFTsList.Add(FFT(d));
            double[] FFTs = FFT(d);

            //for (int i = 0; i < 256; i++)
            //{
            //    Console.WriteLine(Ys[i] + ":" + d[i]);
            //}

            /*
            Series series = new Series();
            series.ChartType = SeriesChartType.FastLine;
            for (int i = 0; i < Xs.Length; i++)
            {
                series.Points.AddXY(Xs[i], Ys[i]);
            }
            chartAudio.Series.Clear();
            chartAudio.Series.Add(series);
            chartAudio.Update();
            */

            //Ys2 = FFT(Ys);
            /*
            series = new Series();
            series.ChartType = SeriesChartType.FastLine;
            for (int i = 0; i < Xs2.Length / 2; i++)
            {
                series.Points.AddXY(Xs2.Take(Xs2.Length / 2).ToArray()[i], Ys2.Take(Ys2.Length / 2).ToArray()[i]);
            }
            chartFreq.Series.Clear();
            chartFreq.Series.Add(series);
            chartFreq.Update();
            */

            Thread testingThread = new Thread(new ParameterizedThreadStart(RunTest));
            testingThread.IsBackground = true;
            //testingThread.Start(Ys2);
            testingThread.Start(FFTs);

            timer1.Enabled = true;
        }

        /// <summary>
        /// Feeds FFT data to the network for evaluation
        /// </summary>
        /// <param name="myFFT"></param>
        private void RunTest(object myFFT)
        {
            double[] FFT = (double[])myFFT;
            IntPtr ptr = Run(network, FFT, FFT.Length);
            int size = outputCount;
            double[] output = new double[size];
            Marshal.Copy(ptr, output, 0, size);

            /*
            foreach (double d in output)
            {
                Console.WriteLine(d);
            }
            Console.WriteLine();

            int j = 0;
            foreach (double[] d in l2n.Values)
            {
                bool isEqual = true;
                for (int i = 0; i < output.Length; i++)
                {
                    Console.Write(d[i]);
                    isEqual = true;
                    if (output[i] != d[i])
                    {
                        isEqual = false;
                    }
                }
                Console.WriteLine();
                if (isEqual)
                {
                    break;
                }
                j++;
            } 
            */

            //Console.WriteLine(n2l[j]);

            //rtbConsole.Invoke(new Action(() => rtbConsole.AppendText(n2l[j] + "")));
            for (int i = 0; i < output.Length; i++)
            {
                rtbConsole.Invoke(new Action(() => rtbConsole.AppendText(output[i] + ", ")));
            }
            rtbConsole.Invoke(new Action(() => rtbConsole.AppendText("\n")));
        }

        /// <summary>
        /// Fast Fourier Transformation Algorithm
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Starts a thread for training the network
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Trains the network based on the specified data
        /// </summary>
        private void StartTraining()
        {
            while (cbxTrain.Checked)
            {
                pbrEpochs.Invoke(new Action(() => pbrEpochs.Value = 0));

                if (String.IsNullOrEmpty(dataPath))
                {
                    rtbConsole.Invoke(new Action(() => rtbConsole.AppendText("No training data selected\n")));
                    rtbConsole.Invoke(new Action(() => rtbConsole.ScrollToCaret()));
                    return;
                }

                List<double[]> FFTsList = new List<double[]>();
                List<string[]> linesList = new List<string[]>();
                for (int k = 0; k < data.Length; k++)
                {
                    string text = File.ReadAllText(labels[k]);
                    linesList.Add(text.Split(new[] { Environment.NewLine }, StringSplitOptions.None));

                    string samples = File.ReadAllText(data[k]);
                    string[] sample = samples.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                    double[] d;
                    for (int i = 0; i < sample.Length / 160; i++)
                    {
                        d = new double[256];
                        for (int j = 0; j < 160; j++)
                        {
                            d[j] = Double.Parse(sample[i * 160 + j]) * 100000;
                        }

                        /*
                        double[] Ys2 = FFT(d);
                        Series series2 = new Series();
                        series2.ChartType = SeriesChartType.FastLine;
                        for (int j = 0; j < d.Length; j++)
                        {
                            series2.Points.AddXY(j, Ys2[j]);
                        }
                        chartFreq.Invoke(new Action(() => chartFreq.Series.Clear()));
                        chartFreq.Invoke(new Action(() => chartFreq.Series.Add(series2)));
                        chartFreq.Invoke(new Action(() => chartFreq.Update()));
                        */

                        FFTsList.Add(FFT(d));
                        //Thread.Sleep(100);
                    }
                }

                /*
                foreach (double[] dd in FFTsList)
                {
                    foreach (double d in dd)
                    {
                        Console.Write(d + ", ");
                    }
                    Console.WriteLine();
                }
                */
                //FFT inputs
                double[][] FFTs = new double[FFTsList.Count][];
                FFTsList.CopyTo(FFTs);
                foreach (double[] dd in FFTs)
                {
                    foreach (double d in dd)
                    {
                        Console.Write(d + ", ");
                    }
                    Console.WriteLine();
                }

                double[] flatFFTs = new double[FFTs.Length * FFTs[0].Length];
                for (int i = 0; i < FFTs.Length; i++)
                {
                    for (int j = 0; j < FFTs[0].Length; j++)
                    {
                        flatFFTs[(i * FFTs[0].Length) + j] = FFTs[i][j];
                    }
                }

                string[][] lines = new string[linesList.Count][];
                linesList.CopyTo(lines);
                foreach (string[] ss in lines)
                {
                    //Console.WriteLine(ss.Length);
                }

                //Expected letters
                double[][] letters = LetterToNumber(lines);

                foreach (double[] sa in letters)
                {
                    foreach (double s in sa)
                    {
                        //Console.WriteLine(s);
                    }
                    //Console.WriteLine(sa.Length);
                }

                double[] flatLets = new double[letters.Length * letters[0].Length];
                for (int i = 0; i < letters.Length; i++)
                {
                    for (int j = 0; j < letters[0].Length; j++)
                    {
                        flatLets[(i * letters[0].Length) + j] = letters[i][j];
                    }
                }

                network = CreateNetwork();
                IntPtr inputs = CreateMatrixVector(flatFFTs, FFTs.Length, FFTs[0].Length);
                IntPtr expected = CreateMatrixVector(flatLets, letters.Length, letters[0].Length);

                Init(network, FFTs[0].Length, hiddenCount, hiddenCount2, outputCount, weightsPath);

                Series series = new Series();
                series.ChartType = SeriesChartType.FastLine;
                chartError.Invoke(new Action(() => chartError.ChartAreas[0].AxisX.Maximum = epochs));
                chartError.Invoke(new Action(() => chartError.Series.Clear()));
                chartError.Invoke(new Action(() => chartError.Series.Add(series)));
                pbrEpochs.Invoke(new Action(() => pbrEpochs.Maximum = epochs));
                rtbConsole.Invoke(new Action(() => rtbConsole.AppendText("\nTraining Started...\n")));
                rtbConsole.Invoke(new Action(() => rtbConsole.ScrollToCaret()));
                double t = 0;
                int e;
                for (e = 0; e < epochs; e++)
                {
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();

                    double d = Train(network, inputs, expected, learningRate, momentum);
                    //learningRate *= 1.1;

                    if (e == 0)
                    {
                        //chartError.Invoke(new Action(() => chartError.ChartAreas[0].AxisY.Maximum = (((int)Math.Ceiling(d / 100.0)) * 100)));
                    }

                    chartError.Invoke(new Action(() => series.Points.AddXY(e+1, d)));
                    chartError.Invoke(new Action(() => chartError.Update()));

                    stopwatch.Stop();
                    t += Convert.ToDouble(stopwatch.Elapsed.TotalSeconds);
                    PrintEpoch(e, d, Convert.ToDouble(stopwatch.Elapsed.TotalSeconds)); //only on debug

                    pbrEpochs.Invoke(new Action(() => pbrEpochs.PerformStep()));


                    if (d < 0.01)
                    {
                        pbrEpochs.Invoke(new Action(() => pbrEpochs.Value = pbrEpochs.Maximum));
                        break;
                    }
                    else if (cbxAbort.Checked)
                    {
                        rtbConsole.Invoke(new Action(() => rtbConsole.AppendText("Training aborted!\t\t\tAverage epoch time: " + (t / e + 1) + "\n")));
                        rtbConsole.Invoke(new Action(() => rtbConsole.ScrollToCaret()));
                        cbxTrain.Invoke(new Action(() => cbxTrain.CheckState = CheckState.Unchecked));
                        cbxAbort.Invoke(new Action(() => cbxAbort.CheckState = CheckState.Unchecked));
                        Export(network, weightsPath);
                        return;
                    }
                    else if (Double.IsNaN(d)) //not working?
                    {
                        rtbConsole.Invoke(new Action(() => rtbConsole.AppendText("Training failed!\t\t\tAverage epoch time: " + (t / e + 1) + "\n")));
                        rtbConsole.Invoke(new Action(() => rtbConsole.ScrollToCaret()));
                        break;
                    }
                    else if (e == epochs-1)
                    {
                        rtbConsole.Invoke(new Action(() => rtbConsole.AppendText("Training complete!\t\t\tAverage epoch time: " + (t / e + 1) + "\n")));
                        rtbConsole.Invoke(new Action(() => rtbConsole.ScrollToCaret()));
                        Export(network, weightsPath);
                        Thread.Sleep(2000);
                    }
                }
            }
        }

        /*
        /// <summary>
        /// Trains the network based on the specified data
        /// </summary>
        private void StartTraining()
        {
            while (cbxTrain.Checked)
            {
                pbrEpochs.Invoke(new Action(() => pbrEpochs.Value = 0));

                if (String.IsNullOrEmpty(dataPath))
                {
                    rtbConsole.Invoke(new Action(() => rtbConsole.AppendText("No training data selected\n")));
                    rtbConsole.Invoke(new Action(() => rtbConsole.ScrollToCaret()));
                    return;
                }

                //double[][] FFTs = null;
                List<double[]> FFTsList = new List<double[]>();
                string[][] lines = new string[data.Length][];
                int width = 0; 
                for (int k = 0; k < data.Length; k++)
                {
                    string text = File.ReadAllText(labels[k]);
                    lines[k] = text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

                    Console.WriteLine(lines[k].Length);

                    using (WaveFileReader reader = new WaveFileReader(data[k]))
                    {
                        //int pow = (int)Math.Pow(2, Math.Ceiling(Math.Log(reader.Length) / Math.Log(2)));
                        //int pow = 65536; //pow too small sometimes? biggest file...
                        //width = pow / SIZE_BUFFER;
                        width = 64;
                        /*
                        if (FFTs == null)
                        {
                            FFTs = new double[data.Length * (pow / 1024)][];
                            //FFTs = new double[(data.Length * (pow / SIZE_BUFFER))][]; //number of separate files * number of 10 ms chunks
                        }
                        
                        
                        for (int j = 0; j < 64; j++)
                        {
                            byte[] bytesBuffer = new byte[1024];
                            int r = reader.Read(bytesBuffer, 0, bytesBuffer.Length);
                            double[] values = new double[1024];

                            if (r < 1024)
                            {
                                Console.WriteLine(j);
                                break;
                            }

                            //Console.WriteLine(r);
                            //Console.WriteLine(bytesBuffer.Length);

                            for (int i = 0; i < 1024; i += 2)
                            {
                                values[i] = (double)BitConverter.ToInt16(bytesBuffer, i);
                                //Console.Write(BitConverter.ToInt16(bytesBuffer, i) + ", ");
                            }
                            FFTsList.Add(FFT(values));
                            //Console.WriteLine(r);
                        }

                        /*
                        for (int j = 0; j < (pow / SIZE_BUFFER); j++)
                        {
                            byte[] bytesBuffer = new byte[SIZE_BUFFER]; //1024?
                            int r = reader.Read(bytesBuffer, 0, bytesBuffer.Length);

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
                                if (r == 0)
                                {
                                    Ys[i] = 1;
                                }
                                Xs2[i] = (double)i / Ys.Length * RATE / 1000.0;
                            }

                            //wave
                            Series series2 = new Series();
                            series2.ChartType = SeriesChartType.FastLine;
                            for (int i = 0; i < Xs.Length; i++)
                            {
                                series2.Points.AddXY(Xs[i], Ys[i]);
                            }
                            chartAudio.Invoke(new Action(() => chartAudio.Series.Clear()));
                            chartAudio.Invoke(new Action(() => chartAudio.Series.Add(series2)));
                            chartAudio.Invoke(new Action(() => chartAudio.Update()));

                            //FFT
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

                            FFTs[(k * (pow / SIZE_BUFFER)) + j] = Ys2;
                        }
                        
                    }
                }

                //double[][] FFTs = new double[data.Length][];
                //string[][] lines = new string[data.Length][];
                List<double[]> FFTsList = new List<double[]>();
                List<string[]> linesList = new List<string[]>();
                for (int k = 0; k < data.Length; k++)
                {
                    string text = File.ReadAllText(labels[k]);
                    linesList.Add(text.Split(new[] { Environment.NewLine }, StringSplitOptions.None));

                    string samples = File.ReadAllText(data[k]);
                    string[] sample = samples.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                    double[] d;
                    for (int i = 0; i < sample.Length / 160; i++)
                    {
                        d = new double[256];
                        for (int j = 0; j < 160; j++)
                        {
                            d[j] = Double.Parse(sample[i * 160 + j]) * 1000;
                        }

                        /*
                        double[] Ys2 = FFT(d);
                        Series series2 = new Series();
                        series2.ChartType = SeriesChartType.FastLine;
                        for (int j = 0; j < d.Length; j++)
                        {
                            series2.Points.AddXY(j, Ys2[j]);
                        }
                        chartFreq.Invoke(new Action(() => chartFreq.Series.Clear()));
                        chartFreq.Invoke(new Action(() => chartFreq.Series.Add(series2)));
                        chartFreq.Invoke(new Action(() => chartFreq.Update()));

                        FFTsList.Add(FFT(d));
                        //Thread.Sleep(100);
                    }
                }

                /*
                foreach (double[] dd in FFTsList)
                {
                    foreach (double d in dd)
                    {
                        Console.Write(d + ", ");
                    }
                    Console.WriteLine();
                }

                //FFT inputs
                double[][] FFTs = new double[FFTsList.Count][];
                FFTsList.CopyTo(FFTs);
                foreach (double[] dd in FFTs)
                {
                    foreach (double d in dd)
                    {
                        //Console.Write(d + ", ");
                    }
                    //Console.WriteLine();
                }
                //Console.WriteLine("\n\n" + FFTs.Length);
                double[][] FFTs = new double[FFTsList.Count][];
                FFTsList.CopyTo(FFTs);
                foreach (double[] dd in FFTs)
                {
                    foreach (double d in dd)
                    {
                        //Console.Write(d + ", ");
                    }
                    //Console.WriteLine();
                }

                double[] flatFFTs = new double[FFTs.Length * FFTs[0].Length];
                for (int i = 0; i < FFTs.Length; i++)
                {
                    for (int j = 0; j < FFTs[0].Length; j++)
                    {
                        flatFFTs[(i * FFTs[0].Length) + j] = FFTs[i][j];
                    }
                }

                string[][] lines = new string[linesList.Count][];
                linesList.CopyTo(lines);
                foreach (string[] ss in lines)
                {
                    //Console.WriteLine(ss.Length);
                }

                //Expected letters
                double[][] letters = LetterToNumber(lines);

                //Console.WriteLine(letters.Length);

                foreach (double[] sa in letters)
                {
                    foreach (double s in sa)
                    {
                        //Console.WriteLine(s);
                    }
                    //Console.WriteLine(sa.Length);
                }
                
                double[] flatLets = new double[letters.Length * letters[0].Length];
                for (int i = 0; i < letters.Length; i++)
                {
                    for (int j = 0; j < letters[0].Length; j++)
                    {
                        flatLets[(i * letters[0].Length) + j] = letters[i][j];
                    }
                }

                network = CreateNetwork();
                //IntPtr expected = CreateMatrixList(flatLets, letters.Length, letters[0].Length);
                //IntPtr inputs = CreateMatrixList(flatFFTs, FFTs.Length, FFTs[0].Length);
                //Console.WriteLine(FFTs.Length + ":" + FFTs[0].Length);
                //Console.WriteLine(letters.Length + ":" + letters[0].Length);
                IntPtr inputs = CreateMatrixVector(flatFFTs, FFTs.Length, FFTs[0].Length);
                IntPtr expected = CreateMatrixVector(flatLets, letters.Length, letters[0].Length);

                Init(network, FFTs[0].Length, hiddenCount, hiddenCount2, outputCount, weightsPath);

                Series series = new Series();
                series.ChartType = SeriesChartType.FastLine;
                chartError.Invoke(new Action(() => chartError.ChartAreas[0].AxisX.Maximum = epochs));
                chartError.Invoke(new Action(() => chartError.Series.Clear()));
                chartError.Invoke(new Action(() => chartError.Series.Add(series)));
                pbrEpochs.Invoke(new Action(() => pbrEpochs.Maximum = epochs));
                rtbConsole.Invoke(new Action(() => rtbConsole.AppendText("\nTraining Started...\n")));
                rtbConsole.Invoke(new Action(() => rtbConsole.ScrollToCaret()));
                double t = 0;
                int e;
                for (e = 0; e < epochs; e++)
                {
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();

                    double d = Train(network, inputs, expected, learningRate, momentum);
                    learningRate *= 1.1;

                    if (e == 0)
                    {
                        //chartError.Invoke(new Action(() => chartError.ChartAreas[0].AxisY.Maximum = (((int)Math.Ceiling(d / 100.0)) * 100)));
                    }

                    //Console.WriteLine(d);
                    chartError.Invoke(new Action(() => series.Points.AddXY(e, d)));
                    chartError.Invoke(new Action(() => chartError.Update()));

                    stopwatch.Stop();
                    t += Convert.ToDouble(stopwatch.Elapsed.TotalSeconds);
                    PrintEpoch(e, d, Convert.ToDouble(stopwatch.Elapsed.TotalSeconds)); //only on debug
                    
                    pbrEpochs.Invoke(new Action(() => pbrEpochs.PerformStep()));
                    
                    
                    if (d < 0.01)
                    {
                        pbrEpochs.Invoke(new Action(() => pbrEpochs.Value = pbrEpochs.Maximum));
                        break;
                    }
                    else if (cbxAbort.Checked)
                    {
                        rtbConsole.Invoke(new Action(() => rtbConsole.AppendText("Training aborted!\t\t\tAverage epoch time: " + (t / e + 1) + "\n")));
                        rtbConsole.Invoke(new Action(() => rtbConsole.ScrollToCaret()));
                        cbxTrain.Invoke(new Action(() => cbxTrain.CheckState = CheckState.Unchecked));
                        cbxAbort.Invoke(new Action(() => cbxAbort.CheckState = CheckState.Unchecked));
                        Export(network, weightsPath);
                        return;
                    }
                    else if (Double.IsNaN(d)) //not working?
                    {
                        rtbConsole.Invoke(new Action(() => rtbConsole.AppendText("Training failed!\t\t\tAverage epoch time: " + (t / e + 1) + "\n")));
                        rtbConsole.Invoke(new Action(() => rtbConsole.ScrollToCaret()));
                        File.WriteAllText(weightsPath, String.Empty);
                        continue;
                    }
                }
                rtbConsole.Invoke(new Action(() => rtbConsole.AppendText("Training complete!\t\t\tAverage epoch time: " + (t / e+1) + "\n")));
                rtbConsole.Invoke(new Action(() => rtbConsole.ScrollToCaret()));

                Export(network, weightsPath);

                Thread.Sleep(2000);
            }
        }
        */

        /// <summary>
        /// Changes the data labels to network recognizable values
        /// </summary>
        /// <param name="lines"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        private double[][] LetterToNumber(string[][] lines)
        {
            int length = 0;
            foreach (string[] s in lines)
            {
                length += s.Length;
            }

            double[][] results = new double[length][];  

            int i = 0, j = 0;
            for (int k = 0; k < length; k++)
            {
                results[k] = l2n[lines[i][j].ToCharArray()[0]];

                j++;
                if (j >= lines[i].Length)
                {
                    j = 0;
                    i++;
                }
            }

            return results;
        }
        /*
        /// <summary>
        /// Changes the data labels to network recognizable values
        /// </summary>
        /// <param name="lines"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        private double[][] LetterToNumber(string[][] lines, int width)
        {
            double[][] results = new double[lines.Length * width][];

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (j >= lines[i].Length)
                    {
                        results[(i * width) + j] = l2n[' '];
                    }
                    else
                    {
                        results[(i * width) + j] = l2n[lines[i][j].ToCharArray()[0]];
                    }
                }
            }

            //
            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    results[(i * lines[i].Length) + j] = l2n[lines[i][j].ToCharArray()[0]];
                }
            }

            return results;
        }
        */

        /// <summary>
        /// Prints epoch results while debuging
        /// </summary>
        /// <param name="i"></param>
        /// <param name="d"></param>
        /// <param name="t"></param>
        [ConditionalAttribute("DEBUG")]
        private void PrintEpoch(int i, double d, double t)
        {
            rtbConsole.Invoke(new Action(() => rtbConsole.AppendText("Epoch: " + (i + 1) + "\tError: " + d + "\tTime: " + t + "\n")));
            rtbConsole.Invoke(new Action(() => rtbConsole.ScrollToCaret()));
        }

        /// <summary>
        /// Runs the test data through the network for validation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTest_Click(object sender, EventArgs e)
        {
            network = CreateNetwork();
            int inputCount = GetInputSize(network, weightsPath);
            Init(network, inputCount, hiddenCount, hiddenCount2, outputCount, weightsPath);

            string[][] lines = new string[data.Length][];
            for (int k = 0; k < data.Length; k++)
            {
                string samples = File.ReadAllText(data[k]);
                string[] sample = samples.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                double[] d;
                for (int i = 0; i < sample.Length / 160; i++)
                {
                    d = new double[256];
                    for (int j = 0; j < 160; j++)
                    {
                        d[j] = Double.Parse(sample[i * 160 + j]) * 100000;
                    }
                    RunTest(FFT(d));
                }
            }

            rtbConsole.Invoke(new Action(() => rtbConsole.ScrollToCaret()));
        }

        /*
        /// <summary>
        /// Runs the test data through the network for validation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTest_Click(object sender, EventArgs e)
        {
            network = CreateNetwork();
            int inputCount = GetInputSize(network, weightsPath);
            Init(network, inputCount, hiddenCount, hiddenCount2, outputCount, weightsPath);

            /*
            for (int k = 0; k < data.Length; k++)
            {
                using (WaveFileReader reader = new WaveFileReader(data[k]))
                {
                    /*
                    //int pow = (int)Math.Pow(2, Math.Ceiling(Math.Log(reader.Length) / Math.Log(2)));
                    int pow = 65536; //pow too small sometimes? biggest file...

                    for (int j = 0; j < (pow / SIZE_BUFFER); j++)
                    {
                        byte[] bytesBuffer = new byte[SIZE_BUFFER];
                        reader.Read(bytesBuffer, 0, bytesBuffer.Length);

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
                        //wave
                        Series series2 = new Series();
                        series2.ChartType = SeriesChartType.FastLine;
                        for (int i = 0; i < Xs.Length; i++)
                        {
                            series2.Points.AddXY(Xs[i], Ys[i]);
                        }
                        chartAudio.Invoke(new Action(() => chartAudio.Series.Clear()));
                        chartAudio.Invoke(new Action(() => chartAudio.Series.Add(series2)));
                        chartAudio.Invoke(new Action(() => chartAudio.Update()));

                        //FFT
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

                        Ys2 = FFT(Ys);

                        /*
                        Thread testingThread = new Thread(new ParameterizedThreadStart(RunTest));
                        testingThread.IsBackground = true;
                        testingThread.Start(Ys2);
                        RunTest(Ys2);
                    }
                    
                        for (int j = 0; j < 64; j++)
                    {
                        byte[] bytesBuffer = new byte[1024];
                        int r = reader.Read(bytesBuffer, 0, bytesBuffer.Length);
                        double[] values = new double[1024];

                        //Console.WriteLine(r);
                        //Console.WriteLine(bytesBuffer.Length);

                        for (int i = 0; i < 1024; i += 2)
                        {
                            if (i < r)
                            {
                                values[i] = (double)BitConverter.ToInt16(bytesBuffer, i);
                            }
                            else
                            {
                                values[i] = 0.0;
                            }
                            //Console.Write(BitConverter.ToInt16(bytesBuffer, i) + ", ");
                        }
                        RunTest(FFT(values));
                        // FFTs[(k * 64) + j] = FFT(values);
                        //Console.WriteLine(r);
                    }
                }
            }

            //double[][] FFTs = new double[data.Length][];
            string[][] lines = new string[data.Length][];
            for (int k = 0; k < data.Length; k++)
            {
                string samples = File.ReadAllText(data[k]);
                string[] sample = samples.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                double[] d;
                for (int i = 0; i < sample.Length / 160; i++)
                {
                    d = new double[256];
                    for (int j = 0; j < 160; j++)
                    {
                        d[j] = Double.Parse(sample[i * 160 + j]) * 1000;
                    }
                    RunTest(FFT(d));
                }
                //FFTs[k] = d;

                //RunTest(d);
            }


            rtbConsole.Invoke(new Action(() => rtbConsole.ScrollToCaret()));
        }

        */

        /// <summary>
        /// Begins feeds the network realtime audio input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxRun_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxRun.Checked)
            {
                network = CreateNetwork();
                int inputCount = GetInputSize(network, weightsPath);
                Init(network, inputCount, hiddenCount, hiddenCount2, outputCount, weightsPath);

                input.StartRecording();
                timer1.Start();
            }
            else
            {
                input.StopRecording();
                timer1.Stop();
            }
        }

        //***revisit to check for manual folder path input or disable manual***
        #region TOP TOOLSTRIP

        private void sbxData_TextChanged(object sender, EventArgs e)
        {
            dataPath = sbxData.Text;
        }

        private void sbnData_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK && !String.IsNullOrWhiteSpace(folderBrowserDialog1.SelectedPath))
            {
                sbxData.Text = folderBrowserDialog1.SelectedPath;
                Properties.Settings.Default.DataPath = sbxData.Text;
                Properties.Settings.Default.Save();
                LoadData(sbxData.Text);
            }
        }

        private void LoadData(string dataPath)
        {
            if (!String.IsNullOrEmpty(dataPath))
            {
                //data = Directory.GetFiles(dataPath, "*.wav", SearchOption.AllDirectories);
                data = Directory.GetFiles(dataPath, "*.data", SearchOption.AllDirectories);
                /*
                foreach (string d in data)
                {
                    Console.WriteLine(d);
                }
                */
                labels = Directory.GetFiles(dataPath, "*.txt", SearchOption.AllDirectories);
                /*
                foreach (string l in labels)
                {
                    Console.WriteLine(l);
                }
                */
            }
        }

        private void sbxWeights_TextChanged(object sender, EventArgs e)
        {
            weightsPath = sbxWeights.Text;
        }

        private void sbnWeights_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            if (openFileDialog1.ShowDialog() == DialogResult.OK && !String.IsNullOrWhiteSpace(openFileDialog1.FileName))
            {
                sbxWeights.Text = openFileDialog1.SafeFileName;
                weightsPath = sbxWeights.Text;
                Properties.Settings.Default.WeightsPath = sbxWeights.Text;
                Properties.Settings.Default.Save();
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

        #endregion

        #region BOTTOM TOOLSTRIP

        private void tsbHiddenCount_TextChanged(object sender, EventArgs e)
        {
            if (Int32.TryParse(sbxHiddenCount.Text, out int hiddenCount))
            {
                this.hiddenCount = hiddenCount;
            }
            else
            {
                sbxHiddenCount.Text = "";
            }
        }

        private void tsbHiddenCount2_TextChanged(object sender, EventArgs e)
        {
            if (Int32.TryParse(sbxHiddenCount2.Text, out int hiddenCount2))
            {
                this.hiddenCount2 = hiddenCount2;
            }
            else
            {
                sbxHiddenCount2.Text = "";
            }
        }

        private void tsbEpochs_TextChanged(object sender, EventArgs e)
        {
            if (Int32.TryParse(sbxEpochs.Text, out int epochs))
            {
                this.epochs = epochs;
            }
            else
            {
                sbxEpochs.Text = "";
            }
        }

        private void tsbLearningRate_TextChanged(object sender, EventArgs e)
        {
            if (Double.TryParse(sbxLearningRate.Text, out double learningRate))
            {
                this.learningRate = learningRate;
            }
            else
            {
                sbxLearningRate.Text = "";
            }
        }

        private void tsbMomentum_TextChanged(object sender, EventArgs e)
        {
            if (Double.TryParse(sbxMomentum.Text, out double momentum))
            {
                this.momentum = momentum;
            }
            else
            {
                sbxMomentum.Text = "";
            }
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.WeightsPath = weightsPath;
            Properties.Settings.Default.HiddenCount = hiddenCount;
            Properties.Settings.Default.HiddenCount2 = hiddenCount2;
            Properties.Settings.Default.Epochs = epochs;
            Properties.Settings.Default.LearningRate = learningRate;
            Properties.Settings.Default.Momentum = momentum;
            Properties.Settings.Default.Save();
        }

        #endregion
    }
}
