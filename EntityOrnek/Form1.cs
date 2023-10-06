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

namespace EntityOrnek
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        DbSinavOgrenciEntities db = new DbSinavOgrenciEntities();
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void BtnDersListesi_Click(object sender, EventArgs e)
        {
            SqlConnection baglanti = new SqlConnection(@"Data Source=HSYN-HUAWEI;Initial Catalog=DbSinavOgrenci;Integrated Security=True");
            SqlCommand komut = new SqlCommand("Select * From tbldersler", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void BtnÖğrenciListele_Click(object sender, EventArgs e)
        {
            
            dataGridView1.DataSource = db.TBLOGRENCI.ToList();

        }

        private void BtnNotListesi_Click(object sender, EventArgs e)
        {
            var query = from item in db.TBLNOTLAR
                        select new { item.NOTID, item.OGR, item.DERS, item.SINAV1, item.SINAV2, item.SINAV3, item.ORTALAMA, item.DURUM };
            dataGridView1.DataSource = query.ToList();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            TBLOGRENCI o = new TBLOGRENCI();
            o.AD = TxtAd.Text;
            o.SOYAD = TxtSoyad.Text;
            db.TBLOGRENCI.Add(o);
            db.SaveChanges();
            MessageBox.Show("Öğrenci başarıyla eklenmiştir.");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            TBLDERSLER d = new TBLDERSLER();
            d.DERSAD = TxtDersAd.Text;
            db.TBLDERSLER.Add(d);
            db.SaveChanges();
            MessageBox.Show("Ders başarıyla eklenmiştir.");
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            //Girilen Id bilgisine göre öğrenciyi siliyor.

            int id = Convert.ToInt32(TxtÖğrenciID.Text);
            var x = db.TBLOGRENCI.Find(id);
            db.TBLOGRENCI.Remove(x);
            db.SaveChanges();
            MessageBox.Show("Öğrenci silindi!");
        }

        private void BtnGüncelle_Click(object sender, EventArgs e)
        {
            //güncelleme işleminde id ye göre atama yapıyor. 

            int id = Convert.ToInt32(TxtÖğrenciID.Text);
            var y = db.TBLOGRENCI.Find(id);
            y.AD = TxtAd.Text;
            y.SOYAD = TxtSoyad.Text;
            y.FOTOGRAF = TxtFoto.Text;
            db.SaveChanges();
            MessageBox.Show("Öğrenci güncellendi.");
        }

        private void BtnProsedur_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.NOTLISTESI();
        }

        private void BtnBul_Click(object sender, EventArgs e)
        {

        }
    }
}
