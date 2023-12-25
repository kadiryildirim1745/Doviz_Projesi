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
    public partial class Istatistik : Form
    {
        public Istatistik()
        {
            InitializeComponent();
        }
        Baglanti bg = new Baglanti();
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Admin a= new Admin();
            a.Show();
            this.Hide();
        }

        private void Istatistik_Load(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select count(*) from Tbl_Kullanici", bg.connection());
            SqlDataReader rd=cmd.ExecuteReader();
            while (rd.Read())
            {
                label8.Text = rd[0].ToString();
            }
            bg.connection().Close();

            SqlCommand cmd2 = new SqlCommand("select count(*) from Tbl_Sikayet", bg.connection());
            SqlDataReader rd2 = cmd2.ExecuteReader();
            while (rd2.Read())
            {
                label13.Text = rd2[0].ToString();
            }
            bg.connection().Close();

            SqlCommand cmd3 = new SqlCommand("select count(*) from Tbl_Operation", bg.connection());
            SqlDataReader rd3 = cmd3.ExecuteReader();
            while (rd3.Read())
            {
                label9.Text = rd3[0].ToString();
            }
            bg.connection().Close();

            SqlCommand cmd4 = new SqlCommand("SELECT TOP 1 userAd, userSoyad,userDogum FROM Tbl_Kullanici ORDER BY CONVERT(DATE, userDogum, 104) DESC;", bg.connection());
            SqlDataReader rd4 = cmd4.ExecuteReader();
            while (rd4.Read())
            {
                label10.Text = rd4[0].ToString() +" "+ rd4[1].ToString() +" "+ rd4[2].ToString();
            }
            bg.connection().Close();

            SqlCommand cmd5 = new SqlCommand("SELECT TOP 1 userAd, userSoyad, dolar FROM Tbl_Cuzdan inner join Tbl_Kullanici on Tbl_Kullanici.userID=Tbl_Cuzdan.userID ORDER BY dolar DESC;", bg.connection());
            SqlDataReader rd5 = cmd5.ExecuteReader();
            while (rd5.Read())
            {
                label11.Text = rd5[0].ToString() +" "+ rd5[1].ToString() +" "+ rd5[2].ToString()+" $"; ;
            }

            SqlCommand cmd6 = new SqlCommand("SELECT TOP 1 userAd, userSoyad, euro FROM Tbl_Cuzdan inner join Tbl_Kullanici on Tbl_Kullanici.userID=Tbl_Cuzdan.userID ORDER BY euro DESC;", bg.connection());
            SqlDataReader rd6 = cmd6.ExecuteReader();
            while (rd6.Read())
            {
                label12.Text = rd6[0].ToString() + " " + rd6[1].ToString() + " " + rd6[2].ToString() + " €"; ;
            }

            SqlCommand cmd7 = new SqlCommand("SELECT TOP 1 userAd, userSoyad, tl FROM Tbl_Cuzdan inner join Tbl_Kullanici on Tbl_Kullanici.userID=Tbl_Cuzdan.userID ORDER BY tl DESC;", bg.connection());
            SqlDataReader rd7 = cmd7.ExecuteReader();
            while (rd7.Read())
            {
                label14.Text = rd7[0].ToString() + " " + rd7[1].ToString() + " " + rd7[2].ToString() + " ₺"; ;
            }
            bg.connection().Close();
        }
    }
}
