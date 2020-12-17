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

namespace Proje_Hastane
{
    public partial class FrmHastaKayit : Form
    {
        public FrmHastaKayit()
        {
            InitializeComponent();
        }

        newsql nw = new newsql();

        private void FrmHastaKayit_Load(object sender, EventArgs e)
        {
            this.AcceptButton = BtnKayitYap;
        }

        private void BtnKayitYap_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("insert Tbl_Hastalar (HastaAd, HastaSoyad, HastaTC, HastaTelefon,HastaSifre, HastaCinsiyet) values (@HastaAd, @HastaSoyad, @HastaTC, @HastaTelefon, @HastaSifre,@HastaCinsiyet)", nw.ConnSql());
            cmd.Parameters.AddWithValue("@HastaAd", TxtAd.Text);
            cmd.Parameters.AddWithValue("@HastaSoyad", TxtSoyad.Text);
            cmd.Parameters.AddWithValue("@HastaTC", MskTC.Text);
            cmd.Parameters.AddWithValue("@HastaTelefon", MskTelefon.Text);
            cmd.Parameters.AddWithValue("@HastaSifre", TxtSifre.Text);
            cmd.Parameters.AddWithValue("@HastaCinsiyet", cmbCinsiyet.Text);
            cmd.ExecuteNonQuery();
            nw.ConnSql().Close();
            MessageBox.Show("Kaydınız gerçekleştirilmiştir. Şifreniz: " + TxtSifre.Text, "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);



        }
    }
}
