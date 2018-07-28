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
        bool isRunning;

        public MainForm()
        {
            InitializeComponent();

            isRunning = false;

            
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            if (isRunning = !isRunning)
            {

                btnRun.BackColor = Color.Red;
            }
            else
            {

                btnRun.BackColor = Color.DarkGray;
            }
        }
    }
}
