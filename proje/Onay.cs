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
    public partial class Onay : Form
    {
        public Onay()
        {
            InitializeComponent();
        }
        public string tc = "";
        public string kod = "";
        Giris g = new Giris();
        private void Onay_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(kod==textBox1.Text)
            {
                Profil p = new Profil();
                p.tc = tc;
                g.Visible=false;
                p.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Yanlış Girdiniz.");                
            }
        }
    }
}
