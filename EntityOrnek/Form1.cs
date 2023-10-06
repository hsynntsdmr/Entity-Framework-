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
            dataGridView1.DataSource = db.TBLOGRENCI.Where(x => x.AD == TxtAd.Text | x.SOYAD == TxtSoyad.Text).ToList() ;
        }

        private void TxtAd_TextChanged(object sender, EventArgs e)
        {
            string aranan = TxtAd.Text;
            var degerler = from item in db.TBLOGRENCI
                           where item.AD.Contains(aranan)
                           select item;
            dataGridView1.DataSource = degerler.ToList();

          
        }

        private void linq_Click(object sender, EventArgs e)
        {
            if(radioButton1.Checked == true)
            {
                List<TBLOGRENCI> liste1 = db.TBLOGRENCI.OrderBy(p => p.AD).ToList();
                dataGridView1.DataSource = liste1;
            }
            // descending olarak sıralamak için
            else if (radioButton2.Checked == true)
            {
                List<TBLOGRENCI> liste2 = db.TBLOGRENCI.OrderByDescending(p => p.AD).ToList();
                dataGridView1.DataSource = liste2;
            }
            //sadece ilk 3 kaydı almak için
            else if (radioButton3.Checked == true)
            {
                List<TBLOGRENCI> liste3 = db.TBLOGRENCI.OrderBy(p => p.AD).Take(3).ToList();
                dataGridView1.DataSource = liste3;
            }
            // ID'ye göre getirmek için
            else if (radioButton4.Checked == true)
            {
                int id = Convert.ToInt32(TxtÖğrenciID.Text);
                List<TBLOGRENCI> liste4 = db.TBLOGRENCI.Where(p=> p.ID == id).ToList();
                dataGridView1.DataSource = liste4;
            }
            // Ad'ı a ile başlayanlar
            else if (radioButton5.Checked == true)
            {
                List<TBLOGRENCI> liste5 = db.TBLOGRENCI.Where(p => p.AD.StartsWith("a")).ToList();
                dataGridView1.DataSource = liste5;
            }
            // Ad'ı a ile bitenler
            else if (radioButton6.Checked == true)
            {
                List<TBLOGRENCI> liste6 = db.TBLOGRENCI.Where(p => p.AD.EndsWith("a")).ToList();
                dataGridView1.DataSource = liste6;
            }
            // tabloda değer var mı
            else if (radioButton7.Checked == true)
            {
                bool deger = db.TBLOGRENCI.Any();
                MessageBox.Show(deger.ToString(), "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            // toplam değeri ver
            else if (radioButton8.Checked == true)
            {
                int toplam = db.TBLOGRENCI.Count();
                MessageBox.Show(toplam.ToString(), "Toplam Öğrenci Sayısı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            else if (radioButton9.Checked == true)
            {
                int toplam = db.TBLOGRENCI.Count();
                MessageBox.Show(toplam.ToString(), "Toplam Öğrenci Sayısı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            else if (radioButton10.Checked == true)
            {
                int toplam = db.TBLOGRENCI.Count();
                MessageBox.Show(toplam.ToString(), "Toplam Öğrenci Sayısı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            else if (radioButton11.Checked == true)
            {
                int toplam = db.TBLOGRENCI.Count();
                MessageBox.Show(toplam.ToString(), "Toplam Öğrenci Sayısı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            else if (radioButton12.Checked == true)
            {
                int toplam = db.TBLOGRENCI.Count();
                MessageBox.Show(toplam.ToString(), "Toplam Öğrenci Sayısı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }




        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
