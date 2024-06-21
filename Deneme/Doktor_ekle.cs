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
    public partial class Doktor_ekle : Form
    {
        public Doktor_ekle()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source = YASAR\\SQLEXPRESS; Initial Catalog = hastane; Integrated Security = True");

        private void button3_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand VeriKaydet = new SqlCommand("insert into Doktorlar(Doktor_Kod,Doktor_ad,Doktor_soyad,Doktor_alan,Doktor_iletisim,Doktor_kurum,Doktor_id) values(@a1,@a2,@a3,@a4,@a5,@a6,@a7)", baglanti);
            VeriKaydet.Parameters.AddWithValue("@a1", Convert.ToInt32(txtDoktor_kod.Text));
            VeriKaydet.Parameters.AddWithValue("@a2", txtDoktor_ad.Text);
            VeriKaydet.Parameters.AddWithValue("@a3", txtDoktor_soyad.Text);
            VeriKaydet.Parameters.AddWithValue("@a4", txtDoktor_alan.Text);
            VeriKaydet.Parameters.AddWithValue("@a5", txtDoktor_iletisim.Text);
            VeriKaydet.Parameters.AddWithValue("@a6", txtDoktor_kurum.Text);
            VeriKaydet.Parameters.AddWithValue("@a7", Convert.ToInt32(txtDoktor_id.Text));

            VeriKaydet.ExecuteNonQuery();

            baglanti.Close();
            MessageBox.Show("Veri Eklendi");
            this.Hide();
        }
    }
}
