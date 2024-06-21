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

namespace Deneme
{
    public partial class dosya_arama : Form
    {
        public dosya_arama()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source = YASAR\\SQLEXPRESS; Initial Catalog = hastane; Integrated Security = True");
        private void button1_Click(object sender, EventArgs e)
        {
            string kriter = comboBox1.SelectedItem.ToString();
            string kriterValue = textBox1.Text;

            // Seçilen kriter AdSoyad ise, Ad ve Soyadı ayrı ayrı sorgula
            if (kriter == "AdSoyad")
            {
                // AdSoyad'ı boşluğa göre ikiye ayır
                string[] adSoyad = kriterValue.Split(' ');

                if (adSoyad.Length == 2)
                {
                    string ad = adSoyad[0];
                    string soyad = adSoyad[1];

                    // Ad ve Soyad'a göre sorgu yap
                    string query = "SELECT * FROM Hasta_Table WHERE Ad = @ad AND Soyad = @soyad";

                   
                        using (SqlCommand command = new SqlCommand(query, baglanti))
                        {
                            command.Parameters.AddWithValue("@ad", ad);
                            command.Parameters.AddWithValue("@soyad", soyad);

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
                else
                {
                    MessageBox.Show("Lütfen geçerli bir Ad ve Soyad girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                // Diğer durumlar için sorguyu normal şekilde yap
                string query = $"SELECT * FROM HastaTablo WHERE {kriter} = @value";

                using (SqlConnection connection = new SqlConnection("Your_Connection_String"))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@value", kriterValue);

                        connection.Open();

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable table = new DataTable();

                        // Verileri DataTable'e doldur
                        adapter.Fill(table);

                        // DataTable'i DataGridView'e ata
                        dataGridView1.DataSource = table;
                    }
                }
            }
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                string dosyaNumarasi = selectedRow.Cells["Dosya_No"].Value.ToString();
                string ad = selectedRow.Cells["Ad"].Value.ToString();
                string Soyad = selectedRow.Cells["Soyad"].Value.ToString();
                string kurum = selectedRow.Cells["Kurum_Adi"].Value.ToString();
              

                Hastane myOtherForm = new Hastane();
                myOtherForm.SetData(dosyaNumarasi, ad, Soyad,kurum);
              myOtherForm.Show();
                this.Hide();
            }
        }

    }
}
