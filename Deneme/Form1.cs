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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source = YASAR\\SQLEXPRESS; Initial Catalog = hastane; Integrated Security = True");

        private void button1_Click(object sender, EventArgs e)
        {
            string kullaniciAdi = textBox1.Text;
            string sifre = textBox2.Text;

            if (GirisKontrolu(kullaniciAdi, sifre))
            {
                // Eğer kullanıcı adı ve şifre eşleşiyorsa, istediğiniz forma yönlendirin.
                ana_form anaForm = new ana_form();
                anaForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Kullanıcı adı veya şifre yanlış!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglanti.Close();

            }
        }
        private bool GirisKontrolu(string kullaniciAdi, string sifre)
        {
          

          
                baglanti.Open();

                // SQL sorgusunu güncelleyin.
                string query = "SELECT * FROM Kullanıcılar WHERE Kullanici_ad=@kullaniciAd AND Kullanici_sifre=@sifre";

                using (SqlCommand command = new SqlCommand(query, baglanti))
                {
                    command.Parameters.AddWithValue("@kullaniciAd", kullaniciAdi);
                    command.Parameters.AddWithValue("@sifre", sifre);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                    

                    return reader.HasRows;
                    }
              

            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
        }
    }
}
