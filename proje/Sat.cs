using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Net.Mail;

namespace proje
{
    public partial class Sat : Form
    {
        public Sat()
        {
            InitializeComponent();
        }
        public string tc = "";
        public int ID;
        Baglanti bg=new Baglanti();
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlCommand cmd9 = new SqlCommand("select dolar, euro, sterlin, kuveyt, tl from Tbl_Cuzdan where userID=@z1 ", bg.connection());
            cmd9.Parameters.AddWithValue("@z1", ID);
            SqlDataReader rd2 = cmd9.ExecuteReader();
            while (rd2.Read())
            {
                label16.Text = rd2[4].ToString();
                if (comboBox1.Text == "Dolar")
                {
                    label10.Text = "$";
                    label14.Text = "$";
                    label11.Text = rd2[0].ToString();
                }
                else if (comboBox1.Text == "Euro")
                {
                    label10.Text = "€";
                    label14.Text = "€";
                    label11.Text = rd2[1].ToString();
                }
                else if (comboBox1.Text == "Sterlin")
                {
                    label10.Text = "£";
                    label14.Text = "£";
                    label11.Text = rd2[2].ToString();
                }
                else if (comboBox1.Text == "Kuveyt Dinarı")
                {
                    label10.Text = "KWD";
                    label14.Text = "KWD";
                    label11.Text = rd2[3].ToString();
                }
            }
            
            
            bg.connection().Close();
            SqlCommand cmd = new SqlCommand("select dovizID,dovizAl from Tbl_Doviz where dovizAd=@p1", bg.connection());
            cmd.Parameters.AddWithValue("@p1", comboBox1.Text);
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                label7.Text = rd[1].ToString();
                label13.Text = rd[0].ToString();
            }
            bg.connection().Close();
            
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Captcha captcha = new Captcha();
            captcha.Show();
            radioButton1.Visible = false;
            button1.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Convert.ToDouble(textBox1.Text) > Convert.ToDouble(label11.Text))
            {
                MessageBox.Show("Yeterli Bakiyeniz Bulunmamakta.");
            }
            else
            {
                SqlCommand cmd3 = new SqlCommand("insert into Tbl_Operation (userID, dovizID, operasyonTur, operasyonMiktar, toplam, tarih) values (@k1,@k2,@k3,@k4,@k5,@k6)", bg.connection());
                cmd3.Parameters.AddWithValue("@k1", ID);
                cmd3.Parameters.AddWithValue("@k2", Convert.ToInt32(label13.Text));
                cmd3.Parameters.AddWithValue("@k3", "Satış");
                cmd3.Parameters.AddWithValue("@k4", Convert.ToDouble(textBox1.Text));
                cmd3.Parameters.AddWithValue("@k5", toplam);
                cmd3.Parameters.AddWithValue("@k6", label15.Text);
                mail(ID, Convert.ToInt32(label13.Text), "Satış", Convert.ToDouble(textBox1.Text), toplam, label15.Text);
                cmd3.ExecuteNonQuery();
                MessageBox.Show("Satım Onaylandı");
                bg.connection().Close();
                double bakiyetl = Convert.ToDouble(label16.Text) + toplam;
                double bakiyedolar = Convert.ToDouble(label11.Text) - Convert.ToDouble(textBox1.Text);
                double bakiyeeuro = Convert.ToDouble(label11.Text) - Convert.ToDouble(textBox1.Text);
                double bakiyesterlin = Convert.ToDouble(label11.Text) - Convert.ToDouble(textBox1.Text);
                double bakiyekuveyt = Convert.ToDouble(label11.Text) - Convert.ToDouble(textBox1.Text);
                if (label10.Text == "$")
                {
                    SqlCommand cmd5 = new SqlCommand("update Tbl_Cuzdan set tl=@t1, dolar=@t2 where userID=@t3 ", bg.connection());
                    cmd5.Parameters.AddWithValue("@t1", bakiyetl);
                    cmd5.Parameters.AddWithValue("@t2", bakiyedolar);
                    cmd5.Parameters.AddWithValue("@t3", ID);
                    cmd5.ExecuteNonQuery();
                    bg.connection().Close();
                }
                else if (label10.Text == "€")
                {
                    SqlCommand cmd5 = new SqlCommand("update Tbl_Cuzdan set tl=@t1, euro=@t2 where userID=@t3 ", bg.connection());
                    cmd5.Parameters.AddWithValue("@t1", bakiyetl);
                    cmd5.Parameters.AddWithValue("@t2", bakiyeeuro);
                    cmd5.Parameters.AddWithValue("@t3", ID);
                    cmd5.ExecuteNonQuery();
                    bg.connection().Close();
                }
                else if (label10.Text == "€")
                {
                    SqlCommand cmd5 = new SqlCommand("update Tbl_Cuzdan set tl=@t1, sterlin=@t2 where userID=@t3 ", bg.connection());
                    cmd5.Parameters.AddWithValue("@t1", bakiyetl);
                    cmd5.Parameters.AddWithValue("@t2", bakiyesterlin);
                    cmd5.Parameters.AddWithValue("@t3", ID);
                    cmd5.ExecuteNonQuery();
                    bg.connection().Close();
                }
                else
                {
                    SqlCommand cmd5 = new SqlCommand("update Tbl_Cuzdan set tl=@t1, kuveyt=@t2 where userID=@t3 ", bg.connection());
                    cmd5.Parameters.AddWithValue("@t1", bakiyetl);
                    cmd5.Parameters.AddWithValue("@t2", bakiyekuveyt);
                    cmd5.Parameters.AddWithValue("@t3", ID);
                    cmd5.ExecuteNonQuery();
                    bg.connection().Close();
                }
                Profil p = new Profil();
                p.tc = tc;
                p.Visible = true;

                this.Close();
            }

        }
        MailMessage mesaj = new MailMessage();
        string alici = "";
        private void mail(int Id, int doviz, string tur, double miktar, double toplam, string tarih)
        {
            try
            {
                SqlCommand cmd3 = new SqlCommand("select userMail from Tbl_Kullanici where userTc=@s1", bg.connection());
                cmd3.Parameters.AddWithValue("@s1", tc);
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
                mesaj.From = new MailAddress("doviz.gonder@hotmail.com");
                mesaj.Subject = "Dekont";
                string mesajim = "  Kullanıcı Id:  " + ID.ToString() + "  DovizId:  " + doviz.ToString() + "  Doviz Türü:  " + tur + "  Miktarı:  " + miktar.ToString() + "  Toplam:  " + toplam.ToString() + "  Tarih:  " + tarih;
                mesaj.Body = mesajim;
                smtp1.Send(mesaj);

                MessageBox.Show("E-posta adresinize dekont gönderildi.", "Başarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"E-posta gönderme hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Sat_Load(object sender, EventArgs e)
        {
            label9.Text = tc;
            SqlCommand cmd2 = new SqlCommand("select tl, dolar, euro, sterlin, kuveyt from Tbl_Cuzdan where userTc=@l1", bg.connection());
            
            Timer timer = new Timer();
            timer.Interval = 1000; // 1000 milisaniye = 1 saniye
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            // Anlık saat ve tarih bilgisini alarak etiketi güncelle
            label15.Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
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
        double toplam;
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                toplam = Convert.ToDouble(textBox1.Text) * Convert.ToDouble(label7.Text);
                label4.Text = toplam.ToString();
            }
            catch(Exception ex) 
            {
                MessageBox.Show($"Değer Giriniz... {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }
    }
}
