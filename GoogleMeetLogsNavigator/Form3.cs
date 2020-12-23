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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            text_search.Visible = false;
            button_search.Visible = false;
            dataGridView1.Visible = false;
            hScrollBar1.Visible = false;
            vScrollBar2.Visible = false;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void button_logout_Click_1(object sender, EventArgs e)
        {
            var frm = new Form1();
            this.Hide();
            frm.Location = this.Location;
            frm.StartPosition = FormStartPosition.Manual;
            frm.Closed += (s, args) => this.Close();
            frm.Show();
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog FBD = new FolderBrowserDialog();

            if (FBD.ShowDialog() == DialogResult.OK)
            {

                listBox1.Items.Clear();
                string[] files = Directory.GetFiles(FBD.SelectedPath);
                string[] dirs = Directory.GetDirectories(FBD.SelectedPath);

                foreach (string file in files)
                {
                    listBox1.Items.Add(Path.GetFileName(file));
                }
                foreach (string dir in dirs)
                { 
                    listBox1.Items.Add(Path.GetDirectoryName(dir));
                }
            }
        }

        private void fILEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.Visible = true;
            hScrollBar1.Visible = true;
            vScrollBar2.Visible = true;
            text_search.Visible = true;
            button_search.Visible = true;
        }

        private void hScrollBar2_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void aBOUTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new About_form();
            frm.Show();
        }

        private void button_search_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dOWNLOADToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new Download_Form();
            frm.Show();
        }
    }
}
