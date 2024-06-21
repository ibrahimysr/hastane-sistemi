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
using System.Drawing.Printing;
namespace Deneme
{
    public partial class Hastane : Form
    { 
        public Hastane()
        {
            InitializeComponent(); 
        }
        SqlConnection baglanti = new SqlConnection("Data Source = YASAR\\SQLEXPRESS; Initial Catalog = hastane; Integrated Security = True"); private void Hastane_Load(object sender, EventArgs e)
        {
            //verileriListele();
        }
        public void SetData(string dosyaNumarasi, string ad, string Soyad,string kurum )
        {
            // TextBox'lara değerleri yerleştir
            txtDosyaNo.Text = dosyaNumarasi;
            txtAd.Text = ad;
            txtSoyad.Text = Soyad;
            txtKurum.Text = kurum;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand VeriKaydet = new SqlCommand("insert into Tahlil_Table(Poliklinik,Sıra_No,Yapilan_islem,Dr_Kodu,Miktar,Birim_Fiyat,Dosya_No) values(@a1,@a2,@a3,@a4,@a5,@a6,@a7)", baglanti);
            VeriKaydet.Parameters.AddWithValue("@a1",cmbPoliklinik.Text.ToString());
            VeriKaydet.Parameters.AddWithValue("@a2", Convert.ToInt32(txtSiraNo.Text));
            VeriKaydet.Parameters.AddWithValue("@a3", cmbYapilanİslem.Text);
            VeriKaydet.Parameters.AddWithValue("@a4", Convert.ToInt32(cmbDoktorKodu.Text));
            VeriKaydet.Parameters.AddWithValue("@a5", numericMiktar.Value);
            VeriKaydet.Parameters.AddWithValue("@a6", Convert.ToInt32(txtFiyat.Text));
            VeriKaydet.Parameters.AddWithValue("@a7", Convert.ToInt32(txtDosyaNo.Text));

            VeriKaydet.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Veri Eklendi");


            verileriListele();
        }
        public void verileriListele()
        {
            baglanti.Open();
            SqlDataAdapter veri = new SqlDataAdapter("select *from Tahlil_Table", baglanti);
            DataTable table = new DataTable();
            veri.Fill(table);
            dataGridView1.DataSource = table;
            baglanti.Close();
            toplamFiyat();
        }
        void toplamFiyat()
        {
            baglanti.Open();
            SqlCommand doldur = new SqlCommand("select SUM(Birim_Fiyat * Miktar) from Tahlil_Table", baglanti);
            SqlDataReader data = doldur.ExecuteReader();
            while (data.Read())
            {
                toplamLabel.Text = data[0].ToString() + "Tl";

            }

            baglanti.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            txtDosyaNo.Clear(); 
            txtFiyat.Clear();
            txtAd.Clear();
            txtKurum.Clear();
            txtSiraNo.Clear();
            txtSoyad.Clear();
            cmbDoktorKodu.Text = "";
            cmbPoliklinik.Text = "";
            cmbYapilanİslem.Text = "";
            dataGridView1.DataSource = null;
            toplamLabel.Text = "0 TL";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // Seçili satırı kontrol et
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Seçili satırın Id'sini al
                int selectedId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Sıra_No"].Value);

                // Silme işlemini gerçekleştir
                DeleteRecord(selectedId);

                // DataGridView'den seçili satırı kaldır
                dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
            }
            else
            {
                MessageBox.Show("Lütfen silinecek bir satır seçin.");
            }
            void DeleteRecord(int id)
            {
               
                
                    baglanti.Open();

                    
                    SqlCommand command = new SqlCommand("DELETE FROM Tahlil_Table WHERE Sıra_No = @Id", baglanti);
                    command.Parameters.AddWithValue("@Id", id);

                    command.ExecuteNonQuery();
                MessageBox.Show("Veri Silindi");
                baglanti.Close();

            }
           txtDosyaNo.Clear();
            txtAd.Clear();  
            txtSoyad.Clear();
            txtKurum.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            hasta_ekle hasta_ekleme = new hasta_ekle();
            hasta_ekleme.Show();
        }

        public void button1_Click(object sender, EventArgs e)
        {
            dosya_arama dosya = new dosya_arama();
           

           
           
            dosya.Show(); 
            this.Hide();

        }

        private void button7_Click(object sender, EventArgs e)
        {
            int dosyaNo = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Dosya_No"].Value);

            // İkinci tabloyu güncelle
            SqlCommand taburcuGuncelle = new SqlCommand("UPDATE Hasta_Table SET Taburcu = 1 WHERE Dosya_No = @dosyaNo", baglanti);
            taburcuGuncelle.Parameters.AddWithValue("@dosyaNo", dosyaNo);

            baglanti.Open();
            taburcuGuncelle.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Hasta Taburcu Oldu"); 
            
        }

        private void button8_Click(object sender, EventArgs e)
        {
            // Seçilen verileri al
            DataTable selectedData = GetSelectedData();

            // Yazdırma işlemine başla
            printDocument1.Print();
        }
        private DataTable GetSelectedData()
        {
            // Seçili satırları al
            DataGridViewSelectedRowCollection selectedRows = dataGridView1.SelectedRows;

            // Seçilen satırları içeren bir DataTable oluştur
            DataTable selectedData = new DataTable();
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                selectedData.Columns.Add(column.Name);
            }

            foreach (DataGridViewRow row in selectedRows)
            {
                DataRow newRow = selectedData.NewRow();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    newRow[cell.ColumnIndex] = cell.Value;
                }
                selectedData.Rows.Add(newRow);
            }

            return selectedData;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            // Seçilen verileri al
            DataTable selectedData = GetSelectedData();

            // Baskı ön izleme penceresini göster
            printPreviewDialog1.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {

            Font Baslik = new Font("Bahnschrift SemiBold", 20, FontStyle.Bold);

            Font Ana = new Font("Arial Black", 20, FontStyle.Bold);

            SolidBrush sb = new SolidBrush(Color.Black);
            Pen myPen = new Pen(Color.Black);

            String Poliklinik = dataGridView1.SelectedRows[0].Cells["Poliklinik"].Value.ToString();
            int sira_no = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Sıra_No"].Value);
            String yapilan_islem = dataGridView1.SelectedRows[0].Cells["Yapilan_islem"].Value.ToString();
            int Dr_kodu = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Dr_Kodu"].Value);
            int miktar = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Miktar"].Value);
            int birim_fiyat = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Birim_Fiyat"].Value);

            e.Graphics.DrawString("Poliklinik:" + Poliklinik, Baslik, sb, 0, 210);
            e.Graphics.DrawString("Sira No" + sira_no, Ana, sb, 300, 140);
            e.Graphics.DrawLine(myPen, 10, 80, 820, 80);
            e.Graphics.DrawLine(myPen, 10, 200, 820, 200);
            e.Graphics.DrawString("Yapilan İslem:" + yapilan_islem, Baslik, sb, 0, 250);

            e.Graphics.DrawString("Dr Kodu:" + Dr_kodu, Baslik, sb, 0, 300);

            e.Graphics.DrawString("Miktar:" + miktar, Baslik, sb, 0, 350);

            e.Graphics.DrawString("Birim Fiyat:" + birim_fiyat, Baslik, sb, 0, 400);

            e.Graphics.DrawString("Toplam Fiyat:" + ( miktar * birim_fiyat), Baslik, sb, 0, 450);

           





        }


    }
}
