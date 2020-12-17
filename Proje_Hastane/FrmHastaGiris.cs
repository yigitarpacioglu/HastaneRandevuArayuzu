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
    public partial class FrmHastaGiris : Form
    {
        public FrmHastaGiris()
        {
            InitializeComponent();
        }

        newsql nw = new newsql();
       
        private void LnkUyeOl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmHastaKayit fr = new FrmHastaKayit();
            fr.Show();

        }

        private void FrmHastaGiris_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.AcceptButton = BtnGirisYap;
        }

        private void BtnGirisYap_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Select * from Tbl_Hastalar Where HastaTC = @HastaTC and HastaSifre=@HastaSifre" ,nw.ConnSql());
            cmd.Parameters.AddWithValue("@HastaTC", mskTC.Text);
            cmd.Parameters.AddWithValue("@HastaSifre", TxtSifre.Text);
            SqlDataReader sdr = cmd.ExecuteReader();

            if(sdr.Read())
            {
                FrmHastaDetay fr = new FrmHastaDetay();
                fr.tc = mskTC.Text;
                fr.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı 'TC' veya 'Şifre' girişi yapıldı");
            }

            nw.ConnSql().Close();
        }
    }
}
