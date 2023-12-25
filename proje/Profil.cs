using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Xml;
using System.Security.Policy;
using System.Data.SqlClient;
namespace proje
{
    public partial class Profil : Form
    {
        public Profil()
        {
            InitializeComponent();
        }
        public string tc = "";
        int toplam = 0;
        string url;
        Baglanti bg = new Baglanti();
        private void Profil_Load(object sender, EventArgs e)
        {
            label6.Text = tc.ToString();
            url = "https://www.tcmb.gov.tr/kurlar/today.xml";
            yazdir(url);
            //bakiye hesaplama

            toplam = Convert.ToInt32(label9.Text) + (Convert.ToInt32(label8.Text)) * 1900 + (Convert.ToInt32(label38.Text) * 30);
            label17.Text = toplam.ToString();
            
            SqlCommand cmd = new SqlCommand("select userAd,userSoyad,userID from Tbl_Kullanici where userTc=@p1", bg.connection());
            cmd.Parameters.AddWithValue("@p1",tc);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                label4.Text = dr[0].ToString();
                label5.Text = dr[1].ToString();
                label7.Text = dr[2].ToString();
            }
            bg.connection().Close();

            SqlCommand cmd7 = new SqlCommand("select userID from Tbl_Cuzdan where userTc=@l1", bg.connection());
            cmd7.Parameters.AddWithValue("@l1", tc);
            SqlDataReader dr3 = cmd7.ExecuteReader();
            if (dr3.Read())
            {
                bg.connection().Close();
                SqlCommand cmd3 = new SqlCommand("select tl,dolar,euro,sterlin,kuveyt from Tbl_Cuzdan where userTc=@k1", bg.connection());
                cmd3.Parameters.AddWithValue("@k1", tc);
                SqlDataReader dr2 = cmd3.ExecuteReader();
                while (dr2.Read())
                {
                    label9.Text = dr2[0].ToString();
                    label8.Text = dr2[1].ToString();
                    label39.Text = dr2[2].ToString();
                    label38.Text = dr2[3].ToString();
                    label43.Text = dr2[4].ToString();

                    chart1.Series["Bakiye"].Points.Clear();
                    chart1.Series["Bakiye"].Points.AddXY("₺", double.Parse(dr2[0].ToString()));
                    if (label8.Text != "0")
                    {
                        chart1.Series["Bakiye"].Points.AddXY("$", double.Parse(label8.Text) * double.Parse(label29.Text));
                    }
                    if (label39.Text != "0")
                    {
                        chart1.Series["Bakiye"].Points.AddXY("€", double.Parse(label39.Text) * double.Parse(label30.Text));
                    }
                    if (label38.Text != "0")
                    {
                        chart1.Series["Bakiye"].Points.AddXY("£", double.Parse(label38.Text) * double.Parse(label32.Text));
                    }
                    if (label43.Text != "0")
                    {
                        chart1.Series["Bakiye"].Points.AddXY("kwd", double.Parse(label43.Text) * double.Parse(label34.Text));
                    }
                }
                bg.connection().Close();
            }
            else
            {
                bg.connection().Close();
                SqlCommand cmd2 = new SqlCommand("insert into Tbl_Cuzdan (userID,userTc) values (@s1,@s2)", bg.connection());
                cmd2.Parameters.AddWithValue("@s1", Convert.ToInt32(label7.Text));
                cmd2.Parameters.AddWithValue("@s2", tc);
                cmd2.ExecuteNonQuery();
                bg.connection().Close();
            }

            kurlar(decimal.Parse(label29.Text), decimal.Parse(label20.Text), label18.Text, label23.Text);
            kurlar(decimal.Parse(label30.Text), decimal.Parse(label31.Text), label18.Text, label24.Text);
            kurlar(decimal.Parse(label32.Text), decimal.Parse(label33.Text), label18.Text, label25.Text);
            kurlar(decimal.Parse(label34.Text), decimal.Parse(label35.Text), label18.Text, label26.Text);
            

        }
        private void kurlar(decimal d1, decimal d2, string s1,string s2)
        {
            SqlCommand cmd5 = new SqlCommand("update Tbl_Doviz set dovizAl=@v1,dovizSat=@v2,tarih=@v3 where dovizAd=@v4", bg.connection());
            cmd5.Parameters.AddWithValue("@v1",d1);
            cmd5.Parameters.AddWithValue("@v2", d2);
            cmd5.Parameters.AddWithValue("@v3", s1);
            cmd5.Parameters.AddWithValue("@v4", s2);
            cmd5.ExecuteNonQuery();
            bg.connection().Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void yazdir(string url)
        {
            string url1 = url;
            var obj = new XmlDocument();
            obj.Load(url1);
            DateTime tarih = Convert.ToDateTime(obj.SelectSingleNode("//Tarih_Date").Attributes["Tarih"].Value);
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
            label20.Text = dolarsatis.ToString(); label20.Text = label20.Text.Replace(".", ",");
            label29.Text = dolaralis.ToString(); label29.Text = label29.Text.Replace(".", ",");
            label30.Text = euroalis.ToString(); label30.Text = label30.Text.Replace(".", ",");
            label31.Text = eurosatis.ToString(); label31.Text = label31.Text.Replace(".", ",");
            label32.Text = sterlinalis.ToString(); label32.Text = label32.Text.Replace(".", ",");
            label33.Text = sterlinsatis.ToString(); label33.Text = label33.Text.Replace(".", ",");
            label34.Text = kuveytalis.ToString(); label34.Text = label34.Text.Replace(".", ",");
            label35.Text = kuveytsatis.ToString(); label35.Text = label35.Text.Replace(".", ",");
        }
        string url2;
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateTime secilenTarih = dateTimePicker1.Value;

            // Seçilen tarihi istediğiniz formatta gösterme
            string formatliTarih = secilenTarih.ToString("ddMMyyyy");
            string tarihFormatli = secilenTarih.ToString("yyyyMM");
            url2 = $"https://www.tcmb.gov.tr/kurlar/{tarihFormatli}/{formatliTarih}.xml";
            
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
            Bakiye bk = new Bakiye();
            bk.tc = tc;
            bk.ID = Convert.ToInt32(label7.Text);
            bk.Show();
            this.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Al a = new Al();
            a.tc = tc;
            a.ID=Convert.ToInt32(label7.Text);
            a.Show();
            this.Visible=false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Sat s = new Sat();
            s.tc = tc;
            s.ID=Convert.ToInt32(label7.Text);
            s.Show();
            this.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Düzenle d= new Düzenle();
            d.tc= tc;
            d.Show();
            this.Visible=false;
        }


        private void button7_Click(object sender, EventArgs e)
        {
            Cek c= new Cek();
            c.ID = Convert.ToInt32(label7.Text);
            c.tc= tc;
            c.Show();
            this.Visible=false;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Yardim y = new Yardim();
            y.ID = Convert.ToInt32(label7.Text);
            y.tc = tc;
            y.Show();
            this.Visible = false;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Giris g= new Giris();
            g.Show();
            this.Close();
        }
    }
}
