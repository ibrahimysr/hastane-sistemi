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
    public partial class Kullanici_ekle : Form
    {
        public Kullanici_ekle()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source = YASAR\\SQLEXPRESS; Initial Catalog = hastane; Integrated Security = True"); 
        private void button3_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand VeriKaydet = new SqlCommand("insert into Kullanıcılar(Kullanici_ad,Kullanici_sifre,iletisim,kurum,Kullanici_id) values(@a1,@a2,@a3,@a4,@a5)", baglanti);
            VeriKaydet.Parameters.AddWithValue("@a1", txtKullaniciad.Text.ToString());
            VeriKaydet.Parameters.AddWithValue("@a2", txtsifre.Text);
            VeriKaydet.Parameters.AddWithValue("@a3", txtiletisim.Text);
            VeriKaydet.Parameters.AddWithValue("@a4",txtkurum.Text);
            VeriKaydet.Parameters.AddWithValue("@a5", Convert.ToInt32(txtid.Text));

            VeriKaydet.ExecuteNonQuery();

            baglanti.Close();
            MessageBox.Show("Veri Eklendi");
            this.Hide();
        }

    }
}
