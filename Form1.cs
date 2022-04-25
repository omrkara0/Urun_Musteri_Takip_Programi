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
    public partial class Form1 : Form
    {
        public class Musteri
        {
            public string tc;
            public string ad;
            public string soyad;
            public string tel;
            public Musteri(string tc , string ad , string soyad , string tel) {
                this.tc = tc;
                this.ad = ad;
                this.soyad = soyad;
                this.tel = tel;
            }
        }
        public class Urun
        {
            public string urunadi;
            public int miktar;
            public string tc;

            public Urun(string urunadi, int miktar, string tc)
            {
                this.urunadi = urunadi;
                this.miktar = miktar;
                this.tc = tc;
                
            }
        }


        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection baglanti  = new SqlConnection("Data Source=.;Initial Catalog=Proje;Integrated Security=True");
        DataSet ds = new DataSet();
        SqlDataReader dr;

        private void Form1_Load(object sender, EventArgs e)
        {
            combo();
        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            Musteri_ekle();

        }

       

        private void button2_Click(object sender, EventArgs e)
        {
            Urun_ekle();

        }
        private void button3_Click(object sender, EventArgs e)
        {
            musteri_listele();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            urun_listele();
        }

        public void Urun_ekle()
        {
            if (txturunadi.Text == "" || txtmiktar.Text == "" || comboBox1.Text == "")
            {
                MessageBox.Show("Geçerli Deger giriniz.");
            }
            else
            {
                Urun urun = new Urun(txturunadi.Text, Convert.ToInt32(txtmiktar.Text), comboBox1.Text);

                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into urun(urunadi,miktar,tc) values(@urunadi,@miktar,@tc)", baglanti);
                komut.Parameters.AddWithValue("@urunadi", urun.urunadi);
                komut.Parameters.AddWithValue("@miktar", urun.miktar);
                komut.Parameters.AddWithValue("@tc", urun.tc);
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Müşteri eklendi.");
                txttc.Clear();
                txtad.Clear();
                txtsoyad.Clear();
                txttel.Clear();
                combo();
            }
        }

       
        public void Musteri_ekle()
        {
            if (txttc.Text == "" || txtad.Text == "" || txtsoyad.Text == "" || txttel.Text == "")
            {
                MessageBox.Show("Geçerli Deger giriniz.");
            }
            else
            {
                Musteri musteri = new Musteri(txttc.Text, txtad.Text, txtsoyad.Text, txttel.Text);

                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into musteri(tc , ad , soyad , tel) values(@tc , @ad , @soyad , @tel)", baglanti);
                komut.Parameters.AddWithValue("@tc", musteri.tc);
                komut.Parameters.AddWithValue("@ad", musteri.ad);
                komut.Parameters.AddWithValue("@soyad", musteri.soyad);
                komut.Parameters.AddWithValue("@tel", musteri.tel);
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Müşteri eklendi.");
                txttc.Clear();
                txtad.Clear();
                txtsoyad.Clear();
                txttel.Clear();
                combo();
            }
        }
        public void musteri_listele()
        {
            baglanti.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM musteri", baglanti);
            DataTable tablo = new DataTable();
            adapter.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        public void urun_listele()
        {
            baglanti.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM urun", baglanti);
            DataTable tablo = new DataTable();
            adapter.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        public void combo()
        {
            comboBox1.Items.Clear();
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("select tc from musteri", baglanti);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0].ToString());
            }
            dr.Close();
            baglanti.Close();
        }

       
    }
}
