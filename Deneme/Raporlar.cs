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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Deneme
{
    public partial class Raporlar : Form
    {
        public Raporlar()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source = YASAR\\SQLEXPRESS; Initial Catalog = hastane; Integrated Security = True");
        private void button1_Click(object sender, EventArgs e)
        {
            DateTime baslangicTarihi = dateTimePicker1.Value;
            DateTime bitisTarihi = dateTimePicker2.Value;
            int taburcuDurumu = GetTaburcuDurumu();
            string query = "SELECT * FROM Hasta_Table WHERE Sevk_Tarihi BETWEEN @BaslangicTarihi AND @BitisTarihi";

            if (taburcuDurumu == 0 || taburcuDurumu == 1)
            {
                // Eğer bir taburcu durumu seçilmişse, filtrele
                query += " AND Taburcu = @TaburcuDurumu";
            }

            using (SqlCommand command = new SqlCommand(query, baglanti))
                {
                command.Parameters.AddWithValue("@BaslangicTarihi", dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("@BitisTarihi", dateTimePicker2.Value.ToString("yyyy-MM-dd"));

                command.Parameters.AddWithValue("@TaburcuDurumu", taburcuDurumu);

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
        private int GetTaburcuDurumu()
        {
            if (radioButton1.Checked)
                return 1; // Taburcu olmuş durumu
            else if (radioButton2.Checked)
                return 0; // Taburcu olmamış durumu
            else
                return -1; // Hiçbiri seçilmediyse, istediğiniz bir değeri kullanabilirsiniz.
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
        }
    }
}
