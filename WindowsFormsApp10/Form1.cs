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
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsFormsApp10
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private metotlarim mt = new metotlarim();

        private void label2_Click(object sender, EventArgs e)
        {
            //Hatalı Açılmıştır
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            mt.sorularigetir(comboBox1);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string soruno = comboBox1.SelectedValue.ToString();
                mt.cevaplarigetir(soruno, listBox1);
                mt.grafik_goster(soruno, chart1);
            }
            catch (Exception)
            {
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                string cevapno = listBox1.SelectedValue.ToString();
                string soruno = comboBox1.SelectedValue.ToString();
                mt.oyver(cevapno);
                mt.grafik_goster(soruno, chart1);
            }
            else
            {
                MessageBox.Show("Bir cevap seçmelisiniz!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult cevap = MessageBox.Show("Çıkmak istyor musunuz?", "Program Kapanacaktır!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (cevap == DialogResult.Yes)
            {
                Application.Exit();
            }

        }
    }
}
