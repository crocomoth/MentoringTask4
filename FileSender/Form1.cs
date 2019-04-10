using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileSender
{
    public partial class Form1 : Form
    {
        private Service.FileSender fileSender;
        public Form1()
        {
            InitializeComponent();
            fileSender = new Service.FileSender();
        }

        private void OpenFileButton_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var path = openFileDialog1.FileName;
                fileSender.SendFile(path).GetAwaiter();
            }
        }

        private void CloseConnectionButton_Click(object sender, EventArgs e)
        {
            fileSender.CloseConnection().GetAwaiter();
        }
    }
}
