﻿using System;
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
        private string comboDelimitatorItem;

        private IList<string> supportedDelimitators = new List<string>();

        public string Delimitator
        {
            get { return comboDelimitatorItem; }
        }
        public Form1(IList<string> supportedDelimitators)
        {
            InitializeComponent();
            this.supportedDelimitators = supportedDelimitators;
            comboBox1.Items.AddRange(supportedDelimitators.ToArray());
            comboBox1.SelectedItem = supportedDelimitators.FirstOrDefault();

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            comboDelimitatorItem = (string)comboBox1.SelectedItem;
            if (string.IsNullOrEmpty(comboDelimitatorItem))
            {
                comboDelimitatorItem = comboBox1.Text;
            }
            if (string.IsNullOrEmpty(comboDelimitatorItem) == false)
            {
                this.Close();
                return;
            }

            MessageBox.Show("Inserisci o seleziona un delimitatore", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
