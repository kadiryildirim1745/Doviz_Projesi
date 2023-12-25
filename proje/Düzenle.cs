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
namespace proje
{
    public partial class Düzenle : Form
    {
        public Düzenle()
        {
            InitializeComponent();
        }
        public string tc = "";

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Profil p = new Profil();
            p.tc = tc;
            p.Visible = true;

            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Profil p = new Profil();
            p.tc = tc;
            p.Visible = true;

            this.Close();
        }

        private void Düzenle_Load(object sender, EventArgs e)
        {
            maskedTextBox1.Text = tc;
            Baglanti bg = new Baglanti();
            SqlCommand cmd2 = new SqlCommand("select * from Tbl_Kullanici where userTc=@p1", bg.connection());
            cmd2.Parameters.AddWithValue("@p1", tc);
            SqlDataReader rd= cmd2.ExecuteReader();
            while (rd.Read())
            {
                textBox1.Text = rd[1].ToString();
                textBox2.Text = rd[2].ToString();
                maskedTextBox1.Text = rd[3].ToString();
                maskedTextBox2.Text = rd[4].ToString();
                textBox3.Text = rd[5].ToString();
                maskedTextBox3.Text = rd[6].ToString();
            }
            bg.connection().Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Baglanti bg =new Baglanti();
            SqlCommand cmd = new SqlCommand("update Tbl_Kullanici set userAd=@p1, userSoyad=@p2, userTelefon=@p4, userSifre=@p5, userDogum=@p6 where userTc=@p3", bg.connection());
            cmd.Parameters.AddWithValue("@p1",textBox1.Text);
            cmd.Parameters.AddWithValue("@p2",textBox2.Text);
            cmd.Parameters.AddWithValue("@p3",maskedTextBox1.Text);
            cmd.Parameters.AddWithValue("@p4",maskedTextBox2.Text);
            cmd.Parameters.AddWithValue("@p5",textBox3.Text);
            cmd.Parameters.AddWithValue("@p6",maskedTextBox3.Text);
            cmd.ExecuteNonQuery();
            bg.connection().Close();
            MessageBox.Show("Kişi Güncellendi");
            Profil p = new Profil();
            p.tc = tc;
            p.Visible = true;

            this.Close();
        }

    }
}
