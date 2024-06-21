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
    public partial class Personel_Ekle : Form
    {
        public Personel_Ekle()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source = YASAR\\SQLEXPRESS; Initial Catalog = hastane; Integrated Security = True");
        private void button3_Click(object sender, EventArgs e)
        {

            baglanti.Open();
            SqlCommand VeriKaydet = new SqlCommand("insert into Personeller(Personel_kodu,Personel_ad,Personel_soyad,Personel_iletisim,Personel_kurum,Personel_görevi,Personel_id) values(@a1,@a2,@a3,@a4,@a5,@a6,@a7)", baglanti);
            VeriKaydet.Parameters.AddWithValue("@a1", Convert.ToInt32(txtKod.Text));
            VeriKaydet.Parameters.AddWithValue("@a2", txtAd.Text);
            VeriKaydet.Parameters.AddWithValue("@a3", TxtSoyad.Text);
            VeriKaydet.Parameters.AddWithValue("@a4", txtiletisim.Text);
            VeriKaydet.Parameters.AddWithValue("@a5", txtkurum.Text);
            VeriKaydet.Parameters.AddWithValue("@a6", txtgorev.Text);
            VeriKaydet.Parameters.AddWithValue("@a7", Convert.ToInt32(txtid.Text));

            VeriKaydet.ExecuteNonQuery();

            baglanti.Close();
            MessageBox.Show("Veri Eklendi");
            this.Hide();
        }
    }
}
