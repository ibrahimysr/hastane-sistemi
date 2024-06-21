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


namespace Deneme
{
    public partial class hasta_ekle : Form
    {
        public hasta_ekle()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source = YASAR\\SQLEXPRESS; Initial Catalog = hastane; Integrated Security = True");

        private void button3_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand VeriKaydet = new SqlCommand("insert into Hasta_Table(Dosya_No,Ad,Soyad,Sevk_Tarihi,Poliklinik,Taburcu,Kurum_Adi,İletisim,TC,Tahlil,Kayıt_Saati) values(@a1,@a2,@a3,@a4,@a5,@a6,@a7,@a8,@a9,@a10,@a11)", baglanti);
            VeriKaydet.Parameters.AddWithValue("@a1", Convert.ToInt32(txtDosyaNo.Text));
            VeriKaydet.Parameters.AddWithValue("@a2", txtAd.Text);
            VeriKaydet.Parameters.AddWithValue("@a3", txtSoyad.Text);
            VeriKaydet.Parameters.AddWithValue("@a4", dateTimePicker1.Value.ToString("yyyy-MM-dd"));
            VeriKaydet.Parameters.AddWithValue("@a5", cmbPoliklinik.Text.ToString());
            VeriKaydet.Parameters.AddWithValue("@a6", 0);
            VeriKaydet.Parameters.AddWithValue("@a7", txtKurumAdi.Text);
            VeriKaydet.Parameters.AddWithValue("@a8", txtiletisim.Text);
            VeriKaydet.Parameters.AddWithValue("@a9", txtTC.Text);
            VeriKaydet.Parameters.AddWithValue("@a10", txtTahlil.Text);
            VeriKaydet.Parameters.AddWithValue("@a11", dateTimePicker1.Value.ToString("HH:mm"));





            VeriKaydet.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Hasta Kayıt Edildi");
            temizle();
            verileri_listele();
        }
        void temizle()
        {
            txtDosyaNo.Clear();
            txtAd.Clear();
            txtSoyad.Clear();
            txtKurumAdi.Clear();
            txtiletisim.Clear();
            cmbPoliklinik.Text = ""; 
            txtTC.Clear();
            txtTahlil.Clear();

        }

        private void hasta_ekle_Load(object sender, EventArgs e)
        {
            verileri_listele();
        }
        void verileri_listele()
        {
            baglanti.Open();
            SqlDataAdapter veri = new SqlDataAdapter("select * from Hasta_Table", baglanti);
            DataTable table = new DataTable();
            veri.Fill(table);
            dataGridView1.DataSource = table;
            baglanti.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string query = "SELECT * FROM Hasta_Table WHERE Dosya_No = @dosyano";


            using (SqlCommand command = new SqlCommand(query, baglanti))
            {
                command.Parameters.AddWithValue("@dosyano", txtDosyaNo.Text);
                

                baglanti.Open();

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable table = new DataTable();

                // Verileri DataTable'e doldur
                adapter.Fill(table);

                // DataTable'i DataGridView'e ata
                dataGridView1.DataSource = table;
                baglanti.Close();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtDosyaNo.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtAd.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtSoyad.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            dateTimePicker1.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            cmbPoliklinik.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
           txtTaburcu.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            txtKurumAdi.Text =  dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            txtiletisim.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();  
            txtTC.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString(); 
            txtTahlil.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand Veriguncellet = new SqlCommand("UPDATE Hasta_Table SET  Ad = @a2, Soyad = @a3, Sevk_Tarihi = @a4, Poliklinik = @a5, Taburcu = @a6, Kurum_Adi = @a7, İletisim = @a8, TC = @a9, Tahlil = @a10 WHERE Dosya_No = @a1", baglanti);
            Veriguncellet.Parameters.AddWithValue("@a1", Convert.ToInt32(txtDosyaNo.Text));
            Veriguncellet.Parameters.AddWithValue("@a2", txtAd.Text);
            Veriguncellet.Parameters.AddWithValue("@a3", txtSoyad.Text);
            Veriguncellet.Parameters.AddWithValue("@a4", dateTimePicker1.Value.ToString("yyyy-MM-dd"));
            Veriguncellet.Parameters.AddWithValue("@a5", cmbPoliklinik.Text.ToString());
            Veriguncellet.Parameters.AddWithValue("@a6", txtTaburcu.Text);
            Veriguncellet.Parameters.AddWithValue("@a7", txtKurumAdi.Text);
            Veriguncellet.Parameters.AddWithValue("@a8", txtiletisim.Text);
            Veriguncellet.Parameters.AddWithValue("@a9", txtTC.Text);
            Veriguncellet.Parameters.AddWithValue("@a10", txtTahlil.Text);
  
          
            Veriguncellet.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Hasta Kayıt Güncellendi");
            temizle();
            verileri_listele();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand VeriSil = new SqlCommand("Delete from Hasta_Table where Dosya_No=@Id", baglanti);
            VeriSil.Parameters.AddWithValue("@Id", txtDosyaNo.Text);
            VeriSil.ExecuteNonQuery();
            baglanti.Close();
            verileri_listele();
            MessageBox.Show("Veri silindi");
        }
    }
}
