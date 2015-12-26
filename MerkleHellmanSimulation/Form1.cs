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
        public delegate void UpdateTextBoxDelegate(String textBoxString); // delegate type 
        public UpdateTextBoxDelegate UpdateTextBox; // delegate object
        public MyMerkleHellman MyMerkleHellmanClass;
        void UpdateTextBox1(string str) { textBox9.AppendText(str + Environment.NewLine); } // this method is invoked

        public Form1()
        {
            InitializeComponent();
            UpdateTextBox = new UpdateTextBoxDelegate(UpdateTextBox1); // initialize delegate object
            MyMerkleHellmanClass = new MyMerkleHellman(this);

        }

        private void button1_Click(object sender, EventArgs e)
        {

            var ee = textBox1.Text[0];
            var s = textBox1.Text.TrimEnd(',').Split(',').ToList().Select(z => Convert.ToInt32(z)).ToList();
            var w = Convert.ToInt32(textBox2.Text);
            var n = Convert.ToInt32(textBox3.Text);

            var h = MyMerkleHellmanClass.CalculateHardknapsacks(s, w, n);

            textBox5.Text = string.Join(",", h);
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            textBox4.Text = textBox1.Text.TrimEnd(',').Split(',').ToList().Select(z => Convert.ToInt32(z)).ToList().Count.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var h = textBox5.Text.TrimEnd(',').Split(',').ToList().Select(z => Convert.ToInt32(z)).ToList();
            var enc = MyMerkleHellmanClass.EncryptMessage(h, textBox6.Text.Replace(" ", ""));
            textBox7.Text = string.Join(",", enc);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var msg = textBox7.Text.TrimEnd(',').Split(',').ToList().Select(z => Convert.ToInt32(z)).ToList();
            var w = Convert.ToInt32(textBox2.Text);
            var n = Convert.ToInt32(textBox3.Text);
            var wi = MyMerkleHellmanClass.CalculateWinvers(w, n);
            var s = textBox1.Text.TrimEnd(',').Split(',').ToList().Select(z => Convert.ToInt32(z)).ToList();

            var messge = MyMerkleHellmanClass.DecryptMessage(msg, wi, n, s);
            string str = "";
            foreach (var list in messge)
            {
                str += string.Join("", list);

            }
            textBox6.Text = str;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var w = Convert.ToInt32(textBox2.Text);
            var n = Convert.ToInt32(textBox3.Text);
            var wi = MyMerkleHellmanClass.CalculateWinvers(w, n);
            textBox8.Text = wi.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox9.Clear();
        }
    }
}
