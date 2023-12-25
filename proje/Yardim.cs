using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Reflection.Emit;

namespace proje
{
    public partial class Yardim : Form
    {
        public int ID;
        public string tc = "";
        public Yardim()
        {
            InitializeComponent();
        }
        Baglanti bg=new Baglanti();
        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd= new SqlCommand("insert into Tbl_Sikayet (userID,sikayet,tarih,sikayetTur) values (@p1,@p2,@p3,@p4)",bg.connection());
            cmd.Parameters.AddWithValue("@p1",ID);
            cmd.Parameters.AddWithValue("@p2", richTextBox1.Text);
            cmd.Parameters.AddWithValue("@p3", label3.Text);
            cmd.Parameters.AddWithValue("@p4", comboBox1.Text);
            cmd.ExecuteNonQuery();
            bg.connection().Close();
            MessageBox.Show("Şikayetiniz Kaydedildi");
            Profil p = new Profil();
            p.tc = tc;
            p.Visible = true;

            this.Close();
        }

        private void Yardim_Load(object sender, EventArgs e)
        {
            Timer timer = new Timer();
            timer.Interval = 1000; // 1000 milisaniye = 1 saniye
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            // Anlık saat ve tarih bilgisini alarak etiketi güncelle
            label3.Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Profil p = new Profil();
            p.tc = tc;
            p.Visible = true;

            this.Close();
        }
    }
}
