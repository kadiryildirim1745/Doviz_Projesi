using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace proje
{
    public partial class Cozum : Form
    {
        public Cozum()
        {
            InitializeComponent();
        }
        Baglanti bg= new Baglanti();
        private void Cozum_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select userAd as 'Ad', userSoyad as 'Soyad', sikayetTur as 'Şikayet Türü' ,sikayet as 'Şikayet ',tarih as'Tarih' from Tbl_Sikayet inner join Tbl_Kullanici on Tbl_Kullanici.userID=Tbl_Sikayet.userID ", bg.connection());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Admin a= new Admin();
            a.Show();
            this.Close();
        }
    }
}
