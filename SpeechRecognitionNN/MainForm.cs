using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

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

        [DllImport(@"C:\Users\Dylan\source\repos\XOR-NN\x64\Debug\XORNN.dll")]
        public static extern void Train(IntPtr network, );

        [DllImport(@"C:\Users\Dylan\source\repos\XOR-NN\x64\Debug\XORNN.dll")]
        public static extern int HelloWorld();

        const int SIZE_X = 4000;
        const int SIZE_Y = 40000;
        const int tick = 10;

        const int epochs = 500;

        bool isRunning;

        WaveIn input;
        Queue<double> queue;

        public MainForm()
        {
            InitializeComponent();

            isRunning = false;

            chartAudio.ChartAreas[0].AxisY.Minimum = -SIZE_Y;
            chartAudio.ChartAreas[0].AxisY.Maximum = SIZE_Y;

            pbrEpochs.Maximum = epochs;

            input = new WaveIn();
            input.WaveFormat = new WaveFormat(44100, 1);
            input.BufferMilliseconds = 100;
            input.DataAvailable += new EventHandler<WaveInEventArgs>(DataAvailable);

            queue = new Queue<double>(Enumerable.Repeat(0.0, SIZE_X).ToList());
            chartAudio.Series["Input"].Points.DataBindY(queue);

            timer1.Interval = tick;
        }

        private void DataAvailable(object sender, WaveInEventArgs e)
        {
            for (int i = 0; i < e.BytesRecorded; i += 2)
            {
                queue.Enqueue(BitConverter.ToInt16(e.Buffer, i));
                queue.Dequeue();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            chartAudio.Series["Input"].Points.DataBindY(queue);
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
