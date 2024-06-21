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

namespace Deneme
{
    public partial class Poliklinik_ekle : Form
    {
        public Poliklinik_ekle()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source = YASAR\\SQLEXPRESS; Initial Catalog = hastane; Integrated Security = True");
        private void button3_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand VeriKaydet = new SqlCommand("insert into Poliklinik(ad,iletisim,kurum,doktor_sayisi,Poliklinik_id) values(@a1,@a2,@a3,@a4,@a5)", baglanti);
            VeriKaydet.Parameters.AddWithValue("@a1", txtPoliklinikad.Text.ToString());
            VeriKaydet.Parameters.AddWithValue("@a2",txtiletisim.Text);
            VeriKaydet.Parameters.AddWithValue("@a3", txtkurum.Text);
            VeriKaydet.Parameters.AddWithValue("@a4", Convert.ToInt32(txtdoktor.Text));
            VeriKaydet.Parameters.AddWithValue("@a5", Convert.ToInt32(textBox1.Text));

            VeriKaydet.ExecuteNonQuery();

            baglanti.Close();
            MessageBox.Show("Veri Eklendi");
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand Veriguncellet = new SqlCommand("UPDATE Poliklinik SET  ad = @a2, iletisim = @a3, kurum = @a4, doktor_sayisi = @a5, WHERE Poliklinik_id = @a1", baglanti);
            Veriguncellet.Parameters.AddWithValue("@a1", Convert.ToInt32(textBox1.Text));
            Veriguncellet.Parameters.AddWithValue("@a2", txtPoliklinikad.Text);
            Veriguncellet.Parameters.AddWithValue("@a3", txtiletisim.Text);
            Veriguncellet.Parameters.AddWithValue("@a4", txtkurum.Text);
            Veriguncellet.Parameters.AddWithValue("@a5", Convert.ToInt32(txtdoktor.Text));
          


            Veriguncellet.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kayıt Güncellendi");
            this.Hide();
        
        }

        private void Poliklinik_ekle_Load(object sender, EventArgs e)
        {

        }
    }
}
