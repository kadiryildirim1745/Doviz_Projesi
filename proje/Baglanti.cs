using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace proje
{
    internal class Baglanti
    {
        public SqlConnection connection()
        {
            SqlConnection conn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=dovizDB;Integrated Security=True");
            conn.Open();
            return conn;
        }
    }
}
