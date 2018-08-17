using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpeechRecognitionNN
{
    public partial class MainForm : Form
    {
        const int SIZE_X = 4000;
        const int SIZE_Y = 10000;
        const int tick = 10;

        bool isRunning;

        WaveIn input;
        Queue<double> queue;

        public MainForm()
        {
            InitializeComponent();

            isRunning = false;

            chartAudio.ChartAreas[0].AxisY.Minimum = -SIZE_Y;
            chartAudio.ChartAreas[0].AxisY.Maximum = SIZE_Y;

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
