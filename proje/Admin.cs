using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Data.SqlClient;
using System.Reflection.Emit;

namespace proje
{
    public partial class Admin : Form
    {
        Baglanti bg=new Baglanti();
        public Admin()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        string url;
        string url2;
        public string tc = "";
        private void Admin_Load(object sender, EventArgs e)
        {
            label6.Text = tc.ToString();
            url = "https://www.tcmb.gov.tr/kurlar/today.xml";
            yazdir(url);

            SqlCommand cmd = new SqlCommand("select adminAd, adminSoyad from Tbl_Admin where admintc=@p1",bg.connection());
            cmd.Parameters.AddWithValue("@p1",tc);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                label4.Text = dr[0].ToString();
                label5.Text = dr[1].ToString();
            }
            bg.connection().Close();
            Timer timer = new Timer();
            timer.Interval = 1000; // 1000 milisaniye = 1 saniye
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            // Anlık saat ve tarih bilgisini alarak etiketi güncelle
            label7.Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateTime secilenTarih = dateTimePicker1.Value;

            // Seçilen tarihi istediğiniz formatta gösterme
            string formatliTarih = secilenTarih.ToString("ddMMyyyy");
            string tarihFormatli = secilenTarih.ToString("yyyyMM");
            url2 = $"https://www.tcmb.gov.tr/kurlar/{tarihFormatli}/{formatliTarih}.xml";
        }

        DateTime tarih;
        private void yazdir(string url)
        {
            string url1 = url;
            var obj = new XmlDocument();
            obj.Load(url1);
            tarih = Convert.ToDateTime(obj.SelectSingleNode("//Tarih_Date").Attributes["Tarih"].Value);
            string bultenNo = obj.SelectSingleNode("/Tarih_Date/@Bulten_No").Value;
            string dolarsatis = obj.SelectSingleNode("Tarih_Date/Currency[@Kod='USD']/ForexSelling").InnerXml;
            string dolaralis = obj.SelectSingleNode("Tarih_Date/Currency[@Kod='USD']/ForexBuying").InnerXml;
            string eurosatis = obj.SelectSingleNode("Tarih_Date/Currency[@Kod='EUR']/ForexSelling").InnerXml;
            string euroalis = obj.SelectSingleNode("Tarih_Date/Currency[@Kod='EUR']/ForexBuying").InnerXml;
            string sterlinsatis = obj.SelectSingleNode("Tarih_Date/Currency[@Kod='GBP']/ForexSelling").InnerXml;
            string sterlinalis = obj.SelectSingleNode("Tarih_Date/Currency[@Kod='GBP']/ForexBuying").InnerXml;
            string kuveytsatis = obj.SelectSingleNode("Tarih_Date/Currency[@Kod='KWD']/ForexSelling").InnerXml;
            string kuveytalis = obj.SelectSingleNode("Tarih_Date/Currency[@Kod='KWD']/ForexBuying").InnerXml;
            label18.Text = tarih.ToString();
            label19.Text = bultenNo.ToString();
            label20.Text = dolarsatis.ToString();
            label29.Text = dolaralis.ToString();
            label30.Text = euroalis.ToString();
            label31.Text = eurosatis.ToString();
            label32.Text = sterlinalis.ToString();
            label33.Text = sterlinsatis.ToString();
            label34.Text = kuveytalis.ToString();
            label35.Text = kuveytsatis.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string dosyayolu = "";
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                dosyayolu = folderBrowserDialog1.SelectedPath.ToString();
                label9.Text = dosyayolu;
            }
            StreamWriter sw;
            string dosyaadi = label18.Text;
            DateTime bugün = DateTime.Now;
            string ytarih=bugün.ToString("ddMMyyyy");
            string etarih = bugün.ToString("dd.MM.yyyy");
            sw = File.CreateText(dosyayolu + "\\" + ytarih + ".txt");
            sw.WriteLine("    AD    " + "   SOYAD  " + "   DÖVİZ  " + " OPERASYON" + "  MİKTAR  " + "  TOPLAM  " + "   TARİH  ");
            SqlCommand cmd4 = new SqlCommand("SELECT userAd, userSoyad, dovizAd, operasyonTur, operasyonMiktar,toplam,Tbl_Operation.tarih FROM Tbl_Operation " +
                "inner join Tbl_Kullanici on Tbl_Kullanici.userID=Tbl_Operation.userID " +
                "inner join Tbl_Doviz on Tbl_Doviz.dovizID=Tbl_Operation.dovizID  where LEFT(Tbl_Operation.tarih, 10)=@s1", bg.connection());
            cmd4.Parameters.AddWithValue("@s1", etarih);    
            SqlDataReader dr4 = cmd4.ExecuteReader();
            while (dr4.Read())
            {            
                sw.WriteLine("  "+dr4[0].ToString() + " / " +"  "+ dr4[1].ToString() + " / "+ "  "+ dr4[2].ToString() + " / " +" "+ dr4[3].ToString() + " /"+" "+ dr4[4].ToString() + "/"+" " + dr4[5].ToString() + "/" + dr4[6].ToString());
                
                
                
            }
            MessageBox.Show("Dosya oluşturuldu ve yazma işlemi tamamlandı.");
            sw.Close();


        }

        private void button5_Click(object sender, EventArgs e)
        {
            yazdir(url2);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            yazdir(url);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cozum c = new Cozum();
            c.Show();
            this.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Istatistik i=new Istatistik();
            i.Show();
            this.Visible = false;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Giris g = new Giris();
            g.Show();
            this.Close();
        }
    }
}
