using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Deneme
{
    public partial class ana_form : Form
    {
        private Form currentForm;

        public ana_form()
        {
            InitializeComponent();

            currentForm = null;

        }



        private void Hasta_kabulToolStripMenuItem_Click(object sender, EventArgs e)
        {

            ClearCurrentForm();

            Hastane hastane = new Hastane();
            hastane.TopLevel = false;
            hastane.FormBorderStyle = FormBorderStyle.None;
            hastane.Dock = DockStyle.Fill;
            panel1.Controls.Add(hastane);
            hastane.Show();
            currentForm = hastane;

        }

        private void ana_form_Load(object sender, EventArgs e)
        {

            ClearCurrentForm();

            Hastane hastane = new Hastane();
            hastane.TopLevel = false;
            hastane.FormBorderStyle = FormBorderStyle.None;
            hastane.Dock = DockStyle.Fill;
            panel1.Controls.Add(hastane);
            hastane.Show();
            currentForm = hastane;
        }

        private void raporlarToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            ClearCurrentForm();
            Raporlar raporlar = new Raporlar(); 
            raporlar.TopLevel = false;
            raporlar.FormBorderStyle = FormBorderStyle.None;
            raporlar.Dock = DockStyle.Fill;
            panel1.Controls.Add(raporlar);
            raporlar.Show();
            currentForm = raporlar;

        }
        private void ClearCurrentForm()
        {
            // Mevcut form varsa panel1'den kaldır
            if (currentForm != null)
            {
                panel1.Controls.Remove(currentForm);
                currentForm.Dispose();
            }
        }

        private void hastaKabulToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearCurrentForm();
            Referanslar raporlar = new Referanslar();
            raporlar.TopLevel = false;
            raporlar.FormBorderStyle = FormBorderStyle.None;
            raporlar.Dock = DockStyle.Fill;
            panel1.Controls.Add(raporlar);
            raporlar.Show();
            currentForm = raporlar;
        }
    }
}
