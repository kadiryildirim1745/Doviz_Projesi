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
    public partial class Bakiye : Form
    {
        public string tc = "";
        public int ID;
        public Bakiye()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        Profil p = new Profil();

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Captcha captcha = new Captcha();
            captcha.Show();
            radioButton1.Visible = false;
            button1.Visible = true;
            
        }
        Baglanti bg = new Baglanti();
        private void Bakiye_Load(object sender, EventArgs e)
        {
            label9.Text = tc;
            label10.Text=ID.ToString();
              
            SqlCommand cmd= new SqlCommand("select userID from Tbl_Cuzdan where userTc=@p1",bg.connection());
            cmd.Parameters.AddWithValue("@p1",tc);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                bg.connection().Close();
                SqlCommand cmd3 = new SqlCommand("select tl from Tbl_Cuzdan where userTc=@k1", bg.connection());
                cmd3.Parameters.AddWithValue("@k1", tc);
                SqlDataReader dr2 = cmd3.ExecuteReader();
                while (dr2.Read())
                {
                    label4.Text = dr2[0].ToString();
                }
                bg.connection().Close();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            double toplam= Convert.ToDouble(label4.Text) + Convert.ToDouble(maskedTextBox1.Text);
            SqlCommand cmd4 = new SqlCommand("update Tbl_Cuzdan set tl=@m1 where userTc=@m2", bg.connection());
            cmd4.Parameters.AddWithValue("@m1",toplam);
            cmd4.Parameters.AddWithValue("@m2", tc);
            cmd4.ExecuteNonQuery();
            MessageBox.Show("Bakiye Yüklendi");
            p.tc = tc;
            p.Visible= true;
            
            this.Close();
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
