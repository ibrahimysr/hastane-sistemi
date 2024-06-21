using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Deneme
{
    public partial class Referanslar : Form
    {
        public Referanslar()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source = YASAR\\SQLEXPRESS; Initial Catalog = hastane; Integrated Security = True");
        private void txtPoliklinik_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string poliklinikAdi = txtPoliklinik.Text.Trim();

                if (!string.IsNullOrEmpty(poliklinikAdi))
                {
                    DataTable poliklinikTable = GetBilgiGetir("Poliklinik", poliklinikAdi, "ad");

                    if (poliklinikTable.Rows.Count > 0)
                    {
                        // Poliklinik bilgilerini DataGridView'a ekle
                        dataGridView1.DataSource = poliklinikTable;
                        MessageBox.Show($"Poliklinik Bilgileri:\nPoliklinik Adı: {poliklinikTable.Rows[0]["ad"]}\nİletişim: {poliklinikTable.Rows[0]["iletisim"]}\nKurum: {poliklinikTable.Rows[0]["kurum"]}\nDoktor Sayısı: {poliklinikTable.Rows[0]["doktor_sayisi"]}", "Poliklinik Bulundu");
                    }
                    else
                    {
                        DialogResult result = MessageBox.Show("Bu poliklinik kaydı bulunamadı. Yeni bir poliklinik kaydı eklemek ister misiniz?", "Kayıt Bulunamadı", MessageBoxButtons.YesNo);

                        if (result == DialogResult.Yes)
                        {
                            // Yeni poliklinik ekleme formunu aç
                            Poliklinik_ekle poliklinikEkleForm = new Poliklinik_ekle();
                            poliklinikEkleForm.Show();
                        }
                        else
                        {
                            // Formu temizle
                            txtPoliklinik.Clear();
                        }
                    }
                }
            }
        }
        private DataTable GetBilgiGetir(string tableName, string kosul, string kosul1)
        {
            using (SqlDataAdapter adapter = new SqlDataAdapter($"SELECT * FROM {tableName} WHERE {kosul1} = @kosul", baglanti))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@kosul", kosul);

                DataTable dataTable = new DataTable();
                adapter.FillSchema(dataTable, SchemaType.Source); // DataTable'ın şemasını SQL sonuçlarına uygun şekilde ayarlar
                adapter.Fill(dataTable);

                return dataTable;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Poliklinik_ekle poliklinik_form = new Poliklinik_ekle();

            poliklinik_form.Show();
        }

        private void txtKullanici_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string kullaniciadi = txtKullanici.Text.Trim();

                if (!string.IsNullOrEmpty(kullaniciadi))
                {
                    DataTable poliklinikTable = GetBilgiGetir("Kullanıcılar", kullaniciadi, "Kullanici_ad");

                    if (poliklinikTable.Rows.Count > 0)
                    {

                        dataGridView1.DataSource = poliklinikTable;

                    }
                    else
                    {
                        DialogResult result = MessageBox.Show("Bu Kullanıcı adında kayıt bulunamadı. Yeni bir Kullanıcı kaydı eklemek ister misiniz?", "Kayıt Bulunamadı", MessageBoxButtons.YesNo);

                        if (result == DialogResult.Yes)
                        {

                            Kullanici_ekle kullanici = new Kullanici_ekle();
                            kullanici.Show();
                        }
                        else
                        {

                            txtPoliklinik.Clear();
                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Kullanici_ekle kullanici = new Kullanici_ekle();
            kullanici.Show();
        }

        private void txtDoktor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string doktorkodu = txtDoktor.Text.Trim();

                if (!string.IsNullOrEmpty(doktorkodu))
                {
                    DataTable poliklinikTable = GetBilgiGetir("Doktorlar", doktorkodu, "Doktor_Kod");

                    if (poliklinikTable.Rows.Count > 0)
                    {

                        dataGridView1.DataSource = poliklinikTable;

                    }
                    else
                    {
                        DialogResult result = MessageBox.Show("Bu Doktor Kodunda kayıt bulunamadı. Yeni bir Doktor kaydı eklemek ister misiniz?", "Kayıt Bulunamadı", MessageBoxButtons.YesNo);

                        if (result == DialogResult.Yes)
                        {

                            Doktor_ekle doktor = new Doktor_ekle();
                            doktor.Show();
                        }
                        else
                        {

                            txtPoliklinik.Clear();
                        }
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Doktor_ekle doktor = new Doktor_ekle();
            doktor.Show();
        }

        private void txtPersonel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string personelkodu = txtPersonel.Text.Trim();

                if (!string.IsNullOrEmpty(personelkodu))
                {
                    DataTable personeltable = GetBilgiGetir("Personeller", personelkodu, "Personel_kodu");

                    if (personeltable.Rows.Count > 0)
                    {
                        dataGridView1.DataSource = personeltable;

                    }
                    else
                    {
                        DialogResult result = MessageBox.Show("Bu Personel Kodunda kayıt bulunamadı. Yeni bir Personel kaydı eklemek ister misiniz?", "Kayıt Bulunamadı", MessageBoxButtons.YesNo);

                        if (result == DialogResult.Yes)
                        {
                            Personel_Ekle personel = new Personel_Ekle();
                            personel.Show();
                        }
                        else
                        {
                            txtPoliklinik.Clear();
                        }
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

            Personel_Ekle personel = new Personel_Ekle();
            personel.Show();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {

                string kimlikAlanı = dataGridView1.Columns[0].Name;
                string secilenveri = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();

                YonlendirmeYap(kimlikAlanı, secilenveri);
            }
        }
        private void YonlendirmeYap(string kimlikAlanı, string secilenVeri)
        {
            if (kimlikAlanı == "Doktor_id")
            {
                Doktor_ekle doktorSayfa = new Doktor_ekle();
                doktorSayfa.Show();
                VeriyiGetir("Doktorlar", secilenVeri, "Doktor_id");
            }
            else if (kimlikAlanı == "Polinik_id")
            {
                Poliklinik_ekle polinikSayfa = new Poliklinik_ekle();
                polinikSayfa.Show();

            }
            // Diğer durumlar için gerekli yönlendirmeleri ve TextBox'lara veriyi yazdırma işlemlerini ekleyin
        }
        private string VeriyiGetir(string tabloAdi, string secilenVeri, string id)
        {
            string value = string.Empty;
            baglanti.Open();
            string query = $"SELECT * FROM {tabloAdi} WHERE {id} = @SecilenVeri"; // Örnek bir sorgu, Doktor_id'yi kendi koşullarınıza göre güncelleyin

            using (SqlCommand command = new SqlCommand(query, baglanti))
            {
                command.Parameters.AddWithValue("@SecilenVeri", secilenVeri);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        value = reader.ToString();
                    }
                }
            }

            baglanti.Close();
            return value;
        }




    }


}

