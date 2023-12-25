using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace proje
{
    public partial class Captcha : Form
    {
        public Captcha()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            yazdir();
        }
        private void yazdir()
        {
            richTextBox1.BackColor = Color.White;
            string[] dizi1 = { "q", "w", "e", "r", "t", "y", "k", "i", "o", "p", "u", "a", "s", "d", };
            string[] dizi2 = { "+", "-", "*", "/", "#", "€", "$", "&" };
            int s1, s2, s3;
            Random rand = new Random();
            s1 = rand.Next(0, dizi1.Length);
            s2 = rand.Next(0, dizi2.Length);
            s3 = rand.Next(1, 10);
            label1.Text = dizi1[s1] + dizi2[s2] + s3.ToString();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Bakiye b= new Bakiye();
            string x = richTextBox1.Text;
            if (x == label1.Text)
            {
                richTextBox1.BackColor = Color.Green;
                MessageBox.Show("Onaylandı");
                this.Hide();
            }
            else
            {
                richTextBox1.BackColor = Color.Red;
                MessageBox.Show("Eşleşmedi");

            }
        }

        private void Captcha_Load(object sender, EventArgs e)
        {
            yazdir();
        }
    }
}
