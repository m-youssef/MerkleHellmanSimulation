using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MerkleHellmanSimulation
{
    public partial class Form1 : Form
    {
        public delegate void UpdateTextBoxDelegate(string textBoxString, Color color); // delegate type 
        public UpdateTextBoxDelegate UpdateTextBox; // delegate object
        public MyMerkleHellman MyMerkleHellmanClass;

        private void UpdateTextBoxMethod(string str, Color color = default(Color))
        {
            richTextBox1.SelectionStart = richTextBox1.TextLength;
            richTextBox1.SelectionLength = 0;
            richTextBox1.SelectionColor = color;
            richTextBox1.AppendText(str + Environment.NewLine);
            richTextBox1.SelectionColor = richTextBox1.ForeColor;

        } // this method is invoked

        public Form1()
        {
            InitializeComponent();
            UpdateTextBox = UpdateTextBoxMethod; // initialize delegate object
            MyMerkleHellmanClass = new MyMerkleHellman(this);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var s = textBox1.Text.TrimEnd(',').Split(',').ToList().Select(z => Convert.ToInt32(z)).ToList();
                var w = Convert.ToInt32(textBox2.Text);
                var n = Convert.ToInt32(textBox3.Text);

                var h = MyMerkleHellmanClass.CalculateHardknapsacks(s, w, n);

                textBox5.Text = string.Join(",", h);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            try
            {
                textBox4.Text = textBox1.Text.TrimEnd(',').Split(',').ToList().Select(z => Convert.ToInt32(z)).ToList().Count.ToString();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateTextBoxMethod("**************************************", Color.Red);
                UpdateTextBoxMethod("******** Start Encryption ************", Color.Red);
                UpdateTextBoxMethod("**************************************", Color.Red);

                List<int> h;
                if (string.IsNullOrEmpty(textBox5.Text))
                {
                    var s = textBox1.Text.TrimEnd(',').Split(',').ToList().Select(z => Convert.ToInt32(z)).ToList();
                    var w = Convert.ToInt32(textBox2.Text);
                    var n = Convert.ToInt32(textBox3.Text);

                    h = MyMerkleHellmanClass.CalculateHardknapsacks(s, w, n);

                    textBox5.Text = string.Join(",", h);
                }
                else
                {
                    h = textBox5.Text.TrimEnd(',').Split(',').ToList().Select(z => Convert.ToInt32(z)).ToList();
                }
                var enc = MyMerkleHellmanClass.EncryptMessage(h, textBox6.Text.Replace(" ", ""));
                textBox7.Text = string.Join(",", enc);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateTextBoxMethod("**************************************", Color.Red);
                UpdateTextBoxMethod("******** Start Decryption ************", Color.Red);
                UpdateTextBoxMethod("**************************************", Color.Red);

                var msg = textBox7.Text.TrimEnd(',').Split(',').ToList().Select(z => Convert.ToInt32(z)).ToList();
                var w = Convert.ToInt32(textBox2.Text);
                var n = Convert.ToInt32(textBox3.Text);
                int wi;
                if (string.IsNullOrEmpty(textBox8.Text))
                {
                    wi = MyMerkleHellmanClass.CalculateWinvers(w, n);
                }
                else
                {
                    wi = Convert.ToInt32(textBox8.Text);
                }
                textBox8.Text = wi.ToString();
                var s = textBox1.Text.TrimEnd(',').Split(',').ToList().Select(z => Convert.ToInt32(z)).ToList();

                var messge = MyMerkleHellmanClass.DecryptMessage(msg, wi, n, s);
                var str = "";
                foreach (var list in messge)
                {
                    str += string.Join("", list);

                }
                textBox10.Text = str;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                var w = Convert.ToInt32(textBox2.Text);
                var n = Convert.ToInt32(textBox3.Text);
                var wi = MyMerkleHellmanClass.CalculateWinvers(w, n);
                textBox8.Text = wi.ToString();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox10.Clear();
            richTextBox1.Clear();
        }
    }
}
