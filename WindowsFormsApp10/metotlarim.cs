using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsFormsApp10
{
    internal class metotlarim
    {
        SqlConnection bag = new SqlConnection(@"server=.\SQLExpress;initial catalog=anket;integrated security=true");

        public void sorularigetir(ComboBox cb)
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from sorular order by soru", bag);
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            cb.DataSource = tablo;
            cb.ValueMember = "soruno";
            cb.DisplayMember = "soru";
        }

        public void cevaplarigetir(string soruno, ListBox lb)
        {
            string sql = "select * from cevaplar where soruno=" + soruno;
            SqlDataAdapter da = new SqlDataAdapter(sql, bag);
            da.SelectCommand.Parameters.AddWithValue("@soruno", soruno);
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            lb.DataSource = tablo;
            lb.ValueMember = "cevapno";
            lb.DisplayMember = "cevap";
        }

        public void oyver(string cevapno)
        {
            string sql = "update cevaplar set oy=oy+1 where cevapno=@cevapno";
            SqlCommand komut = new SqlCommand(sql, bag);
            komut.Parameters.AddWithValue("@cevapno", cevapno);
            bag.Open();
            komut.ExecuteNonQuery();
            MessageBox.Show("Oy Verildi", "Oy");
            bag.Close();
        }

        public void grafik_goster(string soruno, Chart c)
        {
            string sql = "select sum(oy) from cevaplar where soruno=@soruno";
            SqlDataAdapter da = new SqlDataAdapter(sql, bag);
            da.SelectCommand.Parameters.AddWithValue("@soruno", soruno);
            DataTable dt = new DataTable();
            da.Fill(dt);
            int toplam = Convert.ToInt32(dt.Rows[0][0]);

            sql = "select cevap,oy,((oy*100)/" + toplam.ToString() + ") as yuzde from cevaplar where soruno=@soruno2";
            SqlDataAdapter da2 = new SqlDataAdapter(sql, bag);
            da2.SelectCommand.Parameters.AddWithValue("@soruno2", soruno);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            c.DataSource = dt2;
            c.Series[0].XValueMember = "cevap";
            c.Series[0].YValueMembers = "yuzde";
            c.Series[0].Name = "Cevaplar";
            c.Series[0].ChartType = SeriesChartType.Pie;
            c.DataBind();
        }
    }
}
