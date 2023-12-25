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
using System.Net.Mail;
using System.Net;

namespace proje
{
    public partial class Giris : Form
    {
        public Giris()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Kayit frm = new Kayit();
            frm.Show();
            this.Hide();
        }
        Baglanti bg = new Baglanti();
        private void button1_Click(object sender, EventArgs e)
        {
            string hash=HashPassword(maskedTextBox1.Text);
            SqlCommand cmd = new SqlCommand("select userTc, userSifre from Tbl_Kullanici where userTc=@p1 and userSifre=@p2",bg.connection());
            cmd.Parameters.AddWithValue("@p1", maskedTextBox2.Text);
            cmd.Parameters.AddWithValue("@p2", hash);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                mail();
                Onay o = new Onay();
                o.tc = maskedTextBox2.Text;
                o.kod = randomSayiString;
                o.Show();
                bg.connection().Close();
                /*Profil p = new Profil();
                p.tc = maskedTextBox2.Text;
                p.Show();*/
                this.Hide();
            }
            else
            {
                SqlCommand cmd2 = new SqlCommand("select adminTc, adminSifre from Tbl_Admin where adminTc=@p1 and adminSifre=@p2", bg.connection());
                cmd2.Parameters.AddWithValue("@p1", maskedTextBox2.Text);
                cmd2.Parameters.AddWithValue("@p2", hash);
                SqlDataReader reader2 = cmd2.ExecuteReader();
                if (reader2.Read())
                {
                    bg.connection().Close();
                    Admin a= new Admin();
                    a.tc = maskedTextBox2.Text;
                    a.Show();
                    this.Hide();
                }
                else
                {
                    bg.connection().Close();
                    MessageBox.Show("Giriş Hatalı.");
                }
                bg.connection().Close();

            }
            
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
                return builder.ToString();
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        MailMessage mesaj = new MailMessage();
        string alici = "";
        string randomSayiString="";
        private void mail()
        {
            try
            {
                Random random = new Random();

                int randomSayi = random.Next(100000, 1000000);

                randomSayiString = randomSayi.ToString();

                SqlCommand cmd3 = new SqlCommand("select userMail from Tbl_Kullanici where userTc=@s1",bg.connection());
                cmd3.Parameters.AddWithValue("@s1", maskedTextBox2.Text);
                SqlDataReader rd3 = cmd3.ExecuteReader();
                if (rd3.Read())
                {
                    alici = rd3[0].ToString();
                }
                SmtpClient smtp1 = new SmtpClient();
                smtp1.Credentials = new System.Net.NetworkCredential("doviz.gonder@hotmail.com", "doviz.sgonder123");
                smtp1.Port = 587;
                smtp1.EnableSsl = true;
                smtp1.Host = "smtp-mail.outlook.com";
                mesaj.To.Add(alici);
                mesaj.From=new MailAddress("doviz.gonder@hotmail.com");
                mesaj.Subject = "Giriş Kontrol";
                mesaj.Body = randomSayiString;
                smtp1.Send(mesaj);

                MessageBox.Show("E-posta adresinize onay kodu gönderildi.", "Başarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"E-posta gönderme hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Giris_Load(object sender, EventArgs e)
        {
            
        }
    }
}
