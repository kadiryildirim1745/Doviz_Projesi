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
using System.Security.Cryptography;

namespace proje
{
    public partial class Kayit : Form
    {
        public Kayit()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Giris form1 = new Giris();
            form1.Show();
            this.Hide();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            string hash= HashPassword(maskedTextBox3.Text);
            Baglanti bg= new Baglanti();
            SqlCommand cmd = new SqlCommand("insert into Tbl_Kullanici (userAd,userSoyad,userTc,userDogum," +
                "userTelefon,userSifre,userMail) values (@p1,@p2,@p3,@p4,@p6,@p7,@p8)", bg.connection());
            cmd.Parameters.AddWithValue("@p1",maskedTextBox2.Text);
            cmd.Parameters.AddWithValue("@p2",maskedTextBox6.Text);
            cmd.Parameters.AddWithValue("@p3",maskedTextBox1.Text);
            cmd.Parameters.AddWithValue("@p4",maskedTextBox5.Text);
            cmd.Parameters.AddWithValue("@p6",maskedTextBox4.Text);
            cmd.Parameters.AddWithValue("@p7",hash);
            cmd.Parameters.AddWithValue("@p8", maskedTextBox7.Text);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Kullanıcı Başarılı Bir Şekilde Kaydedildi.");
            bg.connection().Close();
        }

        private static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // Şifreyi byte dizisine çevir
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Byte dizisini hexadecimal string'e çevir
                StringBuilder builder = new StringBuilder();
                foreach (byte b in hashedBytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                Console.WriteLine(builder);
                return builder.ToString();
            }
        }
    }
}
