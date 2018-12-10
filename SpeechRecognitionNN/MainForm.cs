using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SpeechRecognitionForm
{
    public partial class MainForm : Form
    {
        /*
        [DllImport(@"C:\Users\Dylan\source\repos\ANN\x64\Debug\ANN.dll")]
        public static extern IntPtr CreateNetwork();

        [DllImport(@"C:\Users\Dylan\source\repos\ANN\x64\Debug\ANN.dll")]
        public static extern IntPtr CreateMatrixVector(double[] data, int rows, int columns);

        [DllImport(@"C:\Users\Dylan\source\repos\ANN\x64\Debug\ANN.dll")]
        public static extern void Init(IntPtr network, int inputCount, int hiddenCount, int hiddenCount2, int outputCount, [MarshalAs(UnmanagedType.LPStr)] string weightsPath);

        [DllImport(@"C:\Users\Dylan\source\repos\ANN\x64\Debug\ANN.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern double Train(IntPtr network, IntPtr inputs, IntPtr expected, double learningRate, double momentum);

        [DllImport(@"C:\Users\Dylan\source\repos\ANN\x64\Debug\ANN.dll")]
        public static extern void Export(IntPtr network, [MarshalAs(UnmanagedType.LPStr)] string weightsPath);

        [DllImport(@"C:\Users\Dylan\source\repos\ANN\x64\Debug\ANN.dll")]
        public static extern int GetInputSize(IntPtr network, [MarshalAs(UnmanagedType.LPStr)] string weightsPath);

        [DllImport(@"C:\Users\Dylan\source\repos\ANN\x64\Debug\ANN.dll")]
        public static extern IntPtr Run(IntPtr network, double[] data, int columns);
        */
        
        [DllImport("ANN.dll")]
        public static extern IntPtr CreateNetwork();

        [DllImport("ANN.dll")]
        public static extern IntPtr CreateMatrixVector(double[] data, int rows, int columns);

        [DllImport("ANN.dll")]
        public static extern void Init(IntPtr network, int inputCount, int hiddenCount, int hiddenCount2, int outputCount, [MarshalAs(UnmanagedType.LPStr)] string weightsPath);

        [DllImport("ANN.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern double Train(IntPtr network, IntPtr inputs, IntPtr expected, double learningRate, double momentum);

        [DllImport("ANN.dll")]
        public static extern void Export(IntPtr network, [MarshalAs(UnmanagedType.LPStr)] string weightsPath);

        [DllImport("ANN.dll")]
        public static extern int GetInputSize(IntPtr network, [MarshalAs(UnmanagedType.LPStr)] string weightsPath);

        [DllImport("ANN.dll")]
        public static extern IntPtr Run(IntPtr network, double[] data, int columns);

        private const int RATE = 16000;
        private const int SIZE_X = 320;
        private const int SIZE_Y = 5;
        private const int SIZE_X2 = 25;
        private const int SIZE_Y2 = 100;
        private const int TICK = 20;

        
        private Dictionary<char, double[]> l2n = new Dictionary<char, double[]>()
        {
            { 'c', new double[]{ 1, 0, 0, 0 } },
            { 'a', new double[]{ 0, 1, 0, 0 } },
            { ' ', new double[]{ 0, 0, 1, 0 } },
            { 't', new double[]{ 0, 0, 0, 1 } }
        };

        private Dictionary<string, char> n2l = new Dictionary<string, char>()
        {
            { "1000", 'c' },
            { "0100", 'a' },
            { "0010", '_' },
            { "0001", 't' }
        };
        
        
         /*
        private Dictionary<char, double[]> l2n = new Dictionary<char, double[]>()
        {
            { 'c', new double[]{ 0, 0} },
            { 'a', new double[]{ 0, 1} },
            { ' ', new double[]{ 1, 0} },
            { 't', new double[]{ 1, 1} }
        };

        private Dictionary<string, char> n2l = new Dictionary<string, char>()
        {
            { "00", 'c' },
            { "01", 'a' },
            { "10", '_' },
            { "11", 't' }
        };
        */

        /* Used for dataset of entire alphabet
        private Dictionary<string, double[]> l2n = new Dictionary<string, double[]>()
        {
            { "b",  new double[]{ 0, 0, 0, 0, 0, 0 } },
            { "d",  new double[]{ 0, 0, 0, 0, 0, 1 } },
            { "f",  new double[]{ 0, 0, 0, 0, 1, 0 } },
            { "g",  new double[]{ 0, 0, 0, 0, 1, 1 } },
            { "h",  new double[]{ 0, 0, 0, 1, 0, 0 } },
            { "dg", new double[]{ 0, 0, 0, 1, 0, 1 } },
            { "k",  new double[]{ 0, 0, 0, 1, 1, 0 } },
            { "l",  new double[]{ 0, 0, 0, 1, 1, 1 } },
            { "m",  new double[]{ 0, 0, 1, 0, 0, 0 } },
            { "n",  new double[]{ 0, 0, 1, 0, 0, 1 } },
            { "p",  new double[]{ 0, 0, 1, 0, 1, 0 } },
            { "r",  new double[]{ 0, 0, 1, 0, 1, 1 } },
            { "s",  new double[]{ 0, 0, 1, 1, 0, 0 } },
            { "t",  new double[]{ 0, 0, 1, 1, 0, 1 } },
            { "v",  new double[]{ 0, 0, 1, 1, 1, 0 } },
            { "w",  new double[]{ 0, 0, 1, 1, 1, 1 } },
            { "ch", new double[]{ 0, 1, 0, 0, 0, 0 } },
            { "sh", new double[]{ 0, 1, 0, 0, 0, 1 } },
            { "th", new double[]{ 0, 1, 0, 0, 1, 0 } },
            { "ng", new double[]{ 0, 1, 0, 0, 1, 1 } },
            { "j",  new double[]{ 0, 1, 0, 1, 0, 0 } },
            { "a",  new double[]{ 0, 1, 0, 1, 0, 1 } },
            { "ay", new double[]{ 0, 1, 0, 1, 1, 0 } },
            { "e",  new double[]{ 0, 1, 0, 1, 1, 1 } },
            { "i",  new double[]{ 0, 1, 1, 0, 0, 0 } },
            { "ii", new double[]{ 0, 1, 1, 0, 0, 1 } },
            { "ai", new double[]{ 0, 1, 1, 0, 1, 0 } },
            { "aw", new double[]{ 0, 1, 1, 0, 1, 1 } },
            { "o",  new double[]{ 0, 1, 1, 1, 0, 0 } },
            { "uo", new double[]{ 0, 1, 1, 1, 0, 1 } },
            { "ou", new double[]{ 0, 1, 1, 1, 1, 0 } },
            { "ew", new double[]{ 0, 1, 1, 1, 1, 1 } },
            { "oi", new double[]{ 1, 0, 0, 0, 0, 0 } },
            { "ow", new double[]{ 1, 0, 0, 0, 0, 1 } },
            { "au", new double[]{ 1, 0, 0, 0, 1, 0 } },
            { "er", new double[]{ 1, 0, 0, 0, 1, 1 } },
            { "or", new double[]{ 1, 0, 0, 1, 0, 0 } },
            { "ir", new double[]{ 1, 0, 0, 1, 0, 1 } },
            { "ur", new double[]{ 1, 0, 0, 1, 1, 0 } },
            { "_",  new double[]{ 1, 0, 0, 1, 1, 1 } },
            { "-",  new double[]{ 1, 0, 1, 0, 0, 0 } }
        };

        private Dictionary<string, string> n2l = new Dictionary<string, string>()
        {
            { "b",  "000000" },
            { "d",  "000001" },
            { "f",  "000010" },
            { "g",  "000011" },
            { "h",  "000100" },
            { "dg", "000101" },
            { "k",  "000110" },
            { "l",  "000111" },
            { "m",  "001000" },
            { "n",  "001001" },
            { "p",  "001010" },
            { "r",  "001011" },
            { "s",  "001100" },
            { "t",  "001101" },
            { "v",  "001110" },
            { "w",  "001111" },
            { "ch", "010000" },
            { "sh", "010001" },
            { "th", "010010" },
            { "ng", "010011" },
            { "j",  "010100" },
            { "a",  "010101" },
            { "ay", "010110" },
            { "e",  "010111" },
            { "i",  "011000" },
            { "ii", "011001" },
            { "ai", "011010" },
            { "aw", "011011" },
            { "o",  "011100" },
            { "uo", "011101" },
            { "ou", "011110" },
            { "ew", "011111" },
            { "oi", "100000" },
            { "ow", "100001" },
            { "au", "100010" },
            { "er", "100011" },
            { "or", "100100" },
            { "ir", "100101" },
            { "ur", "100110" },
            { "_",  "100111" },
            { "-",  "101000" }
        };
        */

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
            outputCount = l2n.Count;
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

            input = new WaveIn();
            input.WaveFormat = new WaveFormat(RATE, 1);
            input.BufferMilliseconds = 20;
            input.DataAvailable += new EventHandler<WaveInEventArgs>(DataAvailable);

            buffer = new BufferedWaveProvider(input.WaveFormat);
            buffer.BufferLength = 320 * 2;
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
            var frames = new byte[640];
            buffer.Read(frames, 0, 640);
            if (frames.Length == 0) return;
            if (frames[640 - 2] == 0) return;

            timer1.Enabled = false;

            double[] d = new double[320];
            for (int i = 0; i < 640; i+= 2)
            {
                double c = (short)((frames[i + 1] << 8) | frames[i]);
                c = (c / 32768.0);
                d[i / 2] = c * 10;
            }
            double[] FFTs = d;

            Series series = new Series();
            series.ChartType = SeriesChartType.FastLine;
            for (int i = 0; i < 320; i++)
            {
                series.Points.AddXY(i, FFTs[i]);
            }
            chartAudio.Series.Clear();
            chartAudio.Series.Add(series);
            chartAudio.Update();


            /* for displaying FFT
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
            */

            //Thread testingThread = new Thread(new ParameterizedThreadStart(RunTest));
            //testingThread.IsBackground = true;
            //testingThread.Start(Ys2);
            //testingThread.Start(FFTs);

            RunTest(FFTs);

            timer1.Enabled = true;
        }

        /// <summary>
        /// Feeds FFT data to the network for evaluation
        /// </summary>
        /// <param name="myFFT"></param>
        private char RunTest(object myFFT, char prevOutput = '/')
        {
            double[] FFT = (double[])myFFT;
            IntPtr ptr = Run(network, FFT, FFT.Length);
            int size = outputCount;
            double[] output = new double[size];
            Marshal.Copy(ptr, output, 0, size);

            
            string key = "";
            foreach (double d in output)
            {
                key += d;
            }

            if (n2l.ContainsKey(key))
            {
                if (!n2l[key].Equals(prevOutput) && !n2l[key].Equals('_'))
                {
                    rtbConsole.Invoke(new Action(() => rtbConsole.AppendText(n2l[key] + "")));
                    rtbConsole.Invoke(new Action(() => rtbConsole.ScrollToCaret()));
                    return n2l[key];
                }

                return n2l[key];
            }
            
            /* raw outputs
            for (int i = 0; i < output.Length; i++)
            {
                rtbConsole.Invoke(new Action(() => rtbConsole.AppendText(output[i] + " ")));
            }
            */
            
            return prevOutput;
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
                    for (int i = 0; i < sample.Length / 320; i++)
                    {
                        d = new double[320];
                        for (int j = 0; j < 320; j++)
                        {
                            d[j] = Double.Parse(sample[i * 320 + j])*10;
                        }

                        /* for displaying FFT
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

                        FFTsList.Add(d);
                    }
                }
                //FFT inputs
                double[][] FFTs = new double[FFTsList.Count][];
                FFTsList.CopyTo(FFTs);

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

                //Expected letters
                double[][] letters = LetterToNumber(lines);

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
                double min = 3.5;
                double t = 0;
                int e;
                Stopwatch stopwatch = new Stopwatch();
                for (e = 1; e <= epochs; e++)
                {
                    stopwatch.Start();

                    double d = Train(network, inputs, expected, learningRate, momentum);
                    //experimental increasing of hyperparameters
                    //learningRate *= .95;
                    //momentum *= 1.0001;

                    

                    if (d < 0.001)
                    {
                        pbrEpochs.Invoke(new Action(() => pbrEpochs.Value = pbrEpochs.Maximum));
                        rtbConsole.Invoke(new Action(() => rtbConsole.AppendText("Training complete!\t\t\tAverage epoch time: " + (t / e) + "\n")));
                        rtbConsole.Invoke(new Action(() => rtbConsole.ScrollToCaret()));
                        Export(network, weightsPath);
                        Thread.Sleep(2000);
                        break;
                    }
                    else if (d < min)
                    {
                        rtbConsole.Invoke(new Action(() => rtbConsole.AppendText("Training saved!\n")));
                        rtbConsole.Invoke(new Action(() => rtbConsole.ScrollToCaret()));
                        Export(network, weightsPath);
                        min = d;
                    }
                    else if (e % 10 == 0)
                    {
                        chartError.Invoke(new Action(() => series.Points.AddXY(e, d)));
                        chartError.Invoke(new Action(() => chartError.Update()));

                        stopwatch.Stop();
                        t += Convert.ToDouble(stopwatch.Elapsed.TotalSeconds);
                        PrintEpoch(e, d, Convert.ToDouble(stopwatch.Elapsed.TotalSeconds));
                        stopwatch = new Stopwatch();

                        pbrEpochs.Invoke(new Action(() => pbrEpochs.Value += 10));

                        //rtbConsole.Invoke(new Action(() => rtbConsole.AppendText("Training saved!\n")));
                        rtbConsole.Invoke(new Action(() => rtbConsole.ScrollToCaret()));
                        Export(network, weightsPath);
                    }
                    else if (cbxAbort.Checked)
                    {
                        rtbConsole.Invoke(new Action(() => rtbConsole.AppendText("Training aborted!\t\t\tAverage epoch time: " + (t / e) + "\n")));
                        rtbConsole.Invoke(new Action(() => rtbConsole.ScrollToCaret()));
                        cbxTrain.Invoke(new Action(() => cbxTrain.CheckState = CheckState.Unchecked));
                        cbxAbort.Invoke(new Action(() => cbxAbort.CheckState = CheckState.Unchecked));
                        Export(network, weightsPath);
                        return;
                    }
                    else if (Double.IsNaN(d)) //not working?
                    {
                        rtbConsole.Invoke(new Action(() => rtbConsole.AppendText("Training failed!\t\t\tAverage epoch time: " + (t / e) + "\n")));
                        rtbConsole.Invoke(new Action(() => rtbConsole.ScrollToCaret()));
                        break;
                    }
                    if (e == epochs)
                    {
                        rtbConsole.Invoke(new Action(() => rtbConsole.AppendText("Training complete!\t\t\tAverage epoch time: " + (t / e) + "\n")));
                        rtbConsole.Invoke(new Action(() => rtbConsole.ScrollToCaret()));
                        Thread.Sleep(2000);
                    }
                }
            }
        }

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

        /// <summary>
        /// Prints epoch results while debuging
        /// </summary>
        /// <param name="i"></param>
        /// <param name="d"></param>
        /// <param name="t"></param>
        //[ConditionalAttribute("DEBUG")]
        private void PrintEpoch(int i, double d, double t)
        {
            rtbConsole.Invoke(new Action(() => rtbConsole.AppendText("Epoch: " + i + "\tError: " + d + "\tTime: " + t + "\n")));
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
                char prevOutput = '/';
                for (int i = 0; i < sample.Length / 320; i++)
                {
                    d = new double[320];
                    for (int j = 0; j < 320; j++)
                    {
                        d[j] = Double.Parse(sample[i * 320 + j]) * 10;
                    }
                    prevOutput = RunTest(d, prevOutput);
                }
                rtbConsole.Invoke(new Action(() => rtbConsole.AppendText("\n")));
            }

            rtbConsole.Invoke(new Action(() => rtbConsole.ScrollToCaret()));
        }

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
            try
            {
                if (!String.IsNullOrEmpty(dataPath))
                {
                    data = Directory.GetFiles(dataPath, "*.data", SearchOption.AllDirectories);
                    labels = Directory.GetFiles(dataPath, "*.txt", SearchOption.AllDirectories);
                }
            }
            catch
            {
                sbxData.Text = "";
                Properties.Settings.Default.DataPath = sbxData.Text;
                Properties.Settings.Default.Save();
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