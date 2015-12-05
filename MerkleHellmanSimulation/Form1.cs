using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MerkleHellmanSimulation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var s = textBox1.Text.TrimEnd(',').Split(',').ToList().Select(z=>Convert.ToInt32(z)).ToList();
            var w = Convert.ToInt32(textBox2.Text);
            var n = Convert.ToInt32(textBox3.Text);

            var h = MyMerkleHellman.CalculateHardknapsacks(s, w, n);

            textBox5.Text = string.Join(",", h);
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            textBox4.Text= textBox1.Text.TrimEnd(',').Split(',').ToList().Select(z => Convert.ToInt32(z)).ToList().Count.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var h = textBox5.Text.TrimEnd(',').Split(',').ToList().Select(z => Convert.ToInt32(z)).ToList();
            var enc = MyMerkleHellman.EncruptMessage(h, textBox6.Text);
            textBox7.Text = string.Join(",", enc);
        }
    }
}
