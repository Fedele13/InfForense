using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace GoogleMeetLogsNavigator
{
    public partial class Form1 : Form
    { 
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var frm = new Form_login();
            this.Hide();
            frm.Location = this.Location;
            frm.StartPosition = FormStartPosition.Manual;
            frm.Closed += (s, args) => this.Close();
            frm.Show();
            

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog FBD = new FolderBrowserDialog();

            if ( FBD.ShowDialog() == DialogResult.OK)
            {
                var listBox1 = new ListBox();
                listBox1.Items.Clear();
                string[] files = Directory.GetFiles(FBD.SelectedPath);
                string[] dirs = Directory.GetDirectories(FBD.SelectedPath);

                foreach (string file in files)
                {
                    listBox1.Items.Add(file);
                }
                foreach (string dir in dirs)
                {
                    listBox1.Items.Add(dir);
                }
            }
        }

        private void aBOUTToolStripMenuItem_Click(object sender, EventArgs e)
        {

            var frm = new About_form();
            frm.Show();
        }
    }
}
